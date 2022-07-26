using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Phidget22;

public class RIB_Calibration : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI currentVoltageText;
    [SerializeField] TextMeshProUGUI minText;
    [SerializeField] TextMeshProUGUI maxText;
    

    //PlayerController playerController;
    RIB_Belt_Controller RibController;
   
    [SerializeField] GameObject ribBelt;
    
    // Start is called before the first frame update --> Maybe make this into start ?? Why is it in awake
    
    void Start()
    {
        RibController = ribBelt.GetComponent<RIB_Belt_Controller>();
        
        infoText.text = "Up Arrow to Calibrate Max," + "\n" + "Down Arrow to Calibrate Min!";
      
        timerText.text = "Timer!";
    }

    // Update is called once per frame
    void Update()
    {
        
        currentVoltageText.text = "Current Voltage: " + Math.Round(RibController.currentVoltageForText, 4) + " V";
       
        if (RibController.timerStarted && RibController.toCalibrate == "Max"){
            infoText.text = "Max Voltage Calibration!";
            
        }
       
        if (RibController.timerStarted && RibController.toCalibrate == "Min"){
            infoText.text = "Min Voltage Calibration!";
        }

        

        

        
        if (RibController.timerStarted){

            
            if (RibController.toCalibrate == "Max"){
               
                    
                timerText.text = Math.Round(RibController.currentTime, 2).ToString();
      
                if (RibController.currentTime <= 0){
                    timerText.text = "Done!";
                    infoText.text = "MaxVoltage: " + Math.Round(RibBeltInformation.maxVoltage, 4).ToString();
                }

            }
            
            if (RibController.toCalibrate == "Min"){
              
                timerText.text = Math.Round(RibController.currentTime, 2).ToString();
            
                if (RibController.currentTime <= 0){
                    timerText.text = "Done!";
                    infoText.text = "MinVoltage: " + Math.Round(RibBeltInformation.minVoltage, 4).ToString();
                }
            }
            
        }

        
       
        if(ABBeltInformation.fullyCalibrated){
            infoText.text = "Calibration Reached, M for Main Menu!";
            minText.text = "Min: " + Math.Round(RibBeltInformation.minVoltage, 4).ToString() + " V";
            maxText.text = "Max: " + Math.Round(RibBeltInformation.maxVoltage, 4).ToString() + " V";
        }

        
    }
}
