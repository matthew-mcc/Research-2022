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
    [SerializeField] TextMeshProUGUI bigTimer;
    
    
    bool textFlag = false;

    //PlayerController playerController;
    RIB_Belt_Controller RibController;
   
    [SerializeField] GameObject ribBelt;
    [SerializeField] Slider progressSlider;

    private float currTime = 3f;
    
    // Start is called before the first frame update --> Maybe make this into start ?? Why is it in awake
    
    void Start()
    {
        
        RibController = ribBelt.GetComponent<RIB_Belt_Controller>();
        
        textFlag = false;
        infoText.text = "Breathe, HOLD, Then UP Key!";      
        timerText.text = "BIG BREATH!";
        bigTimer.text = 3.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        currentVoltageText.text = "Current Voltage: " + Math.Round(RibController.currentVoltageForText, 4) + " V";
       
        if (RibController.timerStarted && RibController.toCalibrate == "Max"){
            infoText.text = "Hold that big breath!";
            
        }
       
        if (RibController.timerStarted && RibController.toCalibrate == "Min"){
            infoText.text = "Stay relaxed!";
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            progressSlider.value = 0;
            currTime = 3f;
            
            
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            progressSlider.value = 0;
            currTime = 3f;
        }

        

        

        
        if (RibController.timerStarted){

            
            if (RibController.toCalibrate == "Max"){
                currTime-=Time.deltaTime;
               
                progressSlider.value+= Time.deltaTime/3;
                timerText.text = "HOLD!";
                bigTimer.text = Math.Round(currTime, 1).ToString();
                if(Math.Round(RibController.currentTime, 2) <= 0){
                    timerText.text = "RELEASE!";
                    infoText.text = "Relax, HOLD, Then DOWN Key!";
                    Debug.Log("Rib Belt Calibrated with Max Voltage: " + RibBeltInformation.maxVoltage);
                    if(RibController.minCalibrated){
                        textFlag = true;
                    }
                }

            }
            
            if (RibController.toCalibrate == "Min"){
                
                currTime -= Time.deltaTime;
                progressSlider.value += Time.deltaTime/3;
                timerText.text = "RELAX!";

                bigTimer.text = Math.Round(currTime, 1).ToString();
                if(Math.Round(RibController.currentTime, 2) <= 0){
                    timerText.text = "RELEASE!";
                    Debug.Log("Rib Belt Calibrated with Min Voltage: " + RibBeltInformation.minVoltage);
                    if(RibController.maxCalibrated){
                        textFlag = true;
                    }
                }
            }
            
        }

        
       
        if(textFlag){
            infoText.text = "All done, M for Main Menu!";
            timerText.text = "GREAT JOB!";
        }

        
    }
}
