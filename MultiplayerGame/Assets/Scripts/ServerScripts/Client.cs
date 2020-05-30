using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
public class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 4096;

    public string ip = "127.0.0.1"; //127.0.0.1 = localhost
    public int port = 26950;
    public int myid = 0;
    public TCP tcp;
    public UDP udp;

    private bool isConnected = false;
    private delegate void packethandler(Packet _packet); //Delegates are used to pass methods as arguments to other methods.
    private static Dictionary<int, packethandler> packetHandlers;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            //instance already exists
            Destroy(this);
        }
    }

    private void Start()
    {
        tcp = new TCP();
        udp = new UDP();
    }
    private void OnApplicationQuit()
    {
        Disconnect();
    }
    public void ConnectedToServer()
    {
        InitializeClientData();
        isConnected = true;
        tcp.Connect();
    }
   
    public class TCP
    {
        public TcpClient socket;
        private NetworkStream stream;
        private byte[] receiveBuffer;
        private Packet recievedData;
        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);
        }

        private void ConnectCallback(IAsyncResult _result)
            {
            socket.EndConnect(_result);
            if (!socket.Connected)
                {
                return;
                }
            stream = socket.GetStream();

            recievedData = new Packet();
            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
        public void SendData(Packet _Packet)
        {
            try
            {
                if (socket != null)
                {
                    stream.BeginWrite(_Packet.ToArray(), 0, _Packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                Debug.LogError(_ex);
                
            }
        }
        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                int _bytelength = stream.EndRead(_result);
                if (_bytelength <= 0)
                {
                    instance.Disconnect();
                    return;
                }
                byte[] _data = new Byte[_bytelength];
                Array.Copy(receiveBuffer, _data, _bytelength); //copy recieved bythe array 
                                                               //ToDO handle data
                recievedData.Reset(HandleData(_data)); //uses boolean fromn Handledata to define if a reset is neccisercy
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

            }
            catch (Exception _ex)
            {
                Console.WriteLine($"Error recieving tcp data: {_ex}");
                Disconnect();
            }
        }
        private bool HandleData(byte[] _data)
        {
            int _packagrelength = 0;
            recievedData.SetBytes(_data); //sets recievedData to incoming stream

            if(recievedData.UnreadLength() >= 4) //if there is unread data
            {
                _packagrelength = recievedData.ReadInt();
                if (_packagrelength <= 0) //checks if there is still some data in the stream
                {
                    return true; //resets data
                }
            }
            while (_packagrelength > 0 && _packagrelength <= recievedData.UnreadLength()) //checks package lenght
            {
                byte[] _packetBytes = recievedData.ReadBytes(_packagrelength);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet _packet = new Packet(_packetBytes))
                    {
                        int _packetId = _packet.ReadInt();
                        packetHandlers[_packetId](_packet);
                    }
                });

                _packagrelength = 0;
                if (recievedData.UnreadLength() >= 4) //if there is unread data
                {
                    _packagrelength = recievedData.ReadInt();
                    if (_packagrelength <= 0) //checks if there is still some data in the stream
                    {
                        return true; //resets data
                    }
                }
            }
            if (_packagrelength <= 1)
            {
                return true;
            }
            return false;
        }  
        private void Disconnect()
        {
            instance.Disconnect();

            stream = null;
            recievedData = null;
            receiveBuffer = null;
            socket = null;
        }
    }

    public class UDP
    {
        public UdpClient socket;
        public IPEndPoint endPoint;

        public UDP()
        {
            endPoint = new IPEndPoint(IPAddress.Parse(instance.ip), instance.port);
        }
        public void Connect(int _localPort)
        {
            socket = new UdpClient(_localPort);

            socket.Connect(endPoint);
            socket.BeginReceive(ReceiveCallback, null);

            using (Packet _packet = new Packet())
            {
                SendData(_packet);
            }
        }
        public void SendData(Packet _packet)
        {
            try
            {
                _packet.InsertInt(instance.myid);
                if (socket != null)
                {
                    socket.BeginSend(_packet.ToArray(), _packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error Sending data to server using UDP : {_ex}");
            }
        }
        public void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                Byte[] _data = socket.EndReceive(_result, ref endPoint);
                socket.BeginReceive(ReceiveCallback, null);
                if (_data.Length <4)
                {
                    instance.Disconnect();
                    return;
                }
                HandleData(_data);
                    }
            catch
            {
                Disconnect();

            }
        }
        private void HandleData(byte[] _data)
        {
            using (Packet _packet = new Packet(_data))
            {
                int _packetlenght = _packet.ReadInt();
                _data = _packet.ReadBytes(_packetlenght);
            }

            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet _packet = new Packet(_data))
                {
                    int _packetId = _packet.ReadInt();
                    packetHandlers[_packetId](_packet);
                }
            });
        }
        private void Disconnect()
        {
            instance.Disconnect();

            endPoint = null;
            socket = null;
        }
    }
    private void InitializeClientData()
    {
        packetHandlers = new Dictionary<int, packethandler>()
        {
              { (int)ServerPackets.welcome, ClientHandle.Welcome },
              { (int)ServerPackets.spawnPlayer, ClientHandle.SpawnPlayer },
              { (int)ServerPackets.playerPosition, ClientHandle.PlayerPosition },
              { (int)ServerPackets.playerRotation, ClientHandle.PlayerRotation },
              { (int)ServerPackets.sendplayernames, ClientHandle.SendPlayerNames },
              { (int)ServerPackets.startgame, ClientHandle.StartGame },
              { (int)ServerPackets.SendAudioToPlayers, ClientHandle.SendAudioToPlayers },
              { (int)ServerPackets.gamecode, ClientHandle.GameCode},
               { (int)ServerPackets.sendepisodenameback, ClientHandle.SendEpisodeNameBack},
        };
        Debug.Log("initialized packets.");
    }
    private void Disconnect()
    {
        isConnected = false;
        tcp.socket.Close();
        udp.socket.Close();

        Debug.Log("disconnected from server");
    }
}
