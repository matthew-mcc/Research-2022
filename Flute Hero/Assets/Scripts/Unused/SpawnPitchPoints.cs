using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPitchPoints : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector2 spawnPos;
    [SerializeField] private int numPoints;
    [SerializeField] private float timeBetweenPoints = 1f;
    
    [SerializeField] private bool isRandom = true;
    
    
    private void Start() {
        StartCoroutine(spawner());
    }
    

    IEnumerator spawner(){


        
        for(int i = 0; i < numPoints; i++){

            spawnPoint();
            yield return new WaitForSeconds(timeBetweenPoints);
        }
        
        

    }

    private void spawnPoint(){

        if(isRandom){
            float y = Random.Range(-5, 5);
            Instantiate(prefab, new Vector2(spawnPos.x, y), Quaternion.identity);
        }
        
        else{
            
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
        

        

    }

    
}
