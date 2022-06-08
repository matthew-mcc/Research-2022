using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Phidget22;
using UnityEngine.SceneManagement;


public static class PlayerInformation{
    public static bool fullyCalibrated = false;
    public static double maxVoltage = 0.227;
    public static double minVoltage = 0.34;
}   
//For my belts - 0.243, 0.34 seem to be magic numbers - why did it suddenly decide to start working today??

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    
    SpriteRenderer sr;
    
    //Max Min Voltage -> can be serialized Manually
    // public double maxVoltage = 4.01;
    // public double minVoltage = 3.6;

    [SerializeField] public float verticalMoveSpeed = 100f;

    
    //[SerializeField] public float moveSpeedHorizontal = 4f;
    [SerializeField] int phidgetChannel = 0;
    
    //Calibration Flags
    
    public bool maxCalibrated = false;
    public bool minCalibrated = false;
    
    public string toCalibrate;
    private bool calibrationAllowed = false; //May be a way to use this flag to allow callibration only on calibrate level.

    //Timer Information
    private float previousVoltage = 0f;
    public bool timerStarted = false;
    public float currentTime;
    [SerializeField] float calibrationTime = 3f;

    //Lists to average
    private List<double> maxVoltageArr = new List<double>();
    private List<double> minVoltageArr = new List<double>();

    //coloring
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color calibrateColor;

    public VoltageInput ch;
    
    
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        // if(!minCalibrated || !maxCalibrated){
        //     sr.color = calibrateColor;
        // }
        if(!PlayerInformation.fullyCalibrated){
            sr.color = calibrateColor;
        }
        currentTime = calibrationTime;
        initializePhidget();
    }

    // Update is called once per frame
    void Update()
    {
        //previousVoltage = (float) ch.Voltage;
        //Debug.Log("Interval: " + ch.DataInterval);
        //Debug.Log("Max: " + PlayerInformation.maxVoltage);
        //Debug.Log("Min:" + PlayerInformation.minVoltage);
        
        if(minCalibrated && maxCalibrated){
            PlayerInformation.fullyCalibrated = true;
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            timerStarted = true;
            toCalibrate = "Max";
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            timerStarted = true;
            toCalibrate = "Min";
        }
        if (Input.GetKeyDown(KeyCode.S)){
            minCalibrated = true;
            maxCalibrated = true;
        }
        if (Input.GetKey(KeyCode.M)){
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKey(KeyCode.C)){
            SceneManager.LoadScene("Calibration");
        }
        if (Input.GetKey(KeyCode.R)){
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

        if (timerStarted){

            if (toCalibrate == "Max"){
                currentTime -= Time.deltaTime;
                maxVoltageArr.Add(ch.Voltage);

                if(currentTime <= 0){
                    //PlayerInformation.maxVoltage = (float) averageList(maxVoltageArr);
                    //maxVoltage = (float) averageList(maxVoltageArr);
                    maxCalibrated = true;
                    timerStarted = false;
                    currentTime = calibrationTime;
                }
            }

            if (toCalibrate == "Min"){
                currentTime -= Time.deltaTime;
                minVoltageArr.Add(ch.Voltage);

                if (currentTime <= 0){
                    //PlayerInformation.minVoltage = (float) averageList(minVoltageArr);
                    
                    //minVoltage = (float) averageList(minVoltageArr);
                    minCalibrated = true;
                    timerStarted = false;
                    currentTime = calibrationTime;
                }
            }
        }
        
        //if(maxCalibrated && minCalibrated){
        if(PlayerInformation.fullyCalibrated){
            sr.color = defaultColor;
            float moveDirection = VoltageToMovement();
            
            rb.velocity = new Vector2(0, (-1)*moveDirection * verticalMoveSpeed);
            
            
        }

    }
    
    
    //Helper functions
    void initializePhidget(){
        ch = new VoltageInput();
        ch.Channel = phidgetChannel;
        
        ch.Open(5000);
        ch.DataInterval = 50;
        ch.SensorType = VoltageSensorType.Voltage; //Not sure if we need this...
        
    }

    double averageList(List<double> arr){
        double sum = 0;
        foreach(var val in arr){
            sum += val;
        }
        return sum / arr.Count;
    }
    float VoltageToMovement(){
       
       
        
        //double currentVoltage = ch.Voltage;
        double currentVoltage = Math.Round(ch.Voltage, 3);
        
        
        Debug.Log("Current Voltage: " + currentVoltage);
        double medianVoltage = (PlayerInformation.maxVoltage + PlayerInformation.minVoltage) /2;
        double voltagePosition = currentVoltage / medianVoltage - 1;
        float moveDirection = (float) voltagePosition;
        return moveDirection;
    }

    // private void OnParticleCollision(GameObject other) {
    //     Debug.Log("Hit a particle!");
    //     //Iterate score here
    // }

    
    
}
