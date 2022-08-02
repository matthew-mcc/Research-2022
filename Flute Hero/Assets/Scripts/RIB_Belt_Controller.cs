using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phidget22;
using System;
using UnityEngine.SceneManagement;


public static class RibBeltInformation{
    public static bool fullyCalibrated = false;
    public static double maxVoltage = 3.6;
    public static double minVoltage = 3.35;
    public static int port = 2;
}
public class RIB_Belt_Controller : MonoBehaviour
{

    // Start is called before the first frame update
    public Rigidbody2D rb;
    
    SpriteRenderer sr;
    Scene currentScene;
    
    private float maxRange = 3f;
    private float minRange = -4f;

    [SerializeField] public float verticalMoveSpeed = 100f;
    
    
    int phidgetChannel = 2;
    [SerializeField] float defaultXPos = -6.5f;
    
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

   
    public float currentVoltageForText;
    public VoltageInput ch;

    public float newCurrentVoltage = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentScene = SceneManager.GetActiveScene();
        
        int phidgetChannel = RibBeltInformation.port;
        currentTime = calibrationTime;
        initializePhidget();
    }

    // Update is called once per frame
    void Update()
    {
        //Always checking for full calibration

        currentVoltageForText = newCurrentVoltage;
        if(minCalibrated && maxCalibrated){
            RibBeltInformation.fullyCalibrated = true;
        }
        //Calibrate max keybind

        if(currentScene.name == "RIB_Calibration"){
            if (Input.GetKey(KeyCode.UpArrow)){
            timerStarted = true;
            toCalibrate = "Max";
            }
            //Calibrate min keybind
            if (Input.GetKey(KeyCode.DownArrow)){
                timerStarted = true;
                toCalibrate = "Min";
            }
        }
        
        

        //Main calibration statement
        if (timerStarted){

            //Calibrating Max
            if (toCalibrate == "Max"){
                currentTime -= Time.deltaTime;
                maxVoltageArr.Add(newCurrentVoltage);

                if(currentTime <= 0){
                    RibBeltInformation.maxVoltage = (float) averageList(maxVoltageArr);
                    float maxVoltage = (float) averageList(maxVoltageArr);
                    maxCalibrated = true;
                    timerStarted = false;
                    currentTime = calibrationTime;
                }
            }

            //Calibrating Min
            if (toCalibrate == "Min"){
                currentTime -= Time.deltaTime;
                minVoltageArr.Add(newCurrentVoltage);

                if (currentTime <= 0){
                    RibBeltInformation.minVoltage = (float) averageList(minVoltageArr);
                    
                    float minVoltage = (float) averageList(minVoltageArr);
                    minCalibrated = true;
                    timerStarted = false;
                    currentTime = calibrationTime;
                }
            }
        }
        //Unlocking Movement if BOTH max and min are calibrated (fullyCalibrated)
        if(RibBeltInformation.fullyCalibrated){
            //Debug.Log(ch.Voltage);
            
            
            Vector2 currentPos = rb.transform.position;
            Vector2 endPos = new Vector2(rb.transform.position.x, VoltageToPosition());
            
            rb.transform.position = Vector2.Lerp(currentPos, endPos, Time.deltaTime); //woot woot this is mucho bueno 
           
            
            
        }
    }

    //Initializing Phidget Function -- Called in Start
    void initializePhidget(){
        ch = new VoltageInput();
        ch.Channel = phidgetChannel;
        ch.Error+=phidgetErrorHandler;
        ch.VoltageChange += voltageChange;
        try{
            ch.Open(5000);
        }
        catch (PhidgetException ex){
            Debug.Log("Failed to open: " + ex.Description);
        }
        
        ch.VoltageChangeTrigger = 0;
        ch.DataInterval = 50;
        ch.SensorType = VoltageSensorType.Voltage; //Not sure if we need this...
    }

    void phidgetErrorHandler(object sender, Phidget22.Events.ErrorEventArgs e){
        Debug.Log("Code: " + e.Code.ToString());
        Debug.Log("Description: " + e.Description);
    }

    public void voltageChange(object sender, Phidget22.Events.VoltageInputVoltageChangeEventArgs e){
        float voltage = (float) e.Voltage;
        newCurrentVoltage = voltage;
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
        
        double currentVoltage = Math.Round(newCurrentVoltage, 3);
        

        /*
        Formula for Normalization: https://stats.stackexchange.com/questions/281162/scale-a-number-between-a-range
        
        xNormalized = (b - a) * (x - min(x)) / (max(x) - min(x)) + a
        
        Where: scaling some var x into the range [a, b]
        */

        double movePos = (maxRange - minRange) * ((currentVoltage - RibBeltInformation.minVoltage) / (RibBeltInformation.maxVoltage - RibBeltInformation.minVoltage)) + minRange;

        //Debug.Log(movePos);

        //Error checking against extremely odd values, ensure that can never go off screen.
        if(movePos > maxRange){
            return (float) maxRange;
        }
        if(movePos < minRange){
            return (float) minRange;
        }
        else{
            return (float) movePos;
        }
        
        
    }
    // private void OnApplicationQuit() {
    //     ch.VoltageChange-= voltageChange;
    //     ch.Close();
    //     ch = null;

        
    // }
}
