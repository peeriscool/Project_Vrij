using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class AudioRec : MonoBehaviour
{

    float currCountdownValue;
    AudioClip myAudioClip;
    public int cliplenght;
    void Start() { }
    void Update() { }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 60, 50), "Record"))
        {

            StartCoroutine(RecordWhile(cliplenght));
        }
        if (GUI.Button(new Rect(10, 70, 60, 50), "Save"))
        {
            SavWav.Save("myfile", myAudioClip);

            //        audio.Play();
        }
    }

    public IEnumerator RecordWhile(float countdownValue)
    {
        myAudioClip = Microphone.Start(null, false, cliplenght, 44100);
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1);
            currCountdownValue--;
        }
        Debug.Log("TimerDone");
        MakeFloatArray();
    }

   // experemental code of converting.wav to a float array
    void MakeFloatArray()
    {
        // AudioSource audioSource = GetComponent<AudioSource>();
        float[] samples = new float[myAudioClip.samples * myAudioClip.channels];
        myAudioClip.GetData(samples, 0);

        for (int i = 0; i < samples.Length; ++i)
        {
            samples[i] = samples[i] * 0.5f;
        }

        myAudioClip.SetData(samples, 0);


        //var byteArray = new byte[samples.Length * 4];
       // Buffer.BlockCopy(samples, 0, byteArray, 0, byteArray.Length);

        saveFloatArrayToFile(samples);
    }
    void saveFloatArrayToFile(float[] data)
    {
        BinaryWriter writer = new BinaryWriter(File.Open("name.txt", FileMode.Create));
       
        foreach (float item in data)
        {
            writer.Write(item);
        }
        ReadOutWavefile();
    }
    void ReadOutWavefile()
    {
      AudioClip testng =  WavUtility.ToAudioClip(Application.dataPath +"\name.txt");
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = testng;
        audio.Play();
    }
}