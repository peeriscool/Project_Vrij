﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }
    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }
    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myid);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.PlayerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[Client.instance.myid].transform.rotation);

            SendUDPData(_packet);
        }
    }


    public static void GetListOfPlayers()
    {
        using (Packet _packet = new Packet((int)ClientPackets.GetListOfPlayers))
        {

            _packet.Write(Client.instance.myid);
            _packet.Write(UIManager.instance.usernameField.text); //maybe change to a parameter for the method?

            SendTCPData(_packet);
        }
    }

    public static void PlayerReady(bool status)
    {
        using (Packet _packet = new Packet((int)ClientPackets.PlayerReady))
        {

            // _packet.Write(Client.instance.myid);
            _packet.Write(status);

            SendTCPData(_packet);
        }
    }

    public static void SendAudioBytes(byte[] AudioData)
    {
        //Debug.Log(Client.instance.ip);
        using (Packet _packet = new Packet((int)ClientPackets.SendAudioBytes))
        {

            _packet.Write(Client.instance.myid);
            _packet.Write(AudioData);
            SendTCPData(_packet);
        }
        Debug.Log(AudioData.Length);
    }



    public static void SendEpisodeName(string episodename)
    {
        //Debug.Log(Client.instance.ip);
        using (Packet _packet = new Packet((int)ClientPackets.SendEpisodeName))
        {

            _packet.Write(Client.instance.myid);
            _packet.Write(episodename);
            SendTCPData(_packet);
        }

    }

    public static void requestAudioForPlayback(int DictonaryKey)
    {
        using (Packet _packet = new Packet((int)ClientPackets.requestAudioForPlayback))
        {
            _packet.Write(DictonaryKey);
          //  _packet.Write(Client.instance.myid);
            SendTCPData(_packet);
        }

    }

    
 public static void SendVotesToServer(int vote1,int vote2)
    {
        using (Packet _packet = new Packet((int)ClientPackets.SendVotesToServer))
        {
            _packet.Write(vote1);
            _packet.Write(vote2);
            //  _packet.Write(Client.instance.myid);
            SendTCPData(_packet);
        }

    }


    #endregion
    //private static void SendFile(Socket client, string fileName, byte[] bytes)
    //{
    //    client.SendFile(fileName, null, bytes, TransmitFileOptions.UseDefaultWorkerThread);
    //}
}