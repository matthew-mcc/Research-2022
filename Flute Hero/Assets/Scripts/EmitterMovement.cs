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

        
        foreach(var val in voltageFromFile.Take(numLines)){
            float movePos = val;
            
            if(val > 3.4){
                //we care about this value so lets scale it


                movePos = ((val - inputMin) / (inputMax - inputMin)) * 10;
                
                
            }

           
            rb.transform.position = new Vector2(rb.transform.position.x, (-1)*(movePos - 5)); //Needs to be scaled a bit, dependant on what lucie wants
            //Debug.Log(val);
            yield return new WaitForSeconds(tickRate); //1/60 was giving wrong val
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
        foreach(string line in lines){ //change if u want diff nums of lines...
            string temp = line;
            string[] splitWords = temp.Split('	');
            try{
                float value = float.Parse(splitWords[1]);
                if(value > 3){
                    voltageFromFile.Add(value);
                    //Debug.Log(value);
                }
            }
            catch{
                Debug.Log("Can't convert that to a float!");
            }
        

            // Need to have +5 y and -5 y as the range
            // so therefore the 3.8 should get converted to 5, the 3.3 should get converted to -5
            //Also need to invert the movement...

            /*
                Currently I have values between like 3 and 4, but I need them to be scaled to -5 closer to 4 and 5 closer to 3.
                Might wanna tweak it to be between 3.5 and 3.8 depends on the other data, make it serialized
                https://stats.stackexchange.com/questions/281162/scale-a-number-between-a-range
            */

            
            
        }
        
    }

  
    

    

    // Update is called once per frame
    void Update()
    {
        
        //TODO: Implement movement of the square based on data
    }
}
