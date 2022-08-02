using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio.Wave;
using TMPro;
using System.Linq;

public static class SettingsInformation{

    public static int microphoneNumber = 0;
    public static float targetFrequency = 261.5f;
    //going to be name - freq
    
}




public class Settings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown micDropdown;
    [SerializeField] TMP_Dropdown noteDropdown;
    

    public List<string> microphoneList = new List<string>();
    public Dictionary<string, float> notes = new Dictionary<string, float>();

    void Start() {
        micDropdown.options.Clear();
        noteDropdown.options.Clear();

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
        ABBeltInformation.port = channel;
    }
    public void SetRibPort(int channel){
        RibBeltInformation.port = channel;
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

        //Maybe need more??

    }




}

