using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Phidget22;

public class AB_Calibration : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI currentVoltageText;
    [SerializeField] TextMeshProUGUI minText;
    [SerializeField] TextMeshProUGUI maxText;
    

    //PlayerController playerController;
    AB_Belt_Controller AbController;
   
    [SerializeField] GameObject abBelt;
    
    // Start is called before the first frame update --> Maybe make this into start ?? Why is it in awake
    void Awake() {
        //playerController = player.GetComponent<PlayerController>();
        //AbController = abBelt.GetComponent<AB_Belt_Controller>();
        
    }
    void Start()
    {
        AbController = abBelt.GetComponent<AB_Belt_Controller>();
        
        infoText.text = "Up Arrow to Calibrate Max," + "\n" + "Down Arrow to Calibrate Min!";
      
        timerText.text = "Timer!";
    }

    // Update is called once per frame
    void Update()
    {
        
        currentVoltageText.text = "Current Voltage: " + Math.Round(AbController.currentVoltageForText, 4) + " V";
       
        if (AbController.timerStarted && AbController.toCalibrate == "Max"){
            infoText.text = "Max Voltage Calibration!";
            
        }
       
        if (AbController.timerStarted && AbController.toCalibrate == "Min"){
            infoText.text = "Min Voltage Calibration!";
        }

        

        

        
        if (AbController.timerStarted){

            
            if (AbController.toCalibrate == "Max"){
               
                
                timerText.text = Math.Round(AbController.currentTime, 2).ToString();
      
                if (AbController.currentTime <= 0){
                    timerText.text = "Done!";
                    infoText.text = "MaxVoltage: " + Math.Round(ABBeltInformation.maxVoltage, 4).ToString();
                }

            }
            
            if (AbController.toCalibrate == "Min"){
              
                timerText.text = Math.Round(AbController.currentTime, 2).ToString();
            
                if (AbController.currentTime <= 0){
                    timerText.text = "Done!";
                    infoText.text = "MinVoltage: " + Math.Round(ABBeltInformation.minVoltage, 4).ToString();
                }
            }
            
        }

        
       
        if(ABBeltInformation.fullyCalibrated){
            infoText.text = "Calibration Reached!";
            minText.text = "Min: " + Math.Round(ABBeltInformation.minVoltage, 4).ToString() + " V";
            maxText.text = "Max: " + Math.Round(ABBeltInformation.maxVoltage, 4).ToString() + " V";
        }

        
    }
}
