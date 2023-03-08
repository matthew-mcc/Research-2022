using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;
public class MainMenu : MonoBehaviour
{
    public static bool initDone = false;
    List<string> initialPortNames = new List<string>();
    void Start(){
        if(initDone == false){

        
        string[] port_names = SerialPort.GetPortNames();

        foreach(string pn in port_names){
            initialPortNames.Add(pn);
        }
        foreach(string port in initialPortNames){
            try{
                SerialPort comPort = new SerialPort(port, 19200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                comPort.Handshake = Handshake.None;
                comPort.DtrEnable = true;

                comPort.Open();
                for(int i = 0; i < 10; i++){
                    string indata = comPort.ReadLine();
                    string[] subs = indata.Split(' ');
                    //Debug.Log(indata);
                    Debug.Log("### DEVICE NAME: " + subs[0] + subs[1] + " ###");
                    if(subs[0] == "ATEMP:"){
                        Debug.Log("Found the Right PORT!");
                        SettingsInformation.portName = port;
                        break;
                    }
                }
                comPort.Close();
                
                
                
                

            }
            catch{
                Debug.Log("Failed to use: " + port);
            }
        }
        initDone =true;
        }
    }
    
    
    public void LoadMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void CalibrateAB(){
        Debug.Log("Entered Ab Calibration");
        SceneManager.LoadScene("AB_Calibration");
        
    }
    public void CalibrateRIB(){
        Debug.Log("Entered Rib Calibration");
        SceneManager.LoadScene("RIB_Calibration");
        
    }

    public void QuitGame(){
        Debug.Log("Quitting game!");
        Application.Quit();
    }


    // Hoop Levels

    //Ab
    public void Ab_Hoop_Easy(){
        Debug.Log("Entered Ab-Hoop Easy");
        SceneManager.LoadScene("Ab_Hoop_Easy");
        
    }
    public void Ab_Hoop_Med(){
        Debug.Log("Entered Ab-Hoop Medium");
        SceneManager.LoadScene("Ab_Hoop_Medium");
        
    }
    public void Ab_Hoop_Hard(){
        Debug.Log("Entered Ab-Hoop Hard");
        SceneManager.LoadScene("Ab_Hoop_Hard");
        
    }

    //Rib
    public void Rib_Hoop_Easy(){
        Debug.Log("Entered Rib-Hoop Easy");
        SceneManager.LoadScene("Rib_Hoop_Easy");
        
    }
    public void Rib_Hoop_Med(){
        Debug.Log("Entered Rib-Hoop Medium");
        SceneManager.LoadScene("Rib_Hoop_Medium");
        
    }
    public void Rib_Hoop_Hard(){
        Debug.Log("Entered Rib-Hoop Hard");
        SceneManager.LoadScene("Rib_Hoop_Hard");
       
    }

    //Bar Levels
    public void Bar_Easy(){
        Debug.Log("Entered Bar-Level Easy");
        SceneManager.LoadScene("Bar_Easy");
        
        
        
    }
    public void Bar_Medium(){
        Debug.Log("Entered Bar-Level Medium");
        SceneManager.LoadScene("Bar_Medium");
        
        
    }
    public void Bar_Hard(){
        Debug.Log("Entered Bar-Level Hard");
        SceneManager.LoadScene("Bar_Hard");
        
        
    }

    public void PD_Only(){
        Debug.Log("Entered Pitch Detection Level");
        SceneManager.LoadScene("PD_Only");
        
    }

    public void BaseCamp(){
        Debug.Log("Entered Base Camp!");
        SceneManager.LoadScene("BaseCamp");
    }
}
