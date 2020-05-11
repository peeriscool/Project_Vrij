using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
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
        ClientSend.SendAudioBytes(WavInBytes);
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
    }
  
   // void saveFloatArrayToFile(float[] data)
    //{
    //    BinaryWriter writer = new BinaryWriter(File.Open("name.txt", FileMode.Create));
       
    //    foreach (float item in data)
    //    {
    //        writer.Write(item);
    //    }
    //    ReadOutWavefile();
    //}
    //void ReadOutWavefile()
    //{
    //  AudioClip testng =  WavUtility.ToAudioClip(Application.dataPath +"\name.txt");
    //    AudioSource audio = GetComponent<AudioSource>();
    //    audio.clip = testng;
    //    audio.Play();
    //}

    //void WriteTOByte(byte [] rawData)
    //{

    //    WAV wav = new WAV(rawData);
    //    Debug.Log(wav);
    //    AudioClip audioClip = AudioClip.Create("testSound", wav.SampleCount, 1, wav.Frequency, false, false);
    //    audioClip.SetData(wav.LeftChannel, 0);
    //    GetComponent<AudioSource>().clip = audioClip;
    //    GetComponent<AudioSource>().Play();
    //}
}