using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phidget22;
using TMPro;


public class CalibratePlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI voltageText;
    [SerializeField] float verticalMoveSpeed = 100f;
    

    private float maxVoltage = 4.01f;
    private float minVoltage = 3.6f;
  
    [SerializeField] int phidgetChannel = 0;

    //might want to set these to be true initially, and can begin calibration whenenver you want...
    private bool maxCalibrated = false;
    private bool minCalibrated = false;

    private int toCalibrate; // --> 0 to calibrate max, 1 to calibrate min

    private bool timerStarted = false;
    private float currentTime;
    [SerializeField] float startTime = 3f; //Seconds to calibrate for...

    private List<double> maxVoltageArr = new List<double>();
    private List<double> minVoltageArr = new List<double>();
  
    
    //channel 
    VoltageInput ch;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTime = startTime;
        timerText.text = currentTime.ToString();
        voltageText.text = "^ To Calibrate Max, v to Calibrate Min";
        initializePhidget();
        

    }

    // Update is called once per frame
    void Update()
    {
        //float moveDirection = Input.GetAxisRaw("Vertical"); //-1 is down + 1 is up

        // if we press up arrow, begin calibration of max
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            voltageText.text = "MaxVoltage Calibration!";
            //Debug.Log("Begin Max Voltage Calibration");

            timerStarted = true;
            toCalibrate = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            voltageText.text = "MinVoltage Calibration!";
            //Debug.Log("Begin Min Voltage Calibration");
            timerStarted = true;
            toCalibrate = 1;
        }

        if (timerStarted){
            
            if(toCalibrate == 0){
                //Calibrate max
                timerText.text = currentTime.ToString();
                currentTime -= Time.deltaTime;
                maxVoltageArr.Add(ch.Voltage);

                if(currentTime <= 0){
                    //finish calibration
                    timerText.text = "Done!";
                    maxVoltage = (float) averageList(maxVoltageArr);
                    maxCalibrated = true;
                    timerStarted = false;
                    currentTime = startTime;
                    voltageText.text = "MaxVoltage: " + maxVoltage.ToString();
                }

                
            }
            if(toCalibrate == 1){
                //Calibrate Min
                timerText.text = currentTime.ToString();
                currentTime -= Time.deltaTime;
                minVoltageArr.Add(ch.Voltage);
                if(currentTime <= 0){
                    timerText.text = "Done!";
                    minVoltage = (float) averageList(minVoltageArr);
                    minCalibrated = true;
                    timerStarted = false;
                    currentTime = startTime;
                    voltageText.text = "MinVoltage: " + minVoltage.ToString();
                }
            }
        
        }
        if(maxCalibrated && minCalibrated){
            voltageText.text = "Max: " + maxVoltage.ToString() + " Min: " + minVoltage.ToString();
            //Debug.Log("MaxVoltage: " + maxVoltage);
            //Debug.Log("MinVoltage: " + minVoltage);
            float moveDirection = VoltageToMovement();
            rb.velocity = new Vector2(0, (-1)*moveDirection * verticalMoveSpeed);

        }
        

    
    }
    
    void initializePhidget(){
        ch = new VoltageInput();
        ch.Channel = phidgetChannel;
        ch.Open(5000);
    }

    double averageList(List<double> arr){
        double sum = 0;
        foreach(var val in arr){
            sum += val;
        }
        return sum / arr.Count;
    }

    float VoltageToMovement(){
       
       
        
        double currentVoltage = ch.Voltage;
        double medianVoltage = (maxVoltage + minVoltage) /2;
        double voltagePosition = currentVoltage / medianVoltage - 1;
        float moveDirection = (float) voltagePosition;
        return moveDirection;
    }

    
}
