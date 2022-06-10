using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Phidget22;
using UnityEngine.SceneManagement;


//Static class to hold information about the player - namely calibration information
public static class PlayerInformation
{
    public static bool fullyCalibrated = false;
    public static double maxVoltage = 3.6;
    public static double minVoltage = 3.35;
    
    
}   



public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    
    SpriteRenderer sr;
    
    

    [SerializeField] public float verticalMoveSpeed = 100f;
    
    
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
        
        if(!PlayerInformation.fullyCalibrated){
            sr.color = calibrateColor;
        }
        currentTime = calibrationTime;
        initializePhidget();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Always checking for full calibration
        if(minCalibrated && maxCalibrated){
            PlayerInformation.fullyCalibrated = true;
        }
        
        //Calibrate max keybind
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            timerStarted = true;
            toCalibrate = "Max";
        }
        
        //Calibrate min keybind
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            timerStarted = true;
            toCalibrate = "Min";
        }

        //cheatcode to skip calibration...
        if (Input.GetKeyDown(KeyCode.S)){
            minCalibrated = true;
            maxCalibrated = true;
        }

        //Main menu keybind
        if (Input.GetKey(KeyCode.M)){
            SceneManager.LoadScene("Menu");
        }

        //Calibration scene keybind
        if (Input.GetKey(KeyCode.C)){
            SceneManager.LoadScene("Calibration");
        }

        //Reload level keybind
        if (Input.GetKey(KeyCode.R)){
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

        //Main calibration statement
        if (timerStarted){

            //Calibrating Max
            if (toCalibrate == "Max"){
                currentTime -= Time.deltaTime;
                maxVoltageArr.Add(ch.Voltage);

                if(currentTime <= 0){
                    PlayerInformation.maxVoltage = (float) averageList(maxVoltageArr);
                    float maxVoltage = (float) averageList(maxVoltageArr);
                    maxCalibrated = true;
                    timerStarted = false;
                    currentTime = calibrationTime;
                }
            }

            //Calibrating Min
            if (toCalibrate == "Min"){
                currentTime -= Time.deltaTime;
                minVoltageArr.Add(ch.Voltage);

                if (currentTime <= 0){
                    PlayerInformation.minVoltage = (float) averageList(minVoltageArr);
                    
                    float minVoltage = (float) averageList(minVoltageArr);
                    minCalibrated = true;
                    timerStarted = false;
                    currentTime = calibrationTime;
                }
            }
        }
        
        //Unlocking Movement if BOTH max and min are calibrated (fullyCalibrated)
        if(PlayerInformation.fullyCalibrated){
            Debug.Log(ch.Voltage);
            sr.color = defaultColor;
            
            Vector2 currentPos = rb.transform.position;
            Vector2 endPos = new Vector2(-6.5f, VoltageToPosition());
            
            rb.transform.position = Vector2.Lerp(currentPos, endPos, Time.deltaTime); //woot woot this is mucho bueno 
           
            
            
        }

    }
    
    
    //Initializing Phidget Function -- Called in Start
    void initializePhidget(){
        ch = new VoltageInput();
        ch.Channel = phidgetChannel;
        
        ch.Open(5000);
        ch.VoltageChangeTrigger = 0;
        ch.DataInterval = 50;
        ch.SensorType = VoltageSensorType.Voltage; //Not sure if we need this...
    }

    //Helper function to take the average of a list
    double averageList(List<double> arr){
        double sum = 0;
        foreach(var val in arr){
            sum += val;
        }
        return sum / arr.Count;
    }
    

    //Gets the current voltage value, scales it to a position relative to the scene's scale and then linearly interpolates to that position
    float VoltageToPosition(){
        
        //ASK LUCIE REGARDING THE ROUNDING OF VOLTAGE... 3 DIGITS SHOULD BE JUST FINE
        double currentVoltage = Math.Round(ch.Voltage, 3);
        

        /*
        Formula for Normalization: https://stats.stackexchange.com/questions/281162/scale-a-number-between-a-range
        
        xNormalized = (b - a) * (x - min(x)) / (max(x) - min(x)) + a
        
        Where: scaling some var x into the range [a, b]
        */

        double movePos = (5 - -5) * ((currentVoltage - PlayerInformation.minVoltage) / (PlayerInformation.maxVoltage - PlayerInformation.minVoltage)) + -5;

        Debug.Log(movePos);

        //Error checking against extremely odd values, ensure that can never go off screen.
        if(movePos > 5){
            return 5.0f;
        }
        if(movePos < -5){
            return -5.0f;
        }
        else{
            return (float) movePos;
        }
        
        
    }

    
    
}
