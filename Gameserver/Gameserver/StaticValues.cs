using System;
using System.Collections.Generic;
using System.Text;

namespace Gameserver
{
   public class StaticValues
    {

        public static Dictionary <int,string> episodenames = new Dictionary<int,string>();
        
        
        public static void giveepisodename(int _fromclient,string name)
        {
            episodenames.Add(_fromclient,name);

        }
        public static string getname(int request)
        {
            return episodenames[request];

        }
    }
}
