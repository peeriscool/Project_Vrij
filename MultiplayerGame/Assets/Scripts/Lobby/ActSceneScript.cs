using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSceneScript : MonoBehaviour
{
    void IsItMyTurn()
    {
        //check if it is your turn to reinact the scene and enable the listen to audio button (doesnt have to be a button tho)
        //if it is not your turn disable the PlayRecordedScene audio button
    }

    public void onbuttonclick()
    {
        //load audio from server and play it
        ClientSend.requestAudioForPlayback(Client.instance.myid);
    }

   public void additveloadscene()
    {

        //load Scene that currently being acted on
        //unload scene and move on to the next one
        //until there are no more scenes left
        //then go to the voting scene
        PlaybackLogic.LoadScenelogic();
    }
}
