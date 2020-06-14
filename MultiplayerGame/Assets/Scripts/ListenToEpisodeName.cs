using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ListenToEpisodeName : MonoBehaviour
{
    public Text EpisodeName;
    public Canvas canvas_episodeNamecontrol;
    void Start()
    {
        EpisodeName.text = UserDataAcrossScenes.Episodename;
    }

    // Update is called once per frame
    //public void assignepisodenametotext()
    //{
    //    EpisodeName.text = UserDataAcrossScenes.Episodename;
    //}

   public void hasrecrodingtakenplace()
    {
        if (UserDataAcrossScenes.Recordinghasbeen == false) //recording has not taken place
        {
            canvas_episodeNamecontrol.GetComponent<Canvas>().enabled = true;
            UserDataAcrossScenes.Recordinghasbeen = true;
        }
        else //recording has taken place
        {
            canvas_episodeNamecontrol.GetComponent<Canvas>().enabled = false;
        }
    }
   

}
