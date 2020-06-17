using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class displayepisodename : MonoBehaviour
{

    public Text displayname;
    // Update is called once per frame
    void Update()
    {
        if (UserDataAcrossScenes.Episodenamefordisplay != null)
        {
            displayname.text = UserDataAcrossScenes.Episodenamefordisplay;
        }
    }
}
