using NAudio.Wave;
using System;
using FftSharp; 

/*
Notes + References:
fft lib --> https://github.com/swharden/FftSharp
audio input lib --> https://github.com/SjB/NAudio
baseline tutorial --> https://swharden.com/csdv/audio/naudio/

Current Todos:

1. Real Time Graphing

*/

namespace PitchDetection{

    public class Sound{


        
        int sampleRate = 44100; //Standard sample rate for modern day audio
        int bitDepth = 16; //Not sure what this is for
        int channelCount = 1; //Assuming mono channel 
        int BufferMiliseconds = 20; // not really sure why we need this..

        float accuracyThreshold = 5f;
        float targetFreq = 440f;

        const float sample_freq = 44100;
        float freq_per = 0f;

        float minThreshold = 200f;
        List<float> freqSums = new List<float>();

        float[] freqArr = new float[50*10];
        float[] midiArr = new float[50*5];

        List<byte> rawData = new List<byte>();

        int timerChecker = 0;
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
                
                
                // if(Math.Abs(freq_per-440) < accuracyThreshold){
                    
                //     Console.Write("Middle A 440hz!");
                // }
                // if(Math.Abs(freq_per-262) < accuracyThreshold){
                //    Console.Write("Middle C 262hz!");
                // }
                // if(Math.Abs(freq_per - 330) < accuracyThreshold){
                //    Console.Write("Middle E 330hz!");
                // }
                // else{
                //     Console.Write("Don't Know that note yet!");

                //}

                //Console.WriteLine(hzToMidi(freq_per, 2));
               // Console.Write(freq_per);
               Console.WriteLine(timerChecker);
               
            }while(!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape));
            
            
            

            
        }

      
        

       
        //This is getting called 50 times a second...
        void WaveIn_DataAvailable(object sender, WaveInEventArgs e){
            

           
            
            float sum = 0.0f;
            float sum_old = 0.0f;
            float thresh = 0f;
            
         
            int pd_state = 0;
            int period = 0;

            //95% sure that e.Buffer contains 8-bit unsigned integers..
            for(int i = 0; i < e.Buffer.Length; i++){
                //Console.WriteLine(i);
                //Converts the e.Buffer -> which is a byte array -> to int16 numbers
                
                //Console.WriteLine(e.Buffer[i]);

                rawData.Add(e.Buffer[i]);
                for(int k = 0; k < e.Buffer.Length-i; k++){
                    sum+=(float)((e.Buffer[k]-128) * (e.Buffer[k+i] - 128));
                }

                if(pd_state == 2 && (sum-sum_old) <=0){
                    period = i-1;
                    pd_state = 3;
                }
                if(pd_state == 1 && (sum > thresh) && (sum-sum_old) > 0 ){
                    pd_state = 2;
                }
                if(pd_state == 0){
                    thresh = sum * 0.5f;
                    pd_state = 1;
                }
          

            }

            freq_per = sample_freq/period; //Current Freq in Hertz
            
            try{
                freqArr[timerChecker] = freq_per;
                timerChecker+=1;
            }
            catch{
                Console.WriteLine("Array is full!");

                string[] outArray = new string[rawData.Count];
                for(int i = 0; i < rawData.Count; i++){
                    outArray[i] = rawData[i].ToString();
                }
                File.WriteAllLines("raw.txt", outArray);


            }
            
            
            
            
            Console.CursorLeft = 0;
            Console.CursorVisible = false;
            
            
            
        
           
        }

        float hzToMidi(float freq, int decPlaces){
            double rawVal = 69f + 12*Math.Log2(freq/440f);

            float midiVal = (float) Math.Round(rawVal, decPlaces);
            return midiVal;
        }

        
        

        
    }
    
}