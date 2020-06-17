using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

public static class UserDataAcrossScenes
{
   // public static string Episodename;
    public static string Episodename
    {
        get;
        set;
    }
    public static string Episodenamefordisplay
    {
        get;
        set;
    }
    public static string[] Channelnames = new string[]
    {
         "Cookingshow",
         "Talkshow",
         "teleshopping",
         "Cookingshow",
         "Talkshow",
         "teleshopping",
         "Cookingshow",
         "Talkshow",
         "teleshopping",

    };
   public static int PlayerSceneValue
    {
        get;
        set;
    }

    public static int[] gamecode
    {
        get;
        set;
        
    }

    public static string getchannelnamewithgamecode(int playercode)
    {
        Debug.Log(gamecode[playercode-1] + "dit is het getal dat de speler de scene moet invullen en niet zijn eigen id dus");
        return Channelnames[playercode-1];
    }
    public static string findyourscene(int playercode)
    {
        Debug.Log(gamecode[playercode - 1] + "dit is het getal dat de speler de scene moet invullen en niet zijn eigen id dus");
        //return Channelnames[playercode - 1];
        foreach(int i in gamecode)
        {
            if (playercode-1 == gamecode[i])
            {
                //thats your scene
                return Channelnames[i];
            }
        }
        return "StartMenu"; //als je index niet bestaat ben je aan het hacken dus stuur ik je terug naar de menu hihi
    }

    public static string SendEpisodeNameToCorrectPlayer(string episodename, int playercode)
    {
        //clientsend.methode die de string geeft aan de juiste player in de correcte scene
        return "hier moet nog code neef";
    }
    //---------------------------------------------------------------------------------

    public static bool ContinueButton
    {
        get;
        set;
    }


    public static bool Recordinghasbeen
    {
        get;
        set;
    }

    public static List<int> Votes
    {
        get;
        set;
    }

    public static string Playernames
    {
        get;
        set;
    }
}
