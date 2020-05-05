﻿using System;
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
        private static void SendTCPDataToAll( Packet _packet) //send packet to all players
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
                if(i != _exceptClient)
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
            string Playernames = "";
            for (int i = 1; i < Server.clients.Count; i++)
            {
                if (Server.clients[i].player != null)
                {
                    if (Server.clients[i].player.username != null)
                    {
                        Console.WriteLine(Server.clients[i].player.username);
                        Playernames += Server.clients[i].player.username.ToString() + ",";
                    }  
                }
            }


            using (Packet _packet = new Packet((int)ServerPackets.sendplayernames))
            {
                _packet.Write(Playernames);
                _packet.Write(_toClient);
                SendTCPData(_toClient, _packet);
            }

        }
        #endregion
    }
}
