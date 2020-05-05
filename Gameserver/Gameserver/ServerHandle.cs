
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
namespace Gameserver
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet) //read out data from clients
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            Server.clients[_fromClient].SendIntoGame(_username);
        }
        //public static void UDPTestRecieve(int _fromClient, Packet _packet)
        //{
        //    string _msg = _packet.ReadString();
        //    Console.WriteLine("Received packet via UDP. message is:"+ _msg);
        //}

        public static void PlayerMovement(int _fromClient, Packet _packet) //read out data from clients
        {
            bool[] _inputs = new bool[_packet.ReadInt()]; //readoutlenght of pakcet
            for (int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i] = _packet.ReadBool(); //basicly reads the WASD keys from the playermovement method on the clientside
            }
            Quaternion _rotation = _packet.ReadQuaternion(); //reads out the rotation of playermovement method form client

            Server.clients[_fromClient].player.SetInput(_inputs,_rotation);
        }
        public static void GetListOfPlayers(int _fromClient, Packet _packet) 
        {
           // Console.WriteLine("recieved");
            ServerSend.SendPlayerNames(_fromClient);
        }

    }
}