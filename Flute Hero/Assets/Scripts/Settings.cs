using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio.Wave;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public static class SettingsInformation{

    public static int microphoneNumber = 0;
    public static float targetFrequency = 261.5f;
    public static int abChannel = 1;
    public static int ribChannel = 2;
    public static float PDAccuracyThreshold = 0.5f;
    public static float PDTimeLatency = 0.25f;
   
    
}




public class Settings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown micDropdown;
    [SerializeField] TMP_Dropdown noteDropdown;
    

    public List<string> microphoneList = new List<string>();
    public Dictionary<string, float> notes = new Dictionary<string, float>();

    public Slider latencySlider;
    public Slider thresholdSlider;

    void Start() {
        micDropdown.options.Clear();
        noteDropdown.options.Clear();

        latencySlider.value = SettingsInformation.PDTimeLatency;
        thresholdSlider.value = SettingsInformation.PDAccuracyThreshold;

        ListMicrophones(microphoneList);
        
        micDropdown.AddOptions(microphoneList);

        PopulateNotes(notes);

        List<string> noteNames = new List<string>();
        foreach(KeyValuePair<string, float> entry in notes){
            noteNames.Add(entry.Key);
        }
        noteDropdown.AddOptions(noteNames);
        


    }




   public void ListMicrophones(List<string> mics){
    
    for(int i = 0; i < WaveInEvent.DeviceCount; i++){
        mics.Add(WaveInEvent.GetCapabilities(i).ProductName);
    }
}

    public void SetMic(int micNumber){
        SettingsInformation.microphoneNumber = micNumber;
    }

    public void SetTargetNote(int noteNameIndex){
        SettingsInformation.targetFrequency = notes.ElementAt(noteNameIndex).Value;
    }

    public void SetAbPort(int channel){
        SettingsInformation.abChannel = channel;
    }
    public void SetRibPort(int channel){
        SettingsInformation.ribChannel = channel;
    }
    public void SetPDThreshold(float threshold){
        SettingsInformation.PDAccuracyThreshold = threshold;
    }
    public void SetPDLatency(float latency){
        SettingsInformation.PDTimeLatency = latency;
    }

    public void PopulateNotes(Dictionary<string, float> dict){

        //Fourth Octave
        dict.Add("C4", 261.625f);
        dict.Add("C#4", 277.183f);
        dict.Add("D4", 293.366f);
        dict.Add("D#4", 311.127f);
        dict.Add("E4", 329.627f);
        dict.Add("F4", 349.228f);
        dict.Add("F#4", 369.994f);
        dict.Add("G4", 391.995f);
        dict.Add("G#4", 415.304f);
        dict.Add("A4", 440.000f);
        dict.Add("A#4", 466.164f);
        dict.Add("B4", 493.883f);

        //Fifth Octave
        dict.Add("C5", 523.251f);
        dict.Add("C#5", 554.365f);
        dict.Add("D5", 587.330f);
        dict.Add("D#5", 622.254f);
        dict.Add("E5", 659.255f);
        dict.Add("F5", 698.456f);
        dict.Add("F#5", 739.988f);
        dict.Add("G5", 783.990f);
        dict.Add("G#5", 830.609f);
        dict.Add("A5", 880.000f);
        dict.Add("A#5", 932.327f);
        dict.Add("B5", 987.767f);

        //Sixth Octave
        dict.Add("C6", 1046.502f);
        dict.Add("C#6", 1107.730f);
        dict.Add("D6", 1174.660f);
        dict.Add("D#6", 1244.508f);
        dict.Add("E6", 1318.510f);
        dict.Add("F6", 1396.913f);
        dict.Add("F#6", 1479.978f);
        dict.Add("G6", 1567.982f);
        dict.Add("G#6", 1661.219f);
        dict.Add("A6", 1760.000f);
        dict.Add("A#6", 1864.655f);
        dict.Add("B6", 1975.533f);

    }




}

