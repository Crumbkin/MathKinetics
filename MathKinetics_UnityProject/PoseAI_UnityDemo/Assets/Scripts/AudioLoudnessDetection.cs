using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/***************************************************************************************
*    Title: How to Use Your Voice as Input in Unity - Microphone and Audio Loudness Detection
*    Author: Valem Tutorials
*    Date:  08 Feb 2023
*    Availability: https://www.youtube.com/watch?v=dzD0qP8viLw
*
***************************************************************************************/

public class AudioLoudnessDetection : MonoBehaviour
{

    public int sampleWindow = 64;
    private AudioClip microphoneClip;


    private void Start()
    {
        MicrophoneToAudioClip();
    }
    public void MicrophoneToAudioClip()
    {
        //Get the first microphone in device list
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
        Debug.Log(microphoneClip);
    }

    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }


    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
            return 0;

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        //compute loudness
        float totalLoudness = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }
}
