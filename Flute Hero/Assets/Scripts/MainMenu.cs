using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame(){
        SceneManager.LoadScene("Data_Level");
    }

    public void Calibrate(){
        SceneManager.LoadScene("Calibration");
    }

    public void QuitGame(){
        Debug.Log("Quitting game!");
        Application.Quit();
    }
    public void LevelOne(){
        SceneManager.LoadScene("Level_1");
    }
    public void LevelTwo(){
        SceneManager.LoadScene("Level_2");
    }
    
    public void LevelThree(){
        SceneManager.LoadScene("Level_3");
    }
    
    public void LevelFour(){
        SceneManager.LoadScene("Plane_Hoops");
    }
}
