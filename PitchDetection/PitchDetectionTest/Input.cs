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


        const float sample_freq = 44100;
        float freq_per = 0f;


        


       
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
                
                
               

                Console.WriteLine(freq_per);
               
            }while(!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape));
            
            
            

            
        }

      
        

       
        //This is getting called 50 times a second...
        void WaveIn_DataAvailable(object sender, WaveInEventArgs e){
            
            
           
            //Need to convert the byte array to proper format:
            short[] values = new short[e.Buffer.Length/2]; //try this - the 32 bit might be cranky.

            //figure this shit out
            for(int i = 0; i < e.Buffer.Length/2; i+=2){
                //Console.WriteLine(i);
                values[i] = (short)(e.Buffer[i+1] << 8 | e.Buffer[i]);
                //Console.WriteLine(values[i]);
                
            }

            
            
            
            
            float sum_old = 0.0f;
            float sum = 0.0f;
            float thresh = 0f;
        
            int pd_state = 0;
            int period = 0;



            //Autocorrelation
            for(int i = 0; i < values.Length; i++){ //Needs to change..
                //Console.WriteLine(i);
                //Converts the e.Buffer -> which is a byte array -> to int16 numbers
                
                //Console.WriteLine(e.Buffer[i]);
                
                sum_old = sum;
                sum = 0.0f;
                
                for(int k = 0; k < values.Length-i; k++){
                    sum+= (float)((values[k]-128) * (values[k+i]-128)/256f);
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