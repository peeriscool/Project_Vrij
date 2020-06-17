using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audiostay : MonoBehaviour
{
    public GameObject music;
    public AudioSource audio;

    public void ButtonClick()
    {
        DontDestroyOnLoad(music.gameObject);
        audio = GetComponent<AudioSource>();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Lobby")
        {
            Destroy(music);
        }
    }

    public void PlayMusic()
    {
        if (audio.isPlaying)
        {
            return;
            audio.Play();
        }
    }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Audio");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    

}
