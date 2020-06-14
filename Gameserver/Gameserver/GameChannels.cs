using System;
using System.Collections.Generic;
using System.Text;

namespace Gameserver
{
    class GameChannels
    {
        private int[] usedvalues;
        public int[] onlineplayers; //vull dit met het aantal readyplayers uit de lobby
       public GameChannels(int onlineplayercount)
        {
            onlineplayers = new int[onlineplayercount];          
        }

        void assingallplayersachannel()
        {
            //fill a list from 0 to the amount of players in order
            List<int> randomslotvalues = new List<int>(onlineplayers.Length);
            foreach (int item in onlineplayers)
            {
                randomslotvalues[item] = onlineplayers[item];
            }

        }
       public int[] getrandomvalue(int MaxValue)
        {
           bool isusedvalue = false;
            int randomvalue;
            Random random = new Random();
            usedvalues = new int[MaxValue];
            bool verschillendewaardes = false;
            int allewaardes = 0;
            while (verschillendewaardes == false)
            {
                for (int i = 0; i < usedvalues.Length; i++) //vullen van met random getallen
                {
                    randomvalue = random.Next(0, MaxValue);
                    usedvalues[i] = randomvalue;
                }

                foreach (int waarde in usedvalues) //controller van de getallen
                {
                    while (waarde == usedvalues[waarde]) //je mag niet je eigen waarde hebben 0,0 1,1 2,2 etc...
                    {
                        randomvalue = random.Next(0, MaxValue);
                        usedvalues[waarde] = randomvalue;
                    }
                }
                //https://stackoverflow.com/questions/20765589/finding-duplicate-integers-in-an-array-and-display-how-many-times-they-occurred
                var dict = new Dictionary<int, int>();

                foreach (var value in usedvalues)
                {
                    if (dict.ContainsKey(value))
                        dict[value]++;
                    else
                        dict[value] = 1;
                }
                int times = 0;
                foreach (var pair in dict)
                {
                    times = times + 1;
                //    Console.WriteLine("Value {0} occurred {1} times.", pair.Key, pair.Value);
                   
                }
                if (times == usedvalues.Length)
                {
                    Console.WriteLine($"yay {usedvalues.Length} verschillende waardes");
                    verschillendewaardes = true;
                }
                

            }
            foreach (int item in usedvalues)
            {
                Console.WriteLine(item);
            }

            return usedvalues;
        }
    }
}
