using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;
using System.IO;
using UnityEngine.SceneManagement;


public static class LoggingInfo{
    public static bool loggingStarted = false;
}
public class Logging : MonoBehaviour
{
    // Start is called before the first frame update

    string fullFilePath;
    Scene currScene;
    void Start()
    {
        if(LoggingInfo.loggingStarted == false){

            //make a file
             
            string fileName = "test";
            //string filePath = @Application.streamingAssetsPath + "/Logging_Texts/" + fileName + ".txt";
            string filePath = @"C:\GIT\Research\Research-2022\Flute Hero\Assets\StreamingAssets\Logging_Texts\";
            fullFilePath = filePath + fileName;

            
            File.WriteAllText(fullFilePath, "Creating new Logging File at: " + DateTime.Now.ToString());
            LoggingInfo.loggingStarted = true;
        }
        currScene = SceneManager.GetActiveScene();
        File.WriteAllText(fullFilePath, "Player opened: " + currScene.name + " at: " + DateTime.Now.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        //multiple if statements based on scenes to record?

        if(currScene.name == "AB_Calibration"){

        }
        if(currScene.name == "RIB_Calibration"){

        }
        if(currScene.name == "Bar_Easy"){

        }
        if(currScene.name == "Bar_Medium"){

        }
        if(currScene.name == "Bar_Hard"){

        }

        
    }
}
