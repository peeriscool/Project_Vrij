using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace Gameserver
{
    class Server
    {
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>(); //saved clients represented with id's
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }

        private static TcpListener tcplistner;

        private static UdpClient udplistner;

        public delegate void PacketHandler(int _fromClient, Packet _packet);
        public static Dictionary<int,PacketHandler> packethandlers;
        public static void start(int _maxPlayers, int _Port) //setup server
        {
            MaxPlayers = _maxPlayers;
            Port = _Port;
            InitializeServerData();
            tcplistner = new TcpListener(IPAddress.Any, Port);
            tcplistner.Start();
            tcplistner.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null); //begin tcplistner + callback state. this is the line of code that detects users connceted!!!

            udplistner = new UdpClient(Port);
            udplistner.BeginReceive(UDPRecieveCallback, null); // this is the line of code that detects users connceted!!!
            Console.WriteLine($"Server gestart op {Port}.");

        }
        private static void TCPConnectCallback (IAsyncResult _result) //de callback voor de tcp
        {
            TcpClient _cLient = tcplistner.EndAcceptTcpClient(_result); //store tcp instance van de tcpaccepttcpclient method
            tcplistner.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null); //conncetion met ip accept metohd
            Console.WriteLine($"Incoming Client from {_cLient.Client.RemoteEndPoint}"); //who is joining
            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (clients[i].tcp.socket == null) //if client socket is empty
                {
                    clients[i].tcp.Connect(_cLient); //assign new instance of client to an empty dictionary spot
                    return;
                }
            }
            Console.WriteLine($"{_cLient.Client.RemoteEndPoint} failed to conncect: Server is full!");
        }
        private static void UDPRecieveCallback (IAsyncResult _result)
        {
            try
            {
                IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] _data = udplistner.EndReceive(_result, ref _clientEndPoint);
                udplistner.BeginReceive(UDPRecieveCallback, null);

                if (_data.Length <4) { return; }

                using (Packet _packet = new Packet(_data))
                {
                    int _ClientiD = _packet.ReadInt();
                    if(_ClientiD == 0)
                    {
                        return;
                    }
                    if(clients[_ClientiD].udp.endpoint == null)
                    {
                        clients[_ClientiD].udp.Connect(_clientEndPoint);
                        return;
                    }
                    if (clients[_ClientiD].udp.endpoint.ToString() == _clientEndPoint.ToString())
                    {
                        clients[_ClientiD].udp.HandleData(_packet);
                    }
                }

            }
            catch (Exception _ex)
            {

                Console.WriteLine("udp error for recieving:",_ex);
            }
        }

        public static void SendUDPData(IPEndPoint _clientEndPoint,Packet _packet)
        {
            try
            {
                if ( _clientEndPoint != null)
                {
                    udplistner.BeginSend(_packet.ToArray(), _packet.Length(), _clientEndPoint, null, null);
                }
            }
            catch (Exception _ex)
            {

                Console.WriteLine("udp error for sending to:", _ex);
            }
        }
        private static void InitializeServerData() //fill client dictionary
        {
            for (int i = 1; i <= MaxPlayers; i++) //create slots for players to fill
            {
                clients.Add(i, new Client(i));
            }

            packethandlers = new Dictionary<int, PacketHandler>()
            {
                { (int)ClientPackets.welcomeReceived,ServerHandle.WelcomeReceived},
                { (int)ClientPackets.playerMovement,ServerHandle.PlayerMovement},
                { (int)ClientPackets.getlistofplayers,ServerHandle.GetListOfPlayers},
                { (int)ClientPackets.playerready,ServerHandle.PlayerReady},  

            };
            Console.WriteLine("Initialized packets");
        }
    }
}
