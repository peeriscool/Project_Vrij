using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;
//Use the PointerDown and PointerUP interfaces to detect a mouse down and up on a ui element
public class RecordWhilepressed : MonoBehaviour
{
    AudioClip recording;
    //Keep this one as a global variable (outside the functions) too and use GetComponent during start to save resources
    AudioSource audioSource;
    private float startRecordingTime;
    //Get the audiosource here to save resources
    //Get the audiosource here to save resources
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            OnPointerDown();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            OnPointerUp();
        }
    }
    public void OnPointerUp()
    {
        //End the recording when the keypressed comes back up, then play it
        Microphone.End("");

        //Trim the audioclip by the length of the recording
        AudioClip recordingNew = AudioClip.Create(recording.name, (int)((Time.time - startRecordingTime) * recording.frequency), recording.channels, recording.frequency, false);
        float[] data = new float[(int)((Time.time - startRecordingTime) * recording.frequency)];
        recording.GetData(data, 0);
        recordingNew.SetData(data, 0);
        this.recording = recordingNew;

        // Play recording
        audioSource.clip = recording;
        audioSource.Play();

        //send recording
        Byte[] WavInBytes = WavUtility.FromAudioClip(recording);
        ClientSend.SendAudioBytes(WavInBytes);
    }

    public void OnPointerDown()
    {
        //Get the max frequency of a microphone, if it's less than 44100 record at the max frequency, else record at 44100
        int minFreq;
        int maxFreq;
        int freq = 44100;
        Microphone.GetDeviceCaps("", out minFreq, out maxFreq);
        if (maxFreq < 44100)
            freq = maxFreq;

        //Start the recording, the length of 300 gives it a cap of 5 minutes
        recording = Microphone.Start("", false, 60, 44100);
        startRecordingTime = Time.time;


        // thank you : https://answers.unity.com/questions/299008/how-do-i-get-audio-data-from-microphone-in-realtim.html
        AudioSource aud = GetComponent<AudioSource>();
        aud.clip = recording;
        aud.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        aud.Play();

        // Byte[] WavInBytes = WavUtility.FromAudioClip(recording);
        // ClientSend.SendAudioBytes(WavInBytes);

    }  
    
    //private IEnumerator coroutineA()
    //{
    //    // wait for 1 second
    //    Debug.Log("coroutineA created");
    //  //  yield return new WaitForSeconds(1.0f);
    //    yield return StartCoroutine(coroutineB());
    //    Debug.Log("done");

    //}

    //private IEnumerator sendingbitchesajjj()
    //{
    //    Debug.Log("coroutineA created");
    //    //Get the max frequency of a microphone, if it's less than 44100 record at the max frequency, else record at 44100
    //    int minFreq;
    //    int maxFreq;
    //    int freq = 44100;
    //    Microphone.GetDeviceCaps("", out minFreq, out maxFreq);
    //    if (maxFreq < 44100)
    //        freq = maxFreq;

    //    //Start the recording, the length of 300 gives it a cap of 5 minutes
    //    recording = Microphone.Start("", false, 60, 44100);
    //    startRecordingTime = Time.time;

    //   yield return StartCoroutine(coroutineB()); ;
    //    Debug.Log("done");
    //    //End the recording when the keypressed comes back up, then play it
    //    Microphone.End("");

    //    //Trim the audioclip by the length of the recording
    //    AudioClip recordingNew = AudioClip.Create(recording.name, (int)((Time.time - startRecordingTime) * recording.frequency), recording.channels, recording.frequency, false);
    //    float[] data = new float[(int)((Time.time - startRecordingTime) * recording.frequency)];
    //    recording.GetData(data, 0);
    //    recordingNew.SetData(data, 0);
    //    this.recording = recordingNew;

    //    // Play recording
    //    audioSource.clip = recording;
    //    audioSource.Play();

    //    //send recording
    //    Byte[] WavInBytes = WavUtility.FromAudioClip(recording);
    //    ClientSend.SendAudioBytes(WavInBytes);
    //    // thank you : https://answers.unity.com/questions/299008/how-do-i-get-audio-data-from-microphone-in-realtim.html
    //    //AudioSource aud = GetComponent<AudioSource>();
    //    //aud.clip = recording;
    //    //aud.loop = true;
    //    //while (!(Microphone.GetPosition(null) > 0)) { }
    //    // aud.Play();


    //    // ClientSend.SendAudioBytes(WavInBytes);

    //}
    //IEnumerator coroutineB()
    //{
    //        while (!Input.GetKeyDown(KeyCode.S))
    //            yield return null;

    //    print("Word Typing Ended");
    //}

}