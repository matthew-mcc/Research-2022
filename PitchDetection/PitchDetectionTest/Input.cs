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

        const int numSeconds = 10;

        const float sample_freq = 44100;
        float freq_per = 0f;

        
        const float target_freq = 440f;
        //for output file
        int freqTimer = 0;
        int timerBeforePacking = 0;
        int timerAfterPacking = 0;
        float[] freqArr = new float[50*numSeconds];
        byte[] beforePacking = new byte[1764*50*numSeconds];
        float[] afterPacking = new float[1764/2*50*numSeconds];
        
        string noteName = "A4";


       
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
                DeviceNumber = deviceNum, 
                WaveFormat = new WaveFormat(sampleRate, bitDepth, channelCount), 
                BufferMilliseconds = BufferMiliseconds 
            };

            wave.DataAvailable += WaveIn_DataAvailable; //Adding data to our wave from the event below..
            

            wave.StartRecording(); //Enabling the input off of microphone.

            
            
            //Need something to continously access the wave value - this makes it real time?
            Console.WriteLine("Escape to Exit!");

            do{
                //watever u want here
                Console.CursorLeft = 0;
                Console.CursorVisible = false;
                
                
            //Console.WriteLine(freq_per);

               
               
            }while(!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape));
            
            
            

            
        }

      
        

       
        //This is getting called 50 times a second...
        void WaveIn_DataAvailable(object sender, WaveInEventArgs e){
            

            
            
            //Need to convert the byte array to proper format:
            float[] values = new float[e.Buffer.Length/2]; 

            
            Console.WriteLine(timerBeforePacking);
            //OUTPUT RAW DATA
            Byte[] xRaw = e.Buffer;
            
            try{
                for(int i = 0; i < xRaw.Length; i++){
                    beforePacking[timerBeforePacking] = xRaw[i];
                    timerBeforePacking+=1;
                }
                
            }
            catch{
                Console.WriteLine("Before Packing Array is Full!");
                string[] outArr = new string[beforePacking.Length];
                for(int i = 0; i < beforePacking.Length; i++){
                    outArr[i] = beforePacking[i].ToString();
                }
                string filePath = noteName + "_Before_Packing.txt";
                File.WriteAllLines(filePath, outArr);
            }
            
            //xLo = 0, 2, 4
            //xhi = 1, 3, 5

            //refactor dis
            for(int i = 0; i < xRaw.Length/2; i++){
                //val = xhigh * 256 + xlo
                //values[0] = xRaw[1] * 256 + xRaw[0] --> i = 0
                //values[1] = xRaw[3] * 256 + xRaw[2] --> i = 1
                //values[2] = xRaw[5] * 256 + xRaw[4] --> i = 2 // this has type Int32
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

            // for(int i = 0; i < values.Length; i++){
            //     values[i] = values[i] / target_freq;
            // }
            
            try{
                for(int i = 0; i < values.Length; i++){
                    afterPacking[timerAfterPacking] = values[i];
                    timerAfterPacking+=1;
                }
                
            }
            catch{
                Console.WriteLine("After Packing Array Full!");
                string[] outArr = new string[afterPacking.Length];
                for(int i = 0; i < afterPacking.Length; i++){
                    outArr[i] = afterPacking[i].ToString();
                }
                string filePath = noteName + "_After_Packing.txt";
                File.WriteAllLines(filePath, outArr);
            }

            




            

            
            
            
            
            
            
            float sum_old;
            float sum = 0.0f;
            float thresh = 0f;
        
            int pd_state = 0;
            int period = 0;
            //Autocorrelation
            for(int i = 0; i < values.Length; i++){ //Needs to change..
                
                sum_old = sum;
                sum = 0.0f;
                
                for(int k = 0; k < values.Length-i; k++){
                    sum+= (float)((values[k]) * (values[k+i]));
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
            

            //OUTPUT FREQ ARR
            try{
                freqArr[freqTimer] = freq_per; //stores freq Arr

                
                freqTimer+=1;
            }
            catch{

                Console.WriteLine("Frequency Array is full!");
                string[] outArr = new string[freqArr.Length];
                for(int i = 0; i < freqArr.Length; i++){
                    float midival = hzToMidi(freqArr[i], 4);

                    //outArr[i] = midival.ToString();
                    outArr[i] = freqArr[i].ToString();
                }
                string filePath = noteName + "_Autocorrelated_Frequency.txt";
                File.WriteAllLines(filePath, outArr);
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