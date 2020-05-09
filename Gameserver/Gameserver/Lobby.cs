using System;
using System.Collections.Generic;
using System.Text;

namespace Gameserver
{
    static class Lobby
    {
        static List<ReadyPlayer> allplayerstatus = new List<ReadyPlayer>(); //list of playerstatus
       static int playersready = 0;
        static int minimalplayers = 0; //Fix!: 0 is actualy 1 players : 0 and 1
        public static void RecievePlayerStatus(int _fromClient, bool status)
        {
            Console.WriteLine("recievedplayerstatus of " + _fromClient);
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
                Console.WriteLine($"added player number {activerequest.Who +1} to the list of ready players");
            }
            //see if a player is already in the list else add him
            for (int i = 0; i < allplayerstatus.Count; i++)
            {
                if (activerequest.Who == allplayerstatus[i].Who)
                {
                    break;
                }
                else
                {
                    allplayerstatus.Add(activerequest);
                    Console.WriteLine($"added player number {activerequest.Who +1} to the list of ready players");
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
            for (int i = 0; i < allplayerstatus.Count; i++)
            {
                Console.WriteLine(i + " index");
                if (allplayerstatus[i].ReadyStatus == true)
                {
                    playersready = playersready + 1;
                }

                Console.WriteLine(playersready + " int");
                Console.WriteLine(allplayerstatus.Count + " count");
                if (playersready == allplayerstatus.Count)
                {
                    if (playersready <= minimalplayers)
                    {
                        Console.WriteLine("to few players");
                    }
                    else
                    {
                        Console.WriteLine("All Players are ready!");
                        //send message to all players that the game can begin
                        ServerSend.StartGame();        // (allplayers,ready);
                    }
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
