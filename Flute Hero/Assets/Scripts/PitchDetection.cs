using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio.Wave;

using System;

public class PitchDetection : MonoBehaviour
{


    [SerializeField] int sampleRate = 44100;
    [SerializeField] int bitDepth = 16;
    [SerializeField] int channelCount = 1;
    [SerializeField] int BufferMiliseconds = 20;

    const float sample_freq = 44100;
    float freq_per = 0f;

    [SerializeField] int mic_number = 0;

    
    


    // Start is called before the first frame update
    void Start()
    {
        beginInput(mic_number);
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }

    private void beginInput(int deviceNum)
    {
        // WaveInEvent wave = new WaveInEvent(){
        //     DeviceNumber = deviceNum,
        //     WaveFormat = new WaveFormat(sampleRate, bitDepth, channelCount),
        //     BufferMiliseconds = BufferMiliseconds
        // };
    }
}
