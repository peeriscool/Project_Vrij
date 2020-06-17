﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.Linq;
public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message From server: {_msg}");
        Client.instance.myid = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }
    public static void newplayerjoined(Packet _packet)
    {
        int _joinedId = _packet.ReadInt();

        Debug.Log("we got a new player!");
        GameObject.Find("Menu").GetComponent<UIManager>().recievednewplayer();
    }
   
    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        //      GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation); //actualy spawn players
    }
    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        //       GameManager.players[_id].transform.position = _position;
    }
    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        //       GameManager.players[_id].transform.rotation = _rotation;
    }
    public static void SendPlayerNames(Packet _packet)
    {
        // int _id = _packet.ReadInt();
        string readnames = _packet.ReadString();
        int playercount = _packet.ReadInt();
        Debug.Log(readnames);

        GameObject.Find("Menu").GetComponent<UIManager>().Fillplayerlist(readnames, playercount);
        UserDataAcrossScenes.Playernames= readnames;

    }
    public static void StartGame(Packet _packet)
    {
        Debug.Log("LET'S GO!");
        if (_packet.ReadBool() == true)
        {
            //code for starting game
          //  SceneManagerScript.EndLobby(true);
            GameMenuScript endlobby = new GameMenuScript();
            endlobby.EndLobby(true);
        }
    }
    public static void SendAudioToPlayers(Packet _packet)
    {
        byte[] RecievedAudio = _packet.ToArray();
        Debug.Log(RecievedAudio.Length);
        GameObject.Find("ScriptHolder_InGame").GetComponent<AudioRec>().ListenToAudioServer(RecievedAudio);

        //AudioClip notmyAudioClip = WavUtility.ToAudioClip(RecievedAudio);
        // byte[] correctlength = RecievedAudio.Skip(4).ToArray();
        //   System.IO.File.WriteAllBytes("yourfilepath.wav", RecievedAudio);
        //AudioClip notmyAudioClip = WavUtility.ToAudioClip(correctlength);
        //// SavWav.Save("Recordingfile", notmyAudioClip);
        //   AudioSource playaudio = new AudioSource();
        //   playaudio.clip = notmyAudioClip;
        //   playaudio.Play();
        // //GameObject audio = GameObject.FindGameObjectWithTag("Audio");
        // //audio.GetComponent<AudioRec>().ListenToAudioServer(RecievedAudio);
    }

    public static void GameCode(Packet _packet)
    {
        string gamecode = _packet.ReadString();
        int myInt;
        int[] array = gamecode.ToCharArray().Where(x =>
        int.TryParse(x.ToString(), out myInt)).Select(x =>
        int.Parse(x.ToString())).ToArray();


        UserDataAcrossScenes.gamecode = array;
        foreach (int i in UserDataAcrossScenes.gamecode)
        {
            Debug.Log(i);
        }
    }


    public static void SendEpisodeNameBack(Packet _packet) //should only be recieved by one user
    {
        string MyEpisodeName = _packet.ReadString();
        Debug.Log(MyEpisodeName + "dit is de naam voor jouw scene!");
        UserDataAcrossScenes.Episodename = MyEpisodeName;
        // GameObject.Find("Main Camera").AddComponent<ListenToEpisodeName>();

    }
    public static void ContinueToPlayback(Packet _packet) //AllmessagesRecorded on server side
    {
        bool recieved = _packet.ReadBool();
        UserDataAcrossScenes.ContinueButton = true;
        PlaybackLogic.LoadScenelogic(); //will load the first acting scene
        GameObject.Find("waitingforplayerscanvas").GetComponent<popup>().Onclick(GameObject.Find("waitingforplayerscanvas"));
        Countdowntonextact current = GameObject.Find("Scriptholder_ActLogic").GetComponent<Countdowntonextact>();
        current.StartCountdown(40); //will start the countdown for the playback
    }

    public static void RecievedVotes(Packet _packet)
    {
        List<int> votes = new List<int>();
        int length = _packet.ReadInt();
        for (int i = 0; i < length; i++)
        {
            votes.Add(_packet.ReadInt());
        }
        //load voting complete scene
        GameObject.Find("Buttonscriptholder").GetComponent<VotingSystem>().reciveAllvotes(votes);
        //Send votes to scene
        //Displayvotes.Displayvotes(votes);

    }
    public static void Namerecieved(Packet _packet)
    {
        string name = _packet.ReadString();
        UserDataAcrossScenes.Episodenamefordisplay = name;
    }
}
