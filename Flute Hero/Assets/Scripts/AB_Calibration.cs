using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Phidget22;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AB_Calibration : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI currentVoltageText;
    [SerializeField] TextMeshProUGUI bigTimer;
    
    [SerializeField] Slider progressSlider;

    bool textFlag = false;
    //PlayerController playerController;
    AB_Belt_Controller AbController;
   
    [SerializeField] GameObject abBelt;

    private float currTime = 3f;

    
    
    // Start is called before the first frame update --> Maybe make this into start ?? Why is it in awake
    
    void Start()
    {
        //Debugging
        
        
        AbController = abBelt.GetComponent<AB_Belt_Controller>();
        
        textFlag = false;
        if(!textFlag)
            infoText.text = "Breathe, HOLD, Then UP Key!";
      
        timerText.text = "BIG BREATH!";
        bigTimer.text = 3.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        currentVoltageText.text = "Current Voltage: " + Math.Round(AbController.currentVoltageForText, 4) + " V";
       
        if (AbController.timerStarted && AbController.toCalibrate == "Max"){
            infoText.text = "Hold that big breath!";
            
           
            
        }
       
        if (AbController.timerStarted && AbController.toCalibrate == "Min"){
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
        
        

        

        
        if (AbController.timerStarted){

            
            if (AbController.toCalibrate == "Max"){
                
                currTime -= Time.deltaTime;
                progressSlider.value += Time.deltaTime/3;
                
                
                //timerText.text = Math.Round(AbController.currentTime, 2).ToString();
                timerText.text = "HOLD!";
                bigTimer.text = Math.Round(currTime, 0).ToString();
                if(Math.Round(AbController.currentTime, 2) <= 0){
                    timerText.text = "RELEASE!";
                    
                    infoText.text = "Relax, HOLD, Then DOWN Key!";

                    //FOR DEBUGGING
                    
                    if(AbController.minCalibrated){
                        textFlag = true;
                    }

                    
                    
                }
                //Debug.Log(ABBeltInformation.maxVoltage);

                //goes from 0 to 1, the time goes from 3 to 0, with 0.002 increments
                // progressSlider.value += 0.0008f;
                
                // if(progressSlider.value == 1){
                //     timerText.text = "RELEASE!";
                //     infoText.text = "MaxVoltage: " + Math.Round(ABBeltInformation.maxVoltage, 4).ToString();
                // }
               

            }
            
            if (AbController.toCalibrate == "Min"){

                
                currTime -= Time.deltaTime;
                
                //timerText.text = Math.Round(AbController.currentTime, 2).ToString();
                progressSlider.value+= Time.deltaTime/3;

                timerText.text = "RELAX!";
                bigTimer.text = Math.Round(currTime, 0).ToString();
                if(Math.Round(AbController.currentTime, 2) <= 0){
                    timerText.text = "RELEASE!";
                    //Debug.Log("Ab Belt Calibrated with Min Voltage: " + ABBeltInformation.minVoltage);

                    if(AbController.maxCalibrated){
                        textFlag = true;
                    }
                }
                
            }
            
        }

        
       
        if(textFlag){
            
            //If anything breaks it will be here
            //Need to test this with belts..
            //Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            
            infoText.text = "All done, M for Main Menu!";
            timerText.text = "GREAT JOB!";
           
        }
        

        
    }
}
