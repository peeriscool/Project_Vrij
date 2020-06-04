using System.Collections;
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
    void Start() { }
    void Update()
    {
        //if (Input.GetKeyDown("a"))
        //{
        //    //start recording
        //    RecordWhiledicksa(cliplenght, true);
        //}
        //if(Input.GetKeyUp("a"))
        //{
        //    //stop recording
        //    RecordWhiledicksa(cliplenght, false);
        //}
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), ButtonText))
        {

            StartCoroutine(RecordWhile(cliplenght));
        }
        if (GUI.Button(new Rect(10, 70, 60, 50), "Save"))
        {
            SavWav.Save("myfile", myAudioClip);

            
        }
        if (GUI.Button(new Rect(10, 140, 60, 50), "End Scene"))
        {
            //TODO: what objects should be kept in the next scene?
            SceneManager.LoadScene("PlaybackScene", LoadSceneMode.Single); //go to scene were you see other players arms move


        }
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
        ClientSend.SendAudioBytes(WavInBytes);
    }
    //public void RecordWhiledicksa(float countdownValue, bool status)
    //{
    //    while (status)
    //    {
    //        myAudioClip = Microphone.Start(null, false, cliplenght, 44100);
    //        currCountdownValue = countdownValue;
    //        while (currCountdownValue > 0)
    //      //  {
    //            Debug.Log("Countdown: " + currCountdownValue);
    //           // yield return new WaitForSeconds(1);
    //        //    currCountdownValue--;
    //       // }
            
    //    }
    //    Debug.Log("TimerDone");
    //    Byte[] WavInBytes = WavUtility.FromAudioClip(myAudioClip);
    //    //  ClientSend.SendAudioBytes(WavInBytes);
    //    AudioClip notmyAudioClip = WavUtility.ToAudioClip(WavInBytes);
    //    AudioSource playaudio = GetComponent<AudioSource>();
    //    playaudio.clip = notmyAudioClip;
    //    playaudio.Play();
    //}

    //convert in to byte[]
    //  AudioClip notmyAudioClip = WavUtility.ToAudioClip(WavInBytes); //convert byte[] in audioclip
    //--------------------------------------playback audio that was just recorded---------------------------------------------------------
    // /*


    //  */
    // ClientSend.SendAudioBytes(WavInBytes);

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
    public void ListenToAudioInternalServer(Byte[] RecievedAudioData)
    {
        Debug.Log("Hoi ik heb byte array gevonden, nu nog omzetten in werkend geluid " + RecievedAudioData.Length);
        AudioClip notmyAudioClip = WavUtility.ToAudioClip(RecievedAudioData); //convert byte[] in audioclip 
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