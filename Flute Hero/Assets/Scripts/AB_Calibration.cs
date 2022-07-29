using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Phidget22;
using UnityEngine.UI;
public class AB_Calibration : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI currentVoltageText;
    
    
    [SerializeField] Slider progressSlider;

    //PlayerController playerController;
    AB_Belt_Controller AbController;
   
    [SerializeField] GameObject abBelt;
    
    // Start is called before the first frame update --> Maybe make this into start ?? Why is it in awake
    
    void Start()
    {
        AbController = abBelt.GetComponent<AB_Belt_Controller>();
        
        infoText.text = "Up Arrow to Calibrate Max," + "\n" + "Down Arrow to Calibrate Min!";
      
        timerText.text = "BIG BREATH!";
    }

    // Update is called once per frame
    void Update()
    {
        
        currentVoltageText.text = "Current Voltage: " + Math.Round(AbController.currentVoltageForText, 4) + " V";
       
        if (AbController.timerStarted && AbController.toCalibrate == "Max"){
            infoText.text = "Max Expansion Calibration!";
            
           
            
        }
       
        if (AbController.timerStarted && AbController.toCalibrate == "Min"){
            infoText.text = "Min Expansion Calibration!";
            
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            progressSlider.value = 0;
            
            
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            progressSlider.value = 0;
        }
        
        

        

        
        if (AbController.timerStarted){

            
            if (AbController.toCalibrate == "Max"){
                
                progressSlider.value += Time.deltaTime/3;
                
                //timerText.text = Math.Round(AbController.currentTime, 2).ToString();
                timerText.text = "HOLD!";
                if(Math.Round(AbController.currentTime, 2) <= 0){
                    timerText.text = "RELEASE!";
                    
                    
                }

                //goes from 0 to 1, the time goes from 3 to 0, with 0.002 increments
                // progressSlider.value += 0.0008f;
                
                // if(progressSlider.value == 1){
                //     timerText.text = "RELEASE!";
                //     infoText.text = "MaxVoltage: " + Math.Round(ABBeltInformation.maxVoltage, 4).ToString();
                // }
               

            }
            
            if (AbController.toCalibrate == "Min"){

                
                    
                //timerText.text = Math.Round(AbController.currentTime, 2).ToString();
                progressSlider.value+= Time.deltaTime/3;

                timerText.text = "HOLD!";
                if(Math.Round(AbController.currentTime, 2) <= 0){
                    timerText.text = "RELEASE!";
                    
                }
                
            }
            
        }

        
       
        if(ABBeltInformation.fullyCalibrated){
            infoText.text = "Calibration Reached, M for Main Menu!";
            timerText.text = "GREAT JOB!";
           
        }

        
    }
}
