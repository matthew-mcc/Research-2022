using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject prefab;
    

    [SerializeField] private int numPoints;
    [SerializeField] private float timeBetweenPoints = 1f;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(spawner());
        
    }
    IEnumerator spawner(){


        
        for(int i = 0; i < numPoints; i++){

            spawnPoint();
            yield return new WaitForSeconds(timeBetweenPoints);
        }
        
        

    }
    private void spawnPoint(){
        
        Vector2 spawnPos = rb.transform.position;
        Instantiate(prefab, spawnPos, Quaternion.identity);
        
        

        

    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Implement movement of the square based on data
    }
}
