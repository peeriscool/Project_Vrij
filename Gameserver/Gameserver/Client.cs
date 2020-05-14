using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
namespace Gameserver
{
    class Client
    {
        public static int dataBufferSize = 4096; //message size
        public int id;
        public TCP tcp; //refrence to tcp
        public UDP udp;
        public Player player; //refrence to player
        public Client(int _clientId) //asign id and init instance TCP
        {
            id = _clientId;
            tcp = new TCP(id);
            udp = new UDP(id);

        }
        public class TCP
        {
            public TcpClient socket; //store instance from RecieveCalback

            private Packet recievedData;
            private readonly int id;
            private NetworkStream stream;
            private byte[] receiveBuffer;
            public TCP(int _id) //get id
            {
                id = _id;
            }

            public void Connect(TcpClient _socket) //assign client to socket field
            {
                socket = _socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                recievedData = new Packet();
                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCalback, null);
                //TODO send welcom packet
                ServerSend.Welcome(id, "Welcome To the server!");
            }
            public void SendData(Packet _packet)
            {
                try
                {
                    if (socket != null)
                    {
                        stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                    }
                }
                catch (Exception _ex)
                {
                    Console.WriteLine($"Error sending data 2 player {id} via TCP {_ex}");
                }
            }
            private void ReceiveCalback(IAsyncResult _result) //try catch recieving data using array based on int bytelenght
            {
                try
                {
                    int _bytelength = stream.EndRead(_result);
                    if (_bytelength <= 0)
                    {
                        Server.clients[id].Disconnect();
                        Lobby.RemovePlayer(id);
                        return;
                    }
                    byte[] _data = new Byte[_bytelength];
                    Array.Copy(receiveBuffer, _data, _bytelength); //copy recieved bythe array 
                    recievedData.Reset(HandleData(_data));
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCalback, null);

                }
                catch (Exception _ex)
                {
                    Console.WriteLine($"Error recieving tcp data: {_ex}");
                    Server.clients[id].Disconnect();
                }
            }
            private bool HandleData(byte[] _data)
            {
                int _packagrelength = 0;
                recievedData.SetBytes(_data); //sets recievedData to incoming stream

                if (recievedData.UnreadLength() >= 4) //if there is unread data
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
                            Server.packethandlers[_packetId](id, _packet);
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

            public void Disconnect()
            {
                socket.Close();
                stream = null;
                recievedData = null;
                receiveBuffer = null;
                socket = null;
                
            }
        }
        public class UDP
        {
            public IPEndPoint endpoint;

            private int id;

            public UDP(int _id)
            {
                id = _id;
            }

            public void Connect(IPEndPoint _endpoint)
            {
                endpoint = _endpoint;
               // ServerSend.UDPTEst(id);
            }
            public void SendData(Packet _packet)
            {
                Server.SendUDPData(endpoint, _packet);
            }

            public void HandleData(Packet _packetdata)
            {
                int _packetlength = _packetdata.ReadInt();
                byte[] _packetBytes = _packetdata.ReadBytes(_packetlength);

                ThreadManager.ExecuteOnMainThread(() =>
                    {
                        using (Packet _packet = new Packet(_packetBytes))
                        {
                            int _packetId = _packet.ReadInt();
                            Server.packethandlers[_packetId](id, _packet);
                        }
                    });


            }
            public void Disconnect()
            {
                endpoint = null;
            }
        }
        public void SendIntoGame(string _PlayerName)
        {
             player = new Player(id, _PlayerName, new Vector3(0, 0, 0)); //initializes a player

            foreach (Client _client in Server.clients.Values) //loop trhough dictionary to send all player information who are already connceted 2 new player
            {
                if (_client.player != null)
                {
                    if (_client.id != id)
                    {
                        ServerSend.SpawnPlayer(id, _client.player);
                    }
                }
            }

            foreach (Client _client in Server.clients.Values) //send new player information to all
            {
                if (_client.player != null)
                {
                    ServerSend.SpawnPlayer(_client.id, player); 
                }
            }
        }
        private void Disconnect()
        {
            Console.WriteLine($"{tcp.socket.Client.RemoteEndPoint} has disconnected");
            player = null;

            tcp.Disconnect();
            udp.Disconnect();
        }
    }
}
