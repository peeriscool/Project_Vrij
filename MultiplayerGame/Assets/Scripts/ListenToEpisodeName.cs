using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ListenToEpisodeName : MonoBehaviour
{
    public Text EpisodeName;

    void Start()
    {
        EpisodeName.text = UserDataAcrossScenes.Episodename;
    }

    // Update is called once per frame
    //public void assignepisodenametotext()
    //{
    //    EpisodeName.text = UserDataAcrossScenes.Episodename;
    //}
}
