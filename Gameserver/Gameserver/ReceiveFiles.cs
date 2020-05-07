using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
//using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Gameserver
{
        public partial class ReceiveFiles
        {
            private const int BufferSize = 1024;
            public string Status = string.Empty;
            public Thread T = null;
            //public Form1()
            //{
            //    InitializeComponent();
            //}

            public void Filetoserver()
            {
              Console.WriteLine( "Server is open for file recieving");
                ThreadStart Ts = new ThreadStart(StartReceiving);
                T = new Thread(Ts);
                T.SetApartmentState(ApartmentState.STA);

                T.Start();
            }
            public void StartReceiving()
            {
                ReceiveTCP(26950);
            }


            public void ReceiveTCP(int portN)
            {
                TcpListener Listener = null;
                try
                {
                    Listener = new TcpListener(IPAddress.Any, portN);
                    Console.WriteLine(Listener.LocalEndpoint);
                    Listener.Start(); //not sure wat it starts
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                byte[] RecData = new byte[BufferSize];
                int RecBytes;

                for (; ; ) //infinite loop comparable to (while true)
                {
                    TcpClient client = null;
                    NetworkStream netstream = null;
                    Status = string.Empty;
                    try
                    {


                        //string message = "Accept the Incoming File ";
                        //string caption = "Incoming Connection";
                       // MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                      bool result = false; //replacment for dialogresult from windows forms?


                        if (Listener.Pending())
                        {
                            client = Listener.AcceptTcpClient();
                            netstream = client.GetStream();
                            Status = "Connected to a client\n";
                            //result = MessageBox.Show(message, caption, buttons);

                            if (result == true)
                            {
                                string SaveFileName = "defaultname.wav";
                                //SaveFileDialog DialogSave = new SaveFileDialog();
                                //DialogSave.Filter = "All files (*.*)|*.*";
                                //DialogSave.RestoreDirectory = true;
                                //DialogSave.Title = "Where do you want to save the file?";
                                //DialogSave.InitialDirectory = @"C:/";
                                //if (DialogSave.ShowDialog() == DialogResult.OK)
                                //    SaveFileName = DialogSave.FileName;
                                if (SaveFileName != string.Empty)
                                {
                                    int totalrecbytes = 0;
                                    FileStream Fs = new FileStream(SaveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                                    while ((RecBytes = netstream.Read(RecData, 0, RecData.Length)) > 0)
                                    {
                                        Fs.Write(RecData, 0, RecBytes);
                                        totalrecbytes += RecBytes;
                                    }
                                    Fs.Close();
                                }
                                netstream.Close();
                                client.Close();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                      //  Console.WriteLine(ex.Message); //Not listening. You must call the Start() method before calling this method.
                                                       //netstream.Close();
                }
                }
            }

            //private void btnExit_Click(object sender, EventArgs e)
            //{
            //    T.Abort();
            //    this.Close();
            //}


        }
    }