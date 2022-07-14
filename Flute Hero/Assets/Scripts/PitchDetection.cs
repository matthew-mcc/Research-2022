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
    
    [SerializeField] Color onTarget;
    [SerializeField] Color offTarget;
    //NEEDS TO CHANGE --> ITS FINE FOR NOW
    [SerializeField] float targetFrequency = 440;
    [SerializeField] float accuracyThreshold = 0.5f;
    const float sample_freq = 44100;
    float freq_per = 0f;

    [SerializeField] int mic_number = 0;

    WaveInEvent wave;
    SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        beginInput(mic_number);

        sr = GetComponent<SpriteRenderer>();
    }

   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            wave.StopRecording();
            wave.Dispose();
        }
        
        //This will be the happy / sad thing..
        if(Math.Abs(hzToMidi(freq_per, 3) - hzToMidi(targetFrequency, 3)) < accuracyThreshold){
            sr.color = onTarget;
        }
        else{
            sr.color = offTarget;
        }
    }

    private void beginInput(int deviceNum)
    {
        wave = new WaveInEvent(){
            DeviceNumber = deviceNum,
            WaveFormat = new WaveFormat(sampleRate, bitDepth, channelCount),
            BufferMilliseconds = BufferMiliseconds,
        };
        
        wave.DataAvailable += WaveIn_DataAvailable;
        wave.StartRecording();
    }

    void WaveIn_DataAvailable(object sender, WaveInEventArgs e){
        float[] values = new float[e.Buffer.Length/2];
        Byte[] xRaw = e.Buffer;

        for(int i = 0; i < xRaw.Length/2; i++){
            values[i] = xRaw[(i*2) + 1] * 256 + xRaw[i*2];
        }
        
        for(int i = 0; i < values.Length; i++){
            if(values[i] >= 32768){
            values[i] = values[i] - 65536;
            }
        }

        for(int i = 0; i < values.Length; i++){
            values[i] = values[i] / 32768;
        }

        float sum_old;
        float sum = 0.0f;
        float thresh = 0f;

        int pd_state = 0;
        int period = 0;

        //Autocorrellation
        for(int i = 0; i < values.Length; i++){
            sum_old = sum;
            sum = 0.0f;

            for(int k = 0; k < values.Length-i; k++){
                sum+= (float)((values[k]) * (values[k+i]));
            }
            
            if(pd_state == 2 && (sum-sum_old) <=0){
                period = i - 1;
                pd_state = 3;
            }
            
            if(pd_state == 1 && (sum > thresh) && (sum-sum_old) > 0){
                pd_state = 2;
            }
            if(pd_state == 0){
                thresh = sum * 0.5f;
                pd_state = 1;
            }
        }
        freq_per = sample_freq / period;

    }
    float hzToMidi(float freq, int decPlaces){
            double rawVal = 69f + 12*Math.Log(freq/440f, 2);

            
            float midiVal = (float) Math.Round(rawVal, decPlaces);
            return midiVal;
        }


}
