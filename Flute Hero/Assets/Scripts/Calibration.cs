using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Phidget22;

public class Calibration : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI voltageText;

    PlayerController playerController;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Awake() {
        playerController = player.GetComponent<PlayerController>();
    }
    void Start()
    {

        voltageText.text = "Up to calibrate Max, Down to Calibrate Min";
        timerText.text = playerController.currentTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.timerStarted && playerController.toCalibrate == "Max"){
            voltageText.text = "Max Voltage Calibration!";
        }
        if (playerController.timerStarted && playerController.toCalibrate == "Min"){
            voltageText.text = "Min Voltage Calibration!";
        }

        if (playerController.timerStarted){

            if (playerController.toCalibrate == "Max"){
                timerText.text = playerController.currentTime.ToString();
                if (playerController.currentTime <= 0){
                    timerText.text = "Done!";
                    voltageText.text = "MaxVoltage: " + PlayerInformation.maxVoltage.ToString();
                }

            }
            if (playerController.toCalibrate == "Min"){
                timerText.text = playerController.currentTime.ToString();
                if (playerController.currentTime <= 0){
                    timerText.text = "Done!";
                    voltageText.text = "MinVoltage: " + PlayerInformation.minVoltage.ToString();
                }
            }
            
        }
        if(playerController.maxCalibrated && playerController.minCalibrated){
            voltageText.text = "Max: " + PlayerInformation.maxVoltage.ToString() + " Min: " + PlayerInformation.minVoltage.ToString();
        }
    }
}
