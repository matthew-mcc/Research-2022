using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Phidget22;

public class Calibration : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI abText;
    [SerializeField] TextMeshProUGUI ribText;

    //PlayerController playerController;
    AB_Belt_Controller AbController;
    RIB_Belt_Controller RibController;
    [SerializeField] GameObject abBelt;
    [SerializeField] GameObject ribBelt;
    // Start is called before the first frame update --> Maybe make this into start ?? Why is it in awake
    void Awake() {
        //playerController = player.GetComponent<PlayerController>();
        AbController = abBelt.GetComponent<AB_Belt_Controller>();
        RibController = ribBelt.GetComponent<RIB_Belt_Controller>();
    }
    void Start()
    {

        ribText.text = "Up+j to calibrate max, up+j to calibrate min";
        abText.text = "Up+a to calibrate max, up+a to calibrate min";
        //timerText.text = playerController.currentTime.ToString();
        //timerText.text = AbController.currentTime.ToString();
        timerText.text = "Timer!";
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerController.timerStarted && playerController.toCalibrate == "Max"){
        if (AbController.timerStarted && AbController.toCalibrate == "Max"){
            abText.text = "AB Max Voltage Calibration!";
        }
        //if (playerController.timerStarted && playerController.toCalibrate == "Min"){
        if (AbController.timerStarted && AbController.toCalibrate == "Min"){
            abText.text = "AB Min Voltage Calibration!";
        }
        if (RibController.timerStarted && RibController.toCalibrate == "Max"){
            ribText.text = "Rib Max Voltage Calibration!";
        }
        if (RibController.timerStarted && RibController.toCalibrate == "Min"){
            ribText.text = "Rib Min Voltage Calibration!";
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
                    abText.text = "AB MaxVoltage: " + ABBeltInformation.maxVoltage.ToString();
                }

            }
            //if (playerController.toCalibrate == "Min"){
            if (AbController.toCalibrate == "Min"){
                //timerText.text = playerController.currentTime.ToString();
                timerText.text = AbController.currentTime.ToString();
                //if (playerController.currentTime <= 0){
                if (AbController.currentTime <= 0){
                    timerText.text = "Done!";
                    abText.text = "AB MinVoltage: " + ABBeltInformation.minVoltage.ToString();
                }
            }
            
        }

        if (RibController.timerStarted){

            if(RibController.toCalibrate == "Max"){
                timerText.text = RibController.currentTime.ToString();

                if (RibController.currentTime <= 0){
                    timerText.text = "Done!";
                    ribText.text = "Rib MaxVoltage: " + RibBeltInformation.maxVoltage.ToString();
                }
            }

            if(RibController.toCalibrate == "Min"){
                timerText.text = RibController.currentTime.ToString();

                if (RibController.currentTime <= 0){
                    timerText.text = "Done!";
                    ribText.text = "Rib MinVoltage: " + RibBeltInformation.minVoltage.ToString();
                }
            }
        }
        //if(playerController.maxCalibrated && playerController.minCalibrated){
        if(AbController.maxCalibrated && AbController.minCalibrated){
            abText.text = "AB --> Max: " + ABBeltInformation.maxVoltage.ToString() + " Min: " + ABBeltInformation.minVoltage.ToString();
        }

        if(RibController.maxCalibrated && RibController.minCalibrated){
            ribText.text = "Rib --> Max: " + RibBeltInformation.maxVoltage.ToString() + " Min: " + RibBeltInformation.minVoltage.ToString();
        }
    }
}
