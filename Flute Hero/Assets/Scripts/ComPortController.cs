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
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.activeSceneChanged += ChangedActiveScene;
        AbController = abBelt.GetComponent<AB_Belt_Controller>();
        RibController = ribBelt.GetComponent<RIB_Belt_Controller>();
        initializeComPort();

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
        
    }

    void OnApplicationQuit(){
        comPort.Close();
    }


    

    
}
