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
            Console.WriteLine("Recieved a vote");

            if (votes == null)
            {
                votes = new List<aVote>();
            }

            votecount += 1; //amount of votes
            aVote vote = new aVote(a, b); //make a vote
            votes.Add(vote); //add vote to list of votes
            are_all_votesin(); //control votes
        }
        public static void are_all_votesin()
        {
            Console.WriteLine(Lobby.playerlist.Count + " playercount/votecount " + votes.Count);
            if (Lobby.playerlist.Count == votes.Count)
            {
                //all votes are in
                CountVotes();
            }
        }
        private static void CountVotes() //logic for who gets what vote
        {
           
            List<int> values = new List<int>();
            foreach (aVote item in votes) //get all votes and put them in a complete list
            {
                values.Add(item.vote1);
                values.Add(item.vote2);
                
            }
          //  List<int> result = new List<int>();
           // int index = 0;
          //  int amountofvotes = 0;
          //  List<int> coppiedvalues = values;

            //while (amountofvotes != values.Count)
            //{
            //    Console.WriteLine("while is true" + amountofvotes + "/" + values.Count);


            //    //forloop crashes server
               

            //        foreach (int vote in coppiedvalues)
            //        {

            //            if (vote == index) //found an integer equal to index
            //            {

            //                amountofvotes += 1;
            //                //add turf to 
            //                  // values.Remove(vote);
            //                result.Add(index);
            //            }
            //        }
            //        index = +1;
            //        }
            //all votes should be saved in results
            //send result to clients
            Console.WriteLine("The voting is complete!!!");
            Console.WriteLine(values);

            ServerSend.SendVotingResults(values);
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
