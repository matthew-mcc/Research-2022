using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HoopController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject hoop;
    [SerializeField] private float timeBetweenJump;
    [SerializeField] private float timeBetweenHold;

    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    

    [SerializeField] float startHoopThreshold = 0f;
    [SerializeField] float endHoopThreshold = 0f;

    [SerializeField] private float startingXPos = 15f;

    [SerializeField] string filePath = @"C:\GIT\Research\Research-2022\Flute Hero\Assets\Level Texts\AB_Hoop_Easy.txt";

    private SpriteRenderer sr;

    private bool CR_Running = false;

    [SerializeField] TextMeshProUGUI infoText;

    //For now data will look like --> (string, float) --> (start, hoopYpos) or (end, hoopYPos)
    //List<(string, float, string, float)> positions = new List<(string, float, string, float)>();
    List<float> positions = new List<float>();
    float hoopSize = 2f;
    void Start()
    {
       
        hoopSize = hoop.transform.localScale.y;
        sr = hoop.GetComponent<SpriteRenderer>();

        
        

        ReadFile();
        
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && CR_Running == false){
            StartCoroutine(spawnHoops());
            
           
        }
        else if(Input.GetKeyDown(KeyCode.Space) && CR_Running == true){
            infoText.text = "Targets Already Spawned!";
            
        }
    }

    private void ReadFile()
    {
        string[] lines = System.IO.File.ReadAllLines(filePath);
        foreach(string line in lines){
            
            positions.Add(float.Parse(line));
            
            
        }
        
    }

    IEnumerator spawnHoops(){
       


        // }
        CR_Running = true;
        for(int i = 0; i < positions.Count; i ++){
            if(i == 0){
                spawnHoop(positions[i], "Start");
                infoText.text = "Good Luck!";
                yield return new WaitForSeconds(timeBetweenJump);
            }
            else if(positions[i] == 1000){
                yield return new WaitForSeconds(5);
                CR_Running = false;
                
            }
            else{
                infoText.text = "";
                spawnHoop(positions[i], "End");
                yield return new WaitForSeconds(timeBetweenHold);
            }

        }
    }
    

    void spawnHoop(float yPos, string identifier){
        if(identifier == "Start"){
            Vector2 spawnPos = new Vector2(startingXPos, (yPos - (hoopSize/2)) + startHoopThreshold);
            sr.color = startColor;
            Instantiate(hoop, spawnPos, Quaternion.identity);
        }
        if(identifier == "End"){
            Vector2 spawnPos = new Vector2(startingXPos, (yPos - (hoopSize/2)) + endHoopThreshold);
            sr.color = endColor;
            Instantiate(hoop, spawnPos, Quaternion.identity);
        }
    } 
}
