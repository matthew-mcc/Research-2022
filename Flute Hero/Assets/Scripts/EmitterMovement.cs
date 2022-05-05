using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject prefab;
    
    string filePath = @"C:\GIT\Research-2022\20220316112102AbBelt.txt";

    List<float> voltageFromFile = new List<float>();

    [SerializeField] private int numPoints;
    [SerializeField] private float timeBetweenPoints = 1f;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        readFile();
        StartCoroutine(spawner());
        StartCoroutine(moveEmitter());
        
    }
    IEnumerator spawner(){


        
        for(int i = 0; i < numPoints; i++){

            spawnPoint();
            yield return new WaitForSeconds(timeBetweenPoints);
        }
        
        

    }
    IEnumerator moveEmitter(){


        foreach(var val in voltageFromFile){
            rb.transform.position = new Vector2(rb.transform.position.x, val); //Needs to be scaled a bit, dependant on what lucie wants
            yield return new WaitForSeconds(1/60);
        }
    }
    private void spawnPoint(){
        
        Vector2 spawnPos = rb.transform.position;
        Instantiate(prefab, spawnPos, Quaternion.identity);
        
        

        

    }
    private void readFile(){
        string[] lines = System.IO.File.ReadAllLines(filePath);
        // foreach (string line in lines){
        //     Debug.Log(line);
        // }
        for (int i = 1; i < 100; i++){ //change if u want diff nums of lines...
            string temp = lines[i];
            
            string[] splitWords = temp.Split('	');
            
            
           
            float value = float.Parse(splitWords[1]);
            voltageFromFile.Add(value);
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //TODO: Implement movement of the square based on data
    }
}
