using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Phidget22;
using UnityEngine.UI;

public class RIB_Calibration : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI currentVoltageText;
    
    

    //PlayerController playerController;
    RIB_Belt_Controller RibController;
   
    [SerializeField] GameObject ribBelt;
    [SerializeField] Slider progressSlider;
    
    // Start is called before the first frame update --> Maybe make this into start ?? Why is it in awake
    
    void Start()
    {
        RibController = ribBelt.GetComponent<RIB_Belt_Controller>();
        
        infoText.text = "Up Arrow to Calibrate Max," + "\n" + "Down Arrow to Calibrate Min!";
      
        timerText.text = "BIG BREATH!";
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

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            progressSlider.value = 0;
            
            
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            progressSlider.value = 0;
        }

        

        

        
        if (RibController.timerStarted){

            
            if (RibController.toCalibrate == "Max"){
               
                progressSlider.value+= Time.deltaTime/3;
                timerText.text = "HOLD!";
                if(Math.Round(RibController.currentTime, 2) <= 0){
                    timerText.text = "RELEASE!";
                }

            }
            
            if (RibController.toCalibrate == "Min"){
              
                progressSlider.value += Time.deltaTime/3;
                timerText.text = "HOLD!";
            
                if(Math.Round(RibController.currentTime, 2) <= 0){
                    timerText.text = "RELEASE!";
                }
            }
            
        }

        
       
        if(RibBeltInformation.fullyCalibrated){
            infoText.text = "Calibration Reached, M for Main Menu!";
            timerText.text = "GREAT JOB!";
        }

        
    }
}
