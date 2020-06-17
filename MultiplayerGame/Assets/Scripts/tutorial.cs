using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public Image tutorialframe;
    public Sprite[] sprites;
    int index = 0;

    public AudioSource tut1;
    public AudioSource tut2;
    public AudioSource tut3;
    public AudioSource tut4;
    public AudioSource tut5;
    public AudioSource music;
    


    void Start()
    {
        tutorialframe.sprite = sprites[index];

    }

    // Update is called once per frame
    public void tutorialbuttonclicked()
    {  
        index += 1;
        tutorialframe.sprite = sprites[index];
      
        if(index == sprites.Length)
        {
            index = 0;
            //go to let's plan whats on tv today! scene
            DontDestroyOnLoad(GameObject.Find("ClientManager"));
            SceneManager.LoadScene("EpisodeNamingScene", LoadSceneMode.Single);
            tut5.Stop();
        }

        if (index == 6)
        {
            tut1.Play();
            music.volume = 0.3f;
        }
        if (index == 7)
        {
            tut1.Stop();
            tut2.Play();
        }
        if (index == 8)
        {
            tut2.Stop();
            tut3.Play();
        }
        if (index == 9)
        {
            tut3.Stop();
            tut4.Play();
        }
        if (index == 10)
        {
            tut4.Stop();
            tut5.Play();
        }
    }

    //foreach (Image image in images)
    //{
    //    //if (image.gameObject.CompareTag("face"))
    //    //    face = image;
    //}
}

