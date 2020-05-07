using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

public class TcpAudioClient : MonoBehaviour
{
  public void Main()
    {
        InitializeComponent();

        // Initialize tcp client.
        _tcpClient = new TcpClient();
        _recordingStream = new MemoryStream();

        _soundRecorder = new WaveIn();
        _waveOut = new WaveOut();
        _playbackBufferedWaveProvider = new BufferedWaveProvider(_soundRecorder.WaveFormat);
        _waveOut.Init(_playbackBufferedWaveProvider);
        _soundRecorder.DataAvailable += SoundRecorderOnDataAvailable;

        btnRecord.Enabled = true;
    }

    private void SoundRecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
    {
        if (_networkStream != null)
        {
            _networkStream.Write(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);
            _networkStream.Flush();
        }
    }
}
