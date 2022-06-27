using NAudio.Wave;
using System;
using FftSharp;

namespace PitchDetection{

    public class Sound{


        int inputDevice = 0;
        int sampleRate = 44100;
        int bitDepth = 16;
        int channelCount = 1;
        int BufferMiliseconds = 20; // not really sure why we need this..

        Double[] audioValues;
        Double[] fftValues;

        public void beginInput(){

            audioValues = new Double[sampleRate * BufferMiliseconds / 1000];
            

            //Making an Naudio wave in event!
            WaveInEvent wave = new WaveInEvent(){
                DeviceNumber = inputDevice, //mic to use
                WaveFormat = new WaveFormat(sampleRate, bitDepth, channelCount), //Format of the wave
                BufferMilliseconds = BufferMiliseconds //idk
            };

            wave.DataAvailable += WaveIn_DataAvailable; //Adding data to our wave from the event below..
            

            wave.StartRecording(); //Enabling the input off of microphone.

            
            
            //Need something to continously access the wave value - this makes it real time?
            Console.WriteLine("Escape to Exit!");

            do{
                //watever u want here
                Console.CursorLeft = 0;
                Console.CursorVisible = false;

                //Console.Write(maxPeak!)
                //Console.Write();
                FFT();
            }while(!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape));
            
            
            

            
        }
        // Event which does things with the wave.
        void WaveIn_DataAvailable(object sender, WaveInEventArgs e){
            for(int i = 0; i < e.Buffer.Length / 2; i++){
                audioValues[i] = BitConverter.ToInt16(e.Buffer, i*2);
                
            }

        }
        
        public void Print(){
            Console.WriteLine("yo!");
        }

        public void FFT(){

            //Ensuring the array is a power of 2 
            double[] paddedAudio = FftSharp.Pad.ZeroPad(audioValues);
            //Calculate the power spectrum density in dB units
            double[] fftMag = FftSharp.Transform.FFTpower(paddedAudio);

            //copy values into fftValues - current-most values stored in fftValues
            fftValues = new double[fftMag.Length];
            Array.Copy(fftMag, fftValues, fftMag.Length);

            int peakIndex = 0;
            //Get the peak index of fftMag (the psd)
            for(int i = 0; i < fftMag.Length; i ++){
                if(fftMag[i] > fftMag[peakIndex]){
                    peakIndex = i;
                }
            }

            //Returning the distance between each FFT point in freq units (hertz)
            double fftPeriod = FftSharp.Transform.FFTfreqPeriod(sampleRate, fftMag.Length);

            //Found this online - not sure how it works..
            double peakFrequency = fftPeriod * peakIndex;
            Console.Write(Math.Round(peakFrequency, 0) + " hz");

        }

        
    }
    
}