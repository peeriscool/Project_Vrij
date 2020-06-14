using System;
using System.Collections.Generic;
using System.Text;

namespace Gameserver
{
    static class VotingSystem
    {
        static int votecount = 0;
        static List<aVote> votes;
        public static void RecieveVotes(int a, int b)
        {
            votecount += 1;
            aVote vote = new aVote(a, b);
            votes.Add(vote);
            are_all_votesin();
        }
        public static void are_all_votesin()
        {

            if (Lobby.playerlist.Count == votes.Count)
            {
            //all votes are in
            }
        }
        private static void CountVotes()
        {
            int[] count = new int[10];
            foreach (aVote vote in votes)
            {
                //vote.vote1;
                //vote.vote2;
                //Loop through 0-all votes and count the occurances
                for (int x = 0; x < votes.Count; x++)
                {
                    for (int y = 0; y < vote.vote1; y++)
                    {
                        if (vote.vote1 == x)
                            count[x]++;
                  //      ik tiep hier express errors want hier was ik gebleven
                            //https://stackoverflow.com/questions/22995391/how-to-check-how-many-times-a-value-appears-in-an-array
                    }
                }
            }
        }
    }
   public class aVote
        {
      public int vote1;
       public int vote2;
           public aVote(int a, int b)
            {
            vote1 = a;
            vote2 = b;
            }
        }
}
