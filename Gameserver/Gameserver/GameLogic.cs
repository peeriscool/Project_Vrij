using System;
using System.Collections.Generic;
using System.Text;

namespace Gameserver
{
    class GameLogic
    {
        public static void Update()
        {
            foreach (Client _client in Server.clients.Values)
            {
                if(_client.player != null) //found a player? update his location rotation
                {
                    _client.player.Update();
                }
            }
            ThreadManager.UpdateMain();
        }
    }
}
