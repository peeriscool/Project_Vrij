using System;
using System.Threading;
namespace Gameserver
{
    class Program
    {
        private static Boolean isRunning = false;
        static void Main(string[] args)
        {
            Console.WriteLine("Game server");
            isRunning = true;

            Thread mainThread = new Thread (new ThreadStart(MainThread));
            mainThread.Start();
            Server.start(8,26950); //maxplayer and port

           ReceiveFiles instance = new ReceiveFiles();
            instance.Filetoserver();
        }
        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();
                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}
