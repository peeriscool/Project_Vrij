﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
public class AudioRec : MonoBehaviour
{

    float currCountdownValue;
    AudioClip myAudioClip;
    public int cliplenght;
    string ButtonText = "Record";
    bool recorded = false;
    bool menu_closed = false;
    void OnGUI()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "PlaybackScene")
        {
             recorded = false;
             menu_closed = false;
        }
        GUI.enabled = menu_closed;
        if (GUI.Button(new Rect(10, 10, 100, 50), ButtonText)) //will send: ClientSend.SendAudioBytes();
        {
            StartCoroutine(RecordWhile(cliplenght));

        }
        //GUI.enabled = recorded;
        //if (GUI.Button(new Rect(10, 140, 60, 50), "End Scene"))
        //{
        //    //TODO: what objects should be kept in the next scene?
        //    //go to scene were you see other players arms move
            
        //}
    }
    public void menuclosed ()
        {
        menu_closed = true;
        }
    public IEnumerator RecordWhile(float countdownValue)
    {
        myAudioClip = Microphone.Start(null, false, cliplenght, 44100);
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            ButtonText = currCountdownValue.ToString();
            yield return new WaitForSeconds(1);
            currCountdownValue--;
        }
        ButtonText = "Record again?!";
       Byte[] WavInBytes =  WavUtility.FromAudioClip(myAudioClip);
        SavWav.Save("audiokutzooi", myAudioClip);
        //convert in to byte[]
        //  AudioClip notmyAudioClip = WavUtility.ToAudioClip(WavInBytes); //convert byte[] in audioclip
        //--------------------------------------playback audio that was just recorded---------------------------------------------------------
        /*
        AudioClip notmyAudioClip = WavUtility.ToAudioClip(WavInBytes);
        AudioSource playaudio = GetComponent<AudioSource>();
        playaudio.clip = notmyAudioClip;
        playaudio.Play();
        */
        //    SavWav.Save("myfile", myAudioClip); 
        recorded = true;
        ClientSend.SendAudioBytes(WavInBytes);
        SceneManager.LoadScene("PlaybackScene", LoadSceneMode.Single);
    }
    
    public void ListenToAudioServer(Byte[] RecievedAudioData)
    {
        Debug.Log("Hoi ik heb byte array gevonden, nu nog omzetten in werkend geluid " + RecievedAudioData.Length);

        //code for removing bytes from the recievedAudio
        byte[] doritos = new byte[RecievedAudioData.Length - 12];
        Array.Copy(RecievedAudioData, 12, doritos, 0, doritos.Length);
        Debug.Log("skipped first 4 " + doritos.Length);
        AudioClip notmyAudioClip = WavUtility.ToAudioClip(doritos); //convert byte[] in audioclip 
        //AudioClip testng = WavUtility.ToAudioClip(Application.dataPath + "doritos.wav");
        AudioSource playaudio = GetComponent<AudioSource>();
        playaudio.clip = notmyAudioClip;
        playaudio.Play();

        //go to the next scene
       
    }
    public void ListenToAudioInternalServer(Byte[] RecievedAudioData)
    {
        Debug.Log("Hoi ik heb byte array gevonden, nu nog omzetten in werkend geluid " + RecievedAudioData.Length);
        AudioClip notmyAudioClip = WavUtility.ToAudioClip(RecievedAudioData); //convert byte[] in audioclip 
        AudioSource playaudio = GetComponent<AudioSource>();
        playaudio.clip = notmyAudioClip;
        playaudio.Play();
    }
}