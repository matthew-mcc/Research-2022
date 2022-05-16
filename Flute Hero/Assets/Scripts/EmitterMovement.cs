using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EmitterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject prefab;
    
    string filePath = @"C:\GIT\Research-2022\20220316112102AbBelt.txt";

    List<float> voltageFromFile = new List<float>();

    [SerializeField] private int numPoints;
    [SerializeField] private float timeBetweenPoints = 1f;
    [SerializeField] private int numLines = 1000;

    [SerializeField] private float inputMax = 3.74f;
    [SerializeField] private float inputMin = 3.67f;
    [SerializeField] private float tickRate = 0.0167f;
    [SerializeField] private float hMovespeed = 4f;

    

    



    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        readFile();
       
       //StartCoroutine(spawner());
        StartCoroutine(moveEmitter());
        
        
    }
    IEnumerator spawner(){


        
        for(int i = 0; i < numPoints; i++){

            spawnPoint();
            yield return new WaitForSeconds(timeBetweenPoints);
        }
        
        

    }
    IEnumerator moveEmitter(){
        Vector2 EndPos;
        Vector2 currentPos;
        
        foreach(var val in voltageFromFile.Take(numLines)){
            float elapsedTime = 0;
            float waitTime = tickRate;
            currentPos = rb.transform.position;
            EndPos = new Vector2(rb.transform.position.x, val); //delete the + factor if you want to go back to original movement
           
            //float angle = Mathf.Atan2(EndPos.y, EndPos.x) * Mathf.Rad2Deg;
            //rb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            while (elapsedTime < waitTime){
                
                rb.transform.position = Vector2.Lerp(currentPos, EndPos, (elapsedTime / waitTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            //rb.transform.position = new Vector2(rb.transform.position.x, val); //Needs to be scaled a bit, dependant on what lucie wants
            //yield return new WaitForSeconds(tickRate); //1/60 was giving wrong val
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
        float scaledMovePos;
        
        foreach(string line in lines){ //change if u want diff nums of lines...
            string temp = line;
            string[] splitWords = temp.Split('	');
            try{
                float value = float.Parse(splitWords[1]);
                if(value > 3){

                    //scaling function used to be in the coroutine which moves the emitter
                    if(value > 3.4){
                        scaledMovePos = ((value - inputMin) / (inputMax - inputMin)) * 8;
                        scaledMovePos = (-1) * (scaledMovePos-4);
                        voltageFromFile.Add(scaledMovePos);
                    }
                        
                    //Debug.Log(value);
                }
            }
            catch{
                Debug.Log("Can't convert that to a float!");
            }

            
            
        }
        
    }

  
    

    

    // Update is called once per frame
    void Update()
    {
         
        //TODO: Implement movement of the square based on data
        //rb.transform.Translate(Vector2.right * 2 * Time.deltaTime);
        //rb.transform.LookAt();
    }
}
