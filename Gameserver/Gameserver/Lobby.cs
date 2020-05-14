using System;
using System.Collections.Generic;

namespace Gameserver
{
    static class Lobby
    {
        static List<ReadyPlayer> allplayerstatus = new List<ReadyPlayer>(); //list of playerstatus
        static List<ReadyPlayer> readyplayer = new List<ReadyPlayer>(); //list of playerstatus
        static int playersready = 0;
        static int minimalplayers = 1; //Fix!: 0 is actualy 1 players : 0 and 1
        static List<string> NATO = new List<string>()
        {
            "Alfa", //0
            "Bravo",
            "Charlie",
            "Delta",
            "Echo",
            "Foxtrot",
            "Golf",
            "Hotel",
            "India" //8
        };
        static int PlayerIndex = 0;
      
        public static void RecievePlayerStatus(int _fromClient, bool status)
        {
            
            Console.WriteLine("player " + _fromClient + " status: "+ status);
            Lobby.PlayerStatus(_fromClient, status);
        }
        static void PlayerStatus(int fromClient,bool _status)
        {
            ReadyPlayer activerequest = new ReadyPlayer(fromClient, _status);
            activerequest.NatoName = NATO[fromClient];
            //firstplayer to join
            if (allplayerstatus.Count == 0)
            {
                allplayerstatus.Add(activerequest);
                Console.WriteLine("First player");
            }
            if (allplayerstatus.Count != 0)
            {
                if(allplayerstatus[PlayerIndex].NatoName == activerequest.NatoName)
                {
                    //we found exsisting player
                    Console.WriteLine(activerequest.NatoName);
                    if (allplayerstatus[PlayerIndex].ReadyStatus != activerequest.ReadyStatus)
                    {
                     //   Console.WriteLine("player changed status");
                        allplayerstatus[PlayerIndex].ReadyStatus = activerequest.ReadyStatus;
                    }
                    if (allplayerstatus[PlayerIndex].ReadyStatus == false)
                    {
                        playersready -= 1;
                    }
                }
                else
                {
                    bool doiknowyou = false;
                   
                    for (int i = 0; i <allplayerstatus.Count; i++)
                    {
                        if (allplayerstatus[i].NatoName == activerequest.NatoName)
                        {
                            doiknowyou = true;
                        }
                    }
                    if(doiknowyou == false)
                    {
                        Console.WriteLine("adding new boi or grl");
                        allplayerstatus.Add(activerequest);
                    PlayerIndex += 1;
                    }
                }
            }


            // --------------check if all players are ready----------------
            for (int i = 0; i < allplayerstatus.Count; i++)
            {
                if (allplayerstatus[i].ReadyStatus == true)
                {
                    if (readyplayer.Contains(activerequest)){ }
                    else
	                {
                        playersready = playersready + 1;
                    }
                    
                }
               // Console.WriteLine("playerready count:" + playersready.ToString());
              //  Console.WriteLine("list of players:" + allplayerstatus.Count);
                //if (playersready == allplayerstatus.Count)
                //{
                    if (playersready <= minimalplayers)
                    {
                        Console.WriteLine("to few players" + playersready);
                    }
                    else
                    {
                        Console.WriteLine("All Players are ready!");
                        //send message to all players that the game can begin
                        ServerSend.StartGame();        // (allplayers,ready);
                    }
                //}

            }
           

        }
        static public void RemovePlayer(int _fromClient)
        {
            allplayerstatus.RemoveAt(_fromClient-1);
            //for (int i = 0; i < allplayerstatus.Count; i++)
            //{
            //    if (allplayerstatus[i].Who == _fromClient)
            //    {
            //        allplayerstatus.RemoveAt(i);
            //        Console.WriteLine($"player {i} is removed from ready player");
            //    }
            //}
        }
    }
    class ReadyPlayer
    {
        public int Who;
        public bool ReadyStatus;
        public string NatoName;
       public ReadyPlayer(int who,bool readystatus)
        {
            Who = who;
            ReadyStatus = readystatus;
        }
    }
}

/*
 *  static void PlayerStatus(int fromClient,bool _status)
        {
            ReadyPlayer activerequest = new ReadyPlayer(fromClient, _status);
            //firstplayer to join
            if (allplayerstatus.Count == 0)
            {
                activerequest.NatoName = NATO[1];
                allplayerstatus.Add(activerequest);
                Console.WriteLine("First player");
            }

            bool playerisfoundinlist = false;
            //see if a player is already in the list else add him
            for (int i = 0; i < allplayerstatus.Count; i++)
            {
                if (playerisfoundinlist == false)
                {
                    if (activerequest.Who == allplayerstatus[i].Who)
                    {
                        if (activerequest.ReadyStatus != allplayerstatus[i].ReadyStatus) //changes true or false of activerequest
                        {
                            Console.WriteLine("changed player status to" + activerequest.ReadyStatus);
                            allplayerstatus[i].ReadyStatus = activerequest.ReadyStatus;
                        }
                        else //nothing different
                        {
                           
                        }
                        playerisfoundinlist = true;
                    }
                   // else if(allplayerstatus[i].NatoName != activerequest.NatoName || activerequest.NatoName == null)
                   else if(playerisfoundinlist == false)
                    {

                        allplayerstatus.Add(activerequest);
                //        Console.WriteLine($"added player number {activerequest.Who} on place {allplayerstatus[activerequest.Who]} to the list of players");
                    }
                }
            }   
            //if (activerequest.ReadyStatus == false)
            //{
            //    RemovePlayer(activerequest.Who);
            //}

            // --------------check if all players are ready----------------
            for (int i = 0; i < allplayerstatus.Count; i++)
            {
                if (allplayerstatus[i].ReadyStatus == true)
                {
                    playersready = playersready + 1;
                }
                Console.WriteLine("playerready count:"+playersready.ToString());
                Console.WriteLine("list of players:"+allplayerstatus.Count);
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
            playersready = 0;

        }
 */

