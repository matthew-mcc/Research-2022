using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using UnityEngine.SceneManagement;

public class ComPortController : MonoBehaviour
{   

    public static SerialPort comPort;
    AB_Belt_Controller AbController;
    RIB_Belt_Controller RibController;

    [SerializeField] GameObject abBelt;
    [SerializeField] GameObject ribBelt;

    Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.activeSceneChanged += ChangedActiveScene;
        AbController = abBelt.GetComponent<AB_Belt_Controller>();
        RibController = ribBelt.GetComponent<RIB_Belt_Controller>();
        initializeComPort();
        currentScene = SceneManager.GetActiveScene();
        //initializeComPort();
        
    }

    private void OnEnable() {

        if(comPort.IsOpen){
            comPort.Close();
        }
        else{
            initializeComPort();
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        if(currentScene.name == "Bar_Easy" || currentScene.name == "Bar_Medium" || currentScene.name == "Bar_Hard"
       || currentScene.name == "Ab_Hoop_Easy" || currentScene.name == "Ab_Hoop_Medium" || currentScene.name == "Ab_Hoop_Hard"
       || currentScene.name == "Rib_Hoop_Easy" || currentScene.name == "Rib_Hoop_Medium" || currentScene.name == "Rib_Hoop_Hard"){
        
        string logString = currentScene.name + " (Ab): " + AbController.abComPortVoltage.ToString() + " " + currentScene.name + " (Ab): " + RibController.ribComPortVoltage.ToString();
        Debug.Log(logString);
        //Debug.Log(currentScene.name + " Ab Voltage: " + currentVoltageForText.ToString());
       }
    }

    void initializeComPort(){
        // UISING system.io.ports.6.0.0.zip\runtimes\win\lib\netstandard2.0 dll

        string[] portNames = SerialPort.GetPortNames();

        
        

        
        

        comPort = new SerialPort(SettingsInformation.portName, 19200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
        comPort.Handshake = Handshake.None;
        comPort.DtrEnable = true;

        
        comPort.DataReceived += new SerialDataReceivedEventHandler(DataRecievedHandler);
        comPort.Open(); // could be dangerous, not sure when to close it.

       
    
    }
    public void DataRecievedHandler(object sender, SerialDataReceivedEventArgs e){
        string indata = comPort.ReadLine();
        string[] subs = indata.Split(' ');
        AbController.abComPortVoltage = float.Parse(subs[6]);
        RibController.ribComPortVoltage = float.Parse(subs[3]);

        AbController.deviceName = (subs[0] + subs[1]);
        Debug.Log("### DEVICE_NAME: " + AbController.deviceName + " ###");
       
        
        
    }

    void OnApplicationQuit(){
        comPort.Close();
    }


    

    
}
