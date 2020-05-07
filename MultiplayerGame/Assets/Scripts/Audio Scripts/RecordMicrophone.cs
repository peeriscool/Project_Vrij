﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RecordMicrophone : MonoBehaviour
{
    float currCountdownValue;
    bool isRecording = true;
    private AudioSource audioSource;
    //temporary audio vector we write to every second while recording is enabled..
     List<float> tempRecording = new List<float>();

    //list of recorded clips...
    public List<float[]> recordedClips = new List<float[]>();


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //set up recording to last a max of x seconds and loop over and over
        audioSource.clip = Microphone.Start(null, true, 8, 44100);
        //audioSource.Play();
        //resize our temporary vector every second
        // Invoke("ResizeRecording", 1);
    }
    void ResizeRecording()
    {
        if (isRecording)
        {
            //add the next second of recorded audio to temp vector
            int length = 44100;
            float[] clipData = new float[length];
            audioSource.clip.GetData(clipData, 0);
            tempRecording.AddRange(clipData);
            Invoke("ResizeRecording", 1);
        }
    }
    public void onclickstart()
    {
     //   int time = (int)Time.deltaTime;
      //  int cliplenght = time;
      
        audioSource.Play();
        StartCountdown(1);  
    }
    public void onclicksstop() { audioSource.Stop(); StartCountdown(2); }

    void SwitchClips(int index)
    {
        if (index < recordedClips.Count)
        {
            audioSource.Stop();
            int length = recordedClips[index].Length;
            audioSource.clip = AudioClip.Create("recorded samples", length, 1, 44100, false);
            audioSource.clip.SetData(recordedClips[index], 0);
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void RecordVoice()
    {

        isRecording = !isRecording;
        Debug.Log(isRecording == true ? "Is Recording" : "Off");

        if (isRecording == false)
        {
            //stop recording, get length, create a new array of samples
            int length = Microphone.GetPosition(null);

            Microphone.End(null);
            float[] clipData = new float[length];
            audioSource.clip.GetData(clipData, 0);

            //create a larger vector that will have enough space to hold our temporary
            //recording, and the last section of the current recording
            float[] fullClip = new float[clipData.Length + tempRecording.Count];
            for (int i = 0; i < fullClip.Length; i++)
            {
                //write data all recorded data to fullCLip vector
                if (i < tempRecording.Count)
                    fullClip[i] = tempRecording[i];
                else
                    fullClip[i] = clipData[i - tempRecording.Count];
            }

            recordedClips.Add(fullClip);
            audioSource.clip = AudioClip.Create("recorded samples", fullClip.Length, 1, 44100, false);
            audioSource.clip.SetData(fullClip, 0);


        }
        else
        {
            //stop audio playback and start new recording...
            audioSource.Stop();
            tempRecording.Clear();
            Microphone.End(null);
            audioSource.clip = Microphone.Start(null, true, 1, 44100);
            Invoke("ResizeRecording", 1);
        }
    }
    public IEnumerator StartCountdown(float countdownValue)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(countdownValue);
            currCountdownValue--;
        }
        Debug.Log("TimerDone");
        audioSource.Stop();
    }

}
