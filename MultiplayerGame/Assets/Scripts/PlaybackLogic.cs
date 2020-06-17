using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class PlaybackLogic
{
    public static int CurrentSceneIndex = 1;
    // we waitin for a serverhanddle message
    // that will tell that wich scene we can additve load
    public static void LoadScenelogic()
    {
       
        //how to get all players currently playing?
        //all players is known through the lenght of the "gamecode"
      int[] allplayers =  UserDataAcrossScenes.gamecode;
        if (CurrentSceneIndex > allplayers.Length)
        {
            //all players should have acted out there scene
            //go to voting scene
            SceneManager.LoadScene("Vote");
        }
        //get correct value with UserDataAcrossScenes.Channelnames based on gamecode index
        //one by one until all the scenes from the active players have been played


        GameObject.DontDestroyOnLoad(GameObject.Find("ClientManager"));
        //if (CurrentSceneIndex == 0)
        //{
        //    //load tv shows have been planned
        //}

        if (CurrentSceneIndex > 1) { SceneManager.UnloadSceneAsync(UserDataAcrossScenes.getchannelnamewithgamecode(CurrentSceneIndex - 1) + "_playbackversion"); }
            SceneManager.LoadScene(UserDataAcrossScenes.getchannelnamewithgamecode(CurrentSceneIndex)+"_playbackversion",LoadSceneMode.Additive);
        //gotta play audio for that scene
      // int getaudio = UserDataAcrossScenes.gamecode[CurrentSceneIndex-1]; //will give back the number assoicated with the gamecode
            ClientSend.requestAudioForPlayback(CurrentSceneIndex);
            //up CurrentSceneIndex for next scene
            CurrentSceneIndex +=1;

        //wait for the scene to be over then reload LoadScenelogic()
        try
        {
            GameObject.Find("Scriptholder_ActLogic").GetComponent<Countdowntonextact>().StartCountdown(40);
        }
        catch (System.Exception)
        {
            //no scenes left or Scriptholder_ActLogic is missing
            throw;
        }
     
    }
}
