using System;
using System.Collections.Generic;
using System.Text;

namespace Gameserver
{
    static class RememberGameCode
    {
        public static int[] activegamecode
        {
            get;
            set;
        }

        public static int parseEpisodeName(int UserWhoSendEpisodeName)
        {
          
            Console.WriteLine("ActiveGameCode values by request:" + UserWhoSendEpisodeName);
            //should return the Id of the player you wrote a episode name for
            foreach (int item in activegamecode)
            {
                Console.WriteLine(item);
            }
            int sendbackvalue = activegamecode[UserWhoSendEpisodeName-1]; //-1 want int[] start op 0
            Console.WriteLine("Recieved episode name for player " + sendbackvalue );
            return sendbackvalue+1; //+1 want client id is nooit 0
        }
        public static int activegamecodenumberswitch(int requestedvalue)
        {
            int sendback = activegamecode[requestedvalue];
            return sendback;
        }


       
    }
}
