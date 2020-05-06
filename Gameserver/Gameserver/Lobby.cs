using System;
using System.Collections.Generic;
using System.Text;

namespace Gameserver
{
    static class Lobby
    {
        static List<ReadyPlayer> allplayerstatus = new List<ReadyPlayer>(); //list of playerstatus
       static int playersready = 0;
        public static void RecievePlayerStatus(int _fromClient, bool status)
        {
            int ClientReady = _fromClient;
            bool PlayerStatus = status;
          ReadyPlayer Player = new ReadyPlayer(_fromClient, status);
           Lobby.PlayerStatus(Player);
        }
        static void PlayerStatus(ReadyPlayer activerequest)
        {
            //firstplayer to join
            if (allplayerstatus.Count == 0)
            {
                allplayerstatus.Add(activerequest);
                Console.WriteLine("firstplayer");
            }
            //see if a player is already in the list
            for (int i = 0; i < allplayerstatus.Count; i++)
            {
                if (activerequest.Who == allplayerstatus[i].Who)
                {
                    break;
                }
                else
                {
                    allplayerstatus.Add(activerequest);
                    Console.WriteLine(allplayerstatus.Count + allplayerstatus.ToString());
                }
            }
            //foreach (ReadyPlayer i in allplayerstatus)
            //{
            //    if (activerequest.Who == i.Who)
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        allplayerstatus.Add(activerequest);
            //        Console.WriteLine(allplayerstatus.Count + allplayerstatus.ToString());
            //    }
                
            //}
            //--------------check if all players are ready----------------
            foreach (ReadyPlayer i in allplayerstatus)
            {

                if (i.ReadyStatus == true)
                {
                    playersready = +1;
                }
            }

            if(playersready == allplayerstatus.Count)
            {
                if(playersready <= 1)
                {
                    Console.WriteLine("to few players");
                }
                else
                {
                    Console.WriteLine("All Players are ready!");
                    //send message to all players that the game can begin
                    //ServerSend.         (allplayers,ready);
                }
            }
        }
    }
    class ReadyPlayer
    {
        public int Who;
        public bool ReadyStatus;
       public ReadyPlayer(int who,bool readystatus)
        {
            Who = who;
            ReadyStatus = readystatus;
        }
    }
}
