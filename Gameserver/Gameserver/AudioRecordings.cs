using System;
using System.Collections.Generic;
using System.Text;

namespace Gameserver
{
   static class AudioRecordings
    {
       public static Dictionary<int,byte[]> RecoredVoiceMessages = new Dictionary<int,byte[]>();

       public static void AddvoiceRecord(byte[] message,int _fromclientKey)
        {
            try
            { 
            RecoredVoiceMessages.Add( _fromclientKey, message);
            Console.WriteLine("Added a voice message to the dictonary");

            }
            catch
            {
                Console.WriteLine("Message already exists for that player");
                //TODO: replace existing message with new one
            }
            Console.WriteLine("Recordedmessages/players " + RecoredVoiceMessages.Count + " " + Lobby.playerlist.Count);
            if(RecoredVoiceMessages.Count == Lobby.playerlist.Count) //if all players have sended a message
            {
                //toDO: send to all players that the recordings are in!
                Console.WriteLine("ladies and gentlemen... we got em!");
                ServerSend.AllmessagesRecorded();
            }
        }
    }
}
