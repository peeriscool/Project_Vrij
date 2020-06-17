using System;
using System.Collections.Generic;
using System.Text;

namespace Gameserver
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }
        private static void SendUDpData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }
        private static void SendTCPDataToAll(Packet _packet) //send packet to all players
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet) //send packet to all players except specifik client
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }

            }


        }
        private static void SendUDPDataToAll(Packet _packet) //send packet to all players
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
        private static void SendUDPataToAll(int _exceptClient, Packet _packet) //send packet to all players except specifik client
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }

            }

        }
        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }
        public static void sayhi(int player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.sayhi))
            {
                _packet.Write(player);

                SendTCPDataToAll( _packet);
            }
        }
       
        public static void SpawnPlayer(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer)) //make a packet for spawnplayer
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.position);
                _packet.Write(_player.rotation);

                SendTCPData(_toClient, _packet);
            }
        }
        public static void PlayerPosition(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.position);

                SendUDPDataToAll(_packet);
            }
        }

        /// <summary>Sends a player's updated rotation to all clients except to himself (to avoid overwriting the local player's rotation).</summary>
        /// <param name="_player">The player whose rotation to update.</param>
        public static void PlayerRotation(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.rotation);

                SendUDPataToAll(_player.id, _packet);
            }
        }

        public static void SendPlayerNames(int _toClient) //prints all the player names to the console
        {
            int count = 0;
            string Playernames = "";
            for (int i = 1; i < Server.clients.Count; i++)
            {
                if (Server.clients[i].player != null)
                {
                    if (Server.clients[i].player.username != null)
                    {
                        count++;
                        Console.WriteLine(Server.clients[i].player.username);
                        Playernames += Server.clients[i].player.username.ToString() + ",";
                    }
                }
            }


            using (Packet _packet = new Packet((int)ServerPackets.sendplayernames))
            {
                _packet.Write(Playernames);
                _packet.Write(count);
                _packet.Write(_toClient);
                SendTCPData(_toClient, _packet);
            }

        }
        public static void StartGame()
        {
            //all players are ready send code to end lobby and start game
            //maybe make a overload method for ending rounds?
            using (Packet _packet = new Packet((int)ServerPackets.StartGame))
            {
                _packet.Write(true);
                SendTCPDataToAll(_packet);
            }
        }
        public static void GameCode(int[] gamecode)
        {
            //all players are ready send code to end lobby and start game
            //maybe make a overload method for ending rounds?
            // byte[] result = new byte[gamecode.Length * sizeof(int)];
            // Buffer.BlockCopy(gamecode, 0, result, 0, result.Length);
            string gamecodez = string.Join("", gamecode);
            using (Packet _packet = new Packet((int)ServerPackets.GameCode))
            {
                _packet.Write(gamecodez);
                SendTCPDataToAll(_packet);
            }
        }
        public static void SendAudioToPlayers(byte[] AduioBytes, int _myId) // SendTCPDataToAll ---> Server.clients[i].SendData SENDDATA ADDS 8 BYTES? WTF!
        {

            using (Packet _packet = new Packet((int)ServerPackets.SendAudioToPlayers))
            {
                _packet.Write(AduioBytes);
                SendTCPDataToAll(_myId, _packet); //except sender
                //SendTCPDataToAll(_packet);
            }
            Console.WriteLine("sending audio bytes:" + AduioBytes.Length);
        }
        public static void SendAudioToPlayers(byte[] AduioBytes) // SendTCPDataToAll ---> Server.clients[i].SendData SENDDATA ADDS 8 BYTES? WTF!
        {

            using (Packet _packet = new Packet((int)ServerPackets.SendAudioToPlayers))
            {
                _packet.Write(AduioBytes);
                SendTCPDataToAll(_packet);

            }
            Console.WriteLine("sending audio bytes:" + AduioBytes.Length);
        }
        public static void SendEpisodeNameback(int SendToThisPlayer, string EpisodeName)
        {
            Console.WriteLine(EpisodeName);
            using (Packet _packet = new Packet((int)ServerPackets.SendEpisodeNameback))
            {
                _packet.Write(EpisodeName);
                SendTCPData(SendToThisPlayer, _packet);
            }
        }

        public static void AllmessagesRecorded()
        {
            Console.WriteLine();
            using (Packet _packet = new Packet((int)ServerPackets.AllmessagesRecorded))
            {
                _packet.Write(true);
                SendTCPDataToAll(_packet);
            }
        }

        public static void SendVotingResults(List <int> result)
        {
            Console.WriteLine("Sending votes...");



            using (Packet _packet = new Packet((int)ServerPackets.SendVotingResults))
            {
                _packet.Write(result.Count); //write length of package
                foreach (int vote in result) //write every vote to as a seprate int
                {
                    _packet.Write(vote);
                } 
                SendTCPDataToAll(_packet);
            }
        }
       
        public static void giveEpisodename(string name, int _fromclient)
        {
            using (Packet _packet = new Packet((int)ServerPackets.giveEpisodename))
            {
                _packet.Write(name);
                SendTCPData(_fromclient,_packet);
            }
        }
        #endregion
    }
}
