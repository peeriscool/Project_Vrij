using System;
using System.Collections.Generic;

namespace Gameserver
{
    static class Lobby
    {
        public static List<ReadyPlayer> playerlist = new List<ReadyPlayer>(); //list of playerstatus
       // static List<ReadyPlayer> readyplayer = new List<ReadyPlayer>(); //list of playerstatus
        static int playersready = 0;
        static int minimalplayers = 1; //Fix!: 0 is actualy 1 players : 0 and 1
        static bool doesplayerexist;
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
      
        public static void RecievePlayerStatus(int _fromClient, bool status)
        {  
            Console.WriteLine("player " + _fromClient + " status: "+ status);
            Lobby.PlayerStatus(_fromClient, status);
        }


        static void PlayerStatus(int fromClient, bool _status)
        {
            ReadyPlayer activerequest = new ReadyPlayer(fromClient, _status);
            activerequest.NatoName = NATO[fromClient-1];

           //if(playerlist == null) //first player
           //{
           //    playerlist.Add(activerequest);
           //}

            doesplayerexist = false;


                foreach (ReadyPlayer playercheck in playerlist) 
                {
                    if(playercheck.NatoName == activerequest.NatoName) //check if a nato name is equal to any index of players
                    {
                        //known user
                        doesplayerexist = true;
                        //check for change in status
                        
                    }
                   
                }
           
            if (doesplayerexist == false)
                {
                    playerlist.Add(activerequest);
                    Console.WriteLine(activerequest.NatoName + " " + fromClient + "signed in");
                }
              

            if (doesplayerexist == true)
            {
                foreach (var item in playerlist)
                {
                    Console.WriteLine(item + " Listed display before the questionable code");
                }
                if (activerequest.ReadyStatus != playerlist[fromClient - 1].ReadyStatus) //control if the status was changed
                {
                    Console.WriteLine("changed status");
                    playerlist[fromClient - 1].ReadyStatus = activerequest.ReadyStatus;
                }
                
            }


            int playersreadyint = 0;
            foreach (ReadyPlayer item in playerlist)
            {
                if(item.ReadyStatus == true)
                {
                    playersreadyint += 1;
                }
            }
            Console.WriteLine(playersreadyint + "players are ready of the " + playerlist.Count);


            if (playersreadyint == playerlist.Count) //value is bugging with multiple players (keeps adding more ready players)
            {
                if (playersreadyint == 1)
                {
                    Console.WriteLine("to few players");
                }
                else
                {
                    Console.WriteLine("All Players are ready!");
                    //send message to all players that the game can begin
                    ServerSend.StartGame();
                    //ToDo : clear lobby after game starts


                    //assign values to for determing who gets what scene
                    GameChannels test = new GameChannels(1);
                    int[] gamecode = test.getrandomvalue(playerlist.Count);
                    RememberGameCode.activegamecode = gamecode;
                    ServerSend.GameCode(gamecode);
                    foreach (int item in gamecode)
                    {
                        Console.WriteLine(item);
                    }
                    //send message to all players that the game can begin
                    ServerSend.StartGame();        // (allplayers,ready);
                }
            }
            playersreadyint = 0;



        }   
        static public void RemovePlayer(int _fromClient)
        {
            playerlist.RemoveAt(_fromClient - 1);
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




/*
 * old code 
 * using System;
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





/*
 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 *    static List<ReadyPlayer> allplayerstatus = new List<ReadyPlayer>(); //list of playerstatus
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
                    allplayerstatus[PlayerIndex].ReadyStatus = activerequest.ReadyStatus;
                }
                if (allplayerstatus[PlayerIndex].ReadyStatus == false)
                {
                    if (playersready > 0)
                    { 
                    playersready -= 1;
                    }
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
        int backuupplayersready = playersready;
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
            if (playersready <= minimalplayers)
            {
                Console.WriteLine("to few players" + playersready);
            }
            else if(allplayerstatus.Count == playersready && activerequest.ReadyStatus == true)
            {
             Console.WriteLine("All Players are ready!");
                //assign values to for determing who gets what scene
                GameChannels test = new GameChannels(1);
                int[] gamecode = test.getrandomvalue(allplayerstatus.Count);
                ServerSend.GameCode(gamecode);
                foreach (var item in gamecode)
                {
                    Console.WriteLine(item);
                }
                //send message to all players that the game can begin
                ServerSend.StartGame();        // (allplayers,ready);
            }
        }
        playersready = backuupplayersready;
    }
    static public void RemovePlayer(int _fromClient)
    {
        allplayerstatus.RemoveAt(_fromClient - 1);
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
*/