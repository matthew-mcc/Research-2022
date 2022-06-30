using NAudio.Wave;
using System;
using FftSharp; 

/*
Notes + References:
fft lib --> https://github.com/swharden/FftSharp
audio input lib --> https://github.com/SjB/NAudio
baseline tutorial --> https://swharden.com/csdv/audio/naudio/
*/

namespace PitchDetection{

    public class Sound{


        
        int sampleRate = 44100; //Standard sample rate for modern day audio
        int bitDepth = 16; //Not sure what this is for
        int channelCount = 1; //Assuming mono channel 
        int BufferMiliseconds = 20; // not really sure why we need this..

        int targetFreq = 440; //A 440

        int t = 0;
        

        Double[] audioValues;
        Double[] audioValuesTest;
        Double[] fftValues;

      


        //This entire function is just selecting the recording device..
        public int SelectMicrophone(){
            int inputDevice = 0;
            bool isValidChoice = false;

            do{
                Console.Clear();
                Console.WriteLine("Please select your input device: ");

                for(int i = 0; i < WaveInEvent.DeviceCount; i++){
                    Console.WriteLine(i + ". " + WaveInEvent.GetCapabilities(i).ProductName);
                }
                Console.WriteLine();

                try{
                    if(int.TryParse(Console.ReadLine(), out inputDevice)){
                        isValidChoice = true;
                        Console.WriteLine("You have chosen: " + WaveInEvent.GetCapabilities(inputDevice).ProductName + ".\n");
                    }
                    else{
                        isValidChoice = false;
                    }
                }
                catch{
                    throw new ArgumentException("Device # chosen is out of range :(\n");
                }
            } while(isValidChoice == false);
            return inputDevice;
        }

        public void beginInput(int deviceNum){

            audioValues = new Double[sampleRate * BufferMiliseconds / 1000]; //current values are 882
            audioValuesTest = new Double[sampleRate * BufferMiliseconds / 1000]; //current values are 882
            
            
            //Making an Naudio wave in event!
            WaveInEvent wave = new WaveInEvent(){
                DeviceNumber = deviceNum, //mic to use
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


                //CalculateFFt();
                
               

                
                
            }while(!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape));
            
            
            

            
        }

      
        

        // Event which does things with the wave.

        //This is getting called 50 times a second...
        void WaveIn_DataAvailable(object sender, WaveInEventArgs e){
            

            t+=1; // --> t = 50, 100, 150 every second.
            
            if(t>50){
                
                t = 0;
            }
            
            
            
            for(int i = 0; i < e.Buffer.Length / 2; i++){
                //Console.WriteLine(i);
                //Converts the e.Buffer -> which is a byte array -> to int16 numbers
                audioValues[i] = BitConverter.ToInt16(e.Buffer, i*2);

                //Console.WriteLine(t * 882 / sampleRate);
                double sinVal = Math.Sin(2 * Math.PI * targetFreq * (t * 882 / sampleRate));
                double cosVal = Math.Sin(2 * Math.PI * targetFreq * (t * 882 / sampleRate));

                Console.WriteLine(((t*882) + i ));
               // Console.WriteLine("t: " + t + " i: " + i);


                

            }
            

            
            //every 1/50th of a second we are getting 882 new values to compare to.

            
            
            
            
           
        }
        


        public void CalculateFFt(){

            //Ensuring the array is a power of 2 
            double[] paddedAudio = FftSharp.Pad.ZeroPad(audioValues);
            //Calculate the power spectrum density in dB units
            double[] fftMag = FftSharp.Transform.FFTpower(paddedAudio);

            //copy values into fftValues - current-most values stored in fftValues
            fftValues = new double[fftMag.Length];
            Array.Copy(fftMag, fftValues, fftMag.Length);
            //Console.WriteLine("length" + fftValues.Length);

            int peakIndex = 0;
            //Get the peak index of fftMag (the psd)
            for(int i = 0; i < fftMag.Length; i ++){
                if(fftMag[i] > fftMag[peakIndex]){
                    peakIndex = i;
                }
            }

            //Returning the distance between each FFT point in freq units (hertz)
            double fftPeriod = FftSharp.Transform.FFTfreqPeriod(sampleRate, fftMag.Length);
            //Console.WriteLine("period: " + fftPeriod);

            //Found this online - not sure how it works..
            double peakFrequency = fftPeriod * peakIndex;
            Console.Write(Math.Round(peakFrequency, 0) + " hz"); //Rounding to nearest decimal point


        }

        
        

    

        
    }
    
}