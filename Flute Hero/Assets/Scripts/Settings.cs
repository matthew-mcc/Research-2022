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
    public static string targetFrequencyName = "C4";
    public static int targetFrequencyIndex = 1;
    public static int abChannel = 1;
    public static int ribChannel = 2;
    public static float PDAccuracyThreshold = 0.5f;
    public static float PDTimeLatency = 0.25f;
   
    
}




public class Settings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown micDropdown;
    [SerializeField] TMP_Dropdown noteDropdown;
    [SerializeField] TMP_Dropdown abChannelDropdown;
    [SerializeField] TMP_Dropdown ribChannelDropdown;
    

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
        
        //resetting curr val
        micDropdown.value = SettingsInformation.microphoneNumber;
        abChannelDropdown.value = SettingsInformation.abChannel;
        ribChannelDropdown.value = SettingsInformation.ribChannel;
        noteDropdown.value = SettingsInformation.targetFrequencyIndex;


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
        SettingsInformation.targetFrequencyIndex = noteNameIndex;
        SettingsInformation.targetFrequency = notes.ElementAt(noteNameIndex).Value;
        SettingsInformation.targetFrequencyName = notes.ElementAt(noteNameIndex).Key;
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
        dict.Add("C0", 261.625f);
        dict.Add("C#0", 277.183f);
        dict.Add("D0", 293.366f);
        dict.Add("D#0", 311.127f);
        dict.Add("E0", 329.627f);
        dict.Add("F0", 349.228f);
        dict.Add("F#0", 369.994f);
        dict.Add("G0", 391.995f);
        dict.Add("G#0", 415.304f);
        dict.Add("A0", 440.000f);
        dict.Add("A#0", 466.164f);
        dict.Add("B40", 493.883f);

        //Fifth Octave
        dict.Add("C1", 523.251f);
        dict.Add("C#1", 554.365f);
        dict.Add("D1", 587.330f);
        dict.Add("D#1", 622.254f);
        dict.Add("E1", 659.255f);
        dict.Add("F1", 698.456f);
        dict.Add("F#1", 739.988f);
        dict.Add("G1", 783.990f);
        dict.Add("G#1", 830.609f);
        dict.Add("A1", 880.000f);
        dict.Add("A#1", 932.327f);
        dict.Add("B1", 987.767f);

        //Sixth Octave
        dict.Add("C2", 1046.502f);
        dict.Add("C#2", 1107.730f);
        dict.Add("D2", 1174.660f);
        dict.Add("D#3", 1244.508f);
        dict.Add("E3", 1318.510f);
        dict.Add("F3", 1396.913f);
        dict.Add("F#3", 1479.978f);
        dict.Add("G3", 1567.982f);
        dict.Add("G#3", 1661.219f);
        dict.Add("A3", 1760.000f);
        dict.Add("A#3", 1864.655f);
        dict.Add("B3", 1975.533f);

    }




}

