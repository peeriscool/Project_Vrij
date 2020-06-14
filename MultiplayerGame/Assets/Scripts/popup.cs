using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class popup : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;

    Canvas canvas_episodeNamecontrol;
    public void Start()
    {
        //if popup already loaded and current scene == playbackscene
        //dont show canvas
        //  Scene currentScene = SceneManager.GetActiveScene();
        //  if (currentScene.name == "PlaybackScene")
        //{
        //    canvas.SetActive(false);
        //}
        canvas_episodeNamecontrol = GameObject.Find("canvas_episodeNamecontrol").GetComponent<Canvas>();
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
    public void Onclick(GameObject sender)
    {
        Debug.Log("hide menu");
        sender.SetActive(false);
    }
    private void Update()
    {
        if(UserDataAcrossScenes.ContinueButton)
        {
            this.gameObject.GetComponent<Button>().interactable = true;
        }
    }
}
