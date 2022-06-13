using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Phidget22;

public class Calibration : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI voltageText;

    //PlayerController playerController;
    AB_Belt_Controller AbController;
    RIB_Belt_Controller RibController;
    [SerializeField] GameObject abBelt;
    [SerializeField] GameObject ribBelt;
    // Start is called before the first frame update
    void Awake() {
        //playerController = player.GetComponent<PlayerController>();
        AbController = abBelt.GetComponent<AB_Belt_Controller>();
        RibController = ribBelt.GetComponent<RIB_Belt_Controller>();
    }
    void Start()
    {

        voltageText.text = "Up to calibrate Max, Down to Calibrate Min";
        //timerText.text = playerController.currentTime.ToString();
        timerText.text = AbController.currentTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerController.timerStarted && playerController.toCalibrate == "Max"){
        if (AbController.timerStarted && AbController.toCalibrate == "Max"){
            voltageText.text = "AB Max Voltage Calibration!";
        }
        //if (playerController.timerStarted && playerController.toCalibrate == "Min"){
        if (AbController.timerStarted && AbController.toCalibrate == "Min"){
            voltageText.text = "Min Voltage Calibration!";
        }

        //if (playerController.timerStarted){
        if (AbController.timerStarted){

            //if (playerController.toCalibrate == "Max"){
            if (AbController.toCalibrate == "Max"){
                //timerText.text = playerController.currentTime.ToString();
                timerText.text = AbController.currentTime.ToString();
                //if (playerController.currentTime <= 0){
                if (AbController.currentTime <= 0){
                    timerText.text = "Done!";
                    voltageText.text = "MaxVoltage: " + ABBeltInformation.maxVoltage.ToString();
                }

            }
            //if (playerController.toCalibrate == "Min"){
            if (AbController.toCalibrate == "Min"){
                //timerText.text = playerController.currentTime.ToString();
                timerText.text = AbController.currentTime.ToString();
                //if (playerController.currentTime <= 0){
                if (AbController.currentTime <= 0){
                    timerText.text = "Done!";
                    voltageText.text = "MinVoltage: " + ABBeltInformation.minVoltage.ToString();
                }
            }
            
        }
        //if(playerController.maxCalibrated && playerController.minCalibrated){
        if(AbController.maxCalibrated && AbController.minCalibrated){
            voltageText.text = "Max: " + ABBeltInformation.maxVoltage.ToString() + " Min: " + ABBeltInformation.minVoltage.ToString();
        }
    }
}
