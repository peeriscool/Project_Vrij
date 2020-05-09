﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
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
        Debug.Log(readnames);

        GameObject.Find("Menu").GetComponent<UIManager>().Fillplayerlist(readnames);
    }
    public static void StartGame(Packet _packet)
    {
        Debug.Log("LET'S GO!");
        if (_packet.ReadBool() == true)
        {
            //code for starting game
            SceneManagerScript.EndLobby(true);
        }
    }
    public static void SendAudioTplayers(Packet _packet)
    {
        int length = _packet.ReadInt();
        byte[] RecievedAudio = _packet.ReadBytes(0);
        
        GameObject.Find("scriptholder").GetComponent<AudioRec>().ListenToAudioServer(RecievedAudio);
    }
}
