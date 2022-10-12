using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Main menu keybind
        if (Input.GetKey(KeyCode.M)){
            SceneManager.LoadScene("Menu");
            Scene scene = SceneManager.GetActiveScene();
            Debug.Log("Going Back to Menu From Scene: " + scene.name);
            
        }

        

        //Reload level keybind
        if (Input.GetKey(KeyCode.R)){
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }
}
