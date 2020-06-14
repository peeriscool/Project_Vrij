
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

            Server.clients[_fromClient].player.SetInput(_inputs, _rotation);
        }
        public static void GetListOfPlayers(int _fromClient, Packet _packet)
        {
            // Console.WriteLine("recieved");
            ServerSend.SendPlayerNames(_fromClient);
        }
        public static void PlayerReady(int _fromClient, Packet _packet)
        {
            //read out packet and get bool
            bool status = _packet.ReadBool();
            //send data to lobby class
            Lobby.RecievePlayerStatus(_fromClient, status);
        }
        public static void SendAudioBytes(int _fromClient, Packet _packet)
        {

            byte[] AudioRecieved = _packet.ToArray();
            Console.WriteLine("i recieved audio now what the fuck you want me to with this :" + AudioRecieved.Length);
           AudioRecordings.AddvoiceRecord(AudioRecieved, _fromClient); //should have been reversed but o well
          //  ServerSend.SendAudioToPlayers(AudioRecieved, _fromClient); //playback audio for other players after recorded
        }

        public static void SendEpisodeName(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt(); // not sure if the player id int fucks up the string so i try reading an int first before reading string
            string episodename = _packet.ReadString();
            Console.WriteLine(episodename + " recieved episodename");
            //episode name recieved
            //send episode name to the correct user based on the Gamecode
            int SendToPlayerId = RememberGameCode.parseEpisodeName(_fromClient);
            Console.WriteLine("Sending episodename to player" + SendToPlayerId);
            ServerSend.SendEpisodeNameback(SendToPlayerId, episodename);
        }
        public static void RequestAudioForPlaybackRecieved(int _fromClient, Packet _packet)
        {
            int Dictonarykey = _packet.ReadInt() ;
            int owner = RememberGameCode.parseEpisodeName(Dictonarykey);
            try
            {
                byte[] requestedaudiofile = AudioRecordings.RecoredVoiceMessages[owner];
                ServerSend.SendAudioToPlayers(requestedaudiofile); //sends audio for everybody to hear!
            }
            catch (Exception)
            {
                Console.WriteLine("no recorded audio found in dictonary");
                throw;
            }
                
        }
        public static void Recievedvotes(int _fromClient, Packet _packet)
        {
           
            int vote1 = _packet.ReadInt();
            int vote2 = _packet.ReadInt();
            VotingSystem.RecieveVotes(vote1,vote2);


        }

        
    }
}