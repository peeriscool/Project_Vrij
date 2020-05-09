using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System;

public class SendFilesUsingTcp : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            sendingClass a = new sendingClass();
            a.btnSend_Click();
        }
        
    }
    public partial class sendingClass
    {
        public string SendingFilePath = string.Empty;
        private const int BufferSize = 1024;
        //string ip = "192.168.2.2";
        // int port = 29250;
        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    progressBar1.Visible = true;
        //    progressBar1.Minimum = 1;
        //    progressBar1.Value = 1;
        //    progressBar1.Step = 1;

        //}

        //private void btnBrowse_Click(object sender, EventArgs e) //opens a dialog for choosing files
        //{
        //    OpenFileDialog Dlg = new OpenFileDialog();
        //    Dlg.Filter = "All Files (*.*)|*.*";
        //    Dlg.CheckFileExists = true;
        //    Dlg.Title = "Choose a File";
        //    Dlg.InitialDirectory = @"C:\";
        //    if (Dlg.ShowDialog() == DialogResult.OK)
        //    {
        //        SendingFilePath = Dlg.FileName;

        //    }
        //}
        private void SelectFileToSend() //send a specific file
        {
            SendingFilePath = Application.dataPath + "\name.txt";
        }

        public void btnSend_Click()
        {
            SelectFileToSend();
            if (SendingFilePath != string.Empty)
            {
                SendTCP(SendingFilePath, Client.instance.ip, Client.instance.port); //replaced form code
            }
            else
                Debug.Log("NO file Found");
        }
        public void SendTCP(string M, string IPA, Int32 PortN)
        {
            byte[] SendingBuffer = null;
            TcpClient client = null;
            //lblStatus.Text = "";
            NetworkStream netstream = null;
            try
            {
                // client = new TcpClient(IPA, PortN);
                client = Client.instance.tcp.socket;
                Debug.Log("Connected to the Server...\n");
                netstream = client.GetStream();
                FileStream Fs = new FileStream(M, FileMode.Open, FileAccess.Read);
                int NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Fs.Length) / Convert.ToDouble(BufferSize)));
               // progressBar1.Maximum = NoOfPackets;
                int TotalLength = (int)Fs.Length, CurrentPacketLength, counter = 0;
                for (int i = 0; i < NoOfPackets; i++)
                {
                    if (TotalLength > BufferSize)
                    {
                        CurrentPacketLength = BufferSize;
                        TotalLength = TotalLength - CurrentPacketLength;
                    }
                    else
                        CurrentPacketLength = TotalLength;
                    SendingBuffer = new byte[CurrentPacketLength];
                    Fs.Read(SendingBuffer, 0, CurrentPacketLength);
                    netstream.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                    //if (progressBar1.Value >= progressBar1.Maximum)
                    //    progressBar1.Value = progressBar1.Minimum;
                    //progressBar1.PerformStep();
                }

                Debug.Log("Connected to the Server...\n" + "Sent " + Fs.Length.ToString() + " bytes to the server");
                Fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                netstream.Close();
                client.Close();

            }
        }


    }
}
