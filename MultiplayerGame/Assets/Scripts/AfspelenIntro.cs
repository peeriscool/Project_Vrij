using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfspelenIntro : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip Intro;

    public void jemoeder()
    {
        StartCoroutine(playaudio());
    }
    public void Update()
    {
        
        {
           // if (AudioSource.isPlaying == false)
        //    {

         //   }
        }
    }
    IEnumerator playaudio()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = Intro;
        audio.Play();
        
    }
}
