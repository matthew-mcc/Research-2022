using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame(){
        SceneManager.LoadScene("Data_Level");
    }

    public void CalibrateAB(){
        SceneManager.LoadScene("AB_Calibration");
    }
    public void CalibrateRIB(){
        SceneManager.LoadScene("RIB_Calibration");
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

    // Hoop Levels

    //Ab
    public void Ab_Hoop_Easy(){
        SceneManager.LoadScene("Ab_Hoop_Easy");
    }
    public void Ab_Hoop_Med(){
        SceneManager.LoadScene("Ab_Hoop_Medium");
    }
    public void Ab_Hoop_Hard(){
        SceneManager.LoadScene("Ab_Hoop_Hard");
    }

    //Rib
    public void Rib_Hoop_Easy(){
        SceneManager.LoadScene("Rib_Hoop_Easy");
    }
    public void Rib_Hoop_Med(){
        SceneManager.LoadScene("Rib_Hoop_Medium");
    }
    public void Rib_Hoop_Hard(){
        SceneManager.LoadScene("Rib_Hoop_Hard");
    }

    //Bar Levels
    public void Bar_Easy(){
        SceneManager.LoadScene("Bar_Easy");
    }
    public void Bar_Medium(){
        SceneManager.LoadScene("Bar_Medium");
    }
    public void Bar_Hard(){
        SceneManager.LoadScene("Bar_Hard");
    }
}
