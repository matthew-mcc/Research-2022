using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleHandler : MonoBehaviour
{

    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float timeBetweenWalls = 2f;
    [SerializeField] float heightThreshold = 0f;
    
    // Start is called before the first frame update

    //get some othe rway
    float wallHeight = 6f;

    
    List<float> positions = new List<float>(); //TODO read from file
    [SerializeField] string filePath = @"C:\GIT\Research-2022\Level_Texts\level_1.txt";



    void Start()
    {
        wallHeight = obstaclePrefab.transform.localScale.y;
        
        readFile();
        StartCoroutine(spawner(positions));
        
    }

    private void readFile()
    {
        string[] lines = System.IO.File.ReadAllLines(filePath);
        foreach(string line in lines){
            try{
                float value = float.Parse(line);
                positions.Add(value);
            }
            catch{
                Debug.Log("Can't convert that to a float!");
            }
            
        }
    }

    IEnumerator spawner(List<float> positions){

        foreach(float pos in positions){
            
            if(pos == 1000){
                yield return new WaitForSeconds(10);
                SceneManager.LoadScene("Menu");
            }
            else{
                spawnObstacle(pos);
                yield return new WaitForSeconds(timeBetweenWalls);
            }
            
            
        }
    }


    void spawnObstacle(float yPos){
        Vector2 spawnPos = new Vector2(15, (yPos - (wallHeight/2)) + heightThreshold); //scales so that the top of the wall goes where we want
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }
}
