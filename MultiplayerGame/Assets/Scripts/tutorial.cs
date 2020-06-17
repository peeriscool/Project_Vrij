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

    void Start()
    {
        tutorialframe.sprite = sprites[index];

    }

    // Update is called once per frame
    public void tutorialbuttonclicked()
    {
        tutorialframe.sprite = sprites[index];
        index += 1;
        if(index == sprites.Length)
        {
            index = 0;
            //go to let's plan whats on tv today! scene
            DontDestroyOnLoad(GameObject.Find("ClientManager"));
            SceneManager.LoadScene("EpisodeNamingScene", LoadSceneMode.Single);
        }
    }

    //foreach (Image image in images)
    //{
    //    //if (image.gameObject.CompareTag("face"))
    //    //    face = image;
    //}
}

