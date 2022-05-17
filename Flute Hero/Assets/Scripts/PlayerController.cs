using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phidget22;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    
    private SpriteRenderer rend;
    [SerializeField] public float moveSpeed = 100f;
    //[SerializeField] public float moveSpeedHorizontal = 4f;
    public int phidgetChannel = 0;
    

    VoltageInput ch;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        initializePhidget();
    }

    // Update is called once per frame
    void Update()
    {
        //float moveDirection = Input.GetAxisRaw("Vertical"); //-1 is down + 1 is up
        float moveDirection = VoltageToMovement();
        //float moveDirection = Input.GetAxisRaw("Vertical");
        // Debug.Log(moveDirection);
        // if(moveDirection > 0){
        //     //change color to up
        //     rend.material.SetColor("_Color", Color.red);
        // }
        // if(moveDirection < 0 ){
        //     //change color to down
        // }
        
        rb.velocity = new Vector2(0, moveDirection * moveSpeed);
    
    }
    
    
    void initializePhidget(){
        ch = new VoltageInput();
        ch.Channel = phidgetChannel;
        ch.Open(5000);
    }
    float VoltageToMovement(){
       
       //If needed will have to scale the voltage moreso here...

        double currentVoltage = ch.Voltage;
        double maxVoltage = 5;
        double minVoltage = 0;
        double medianVoltage = (maxVoltage + minVoltage) /2;
        double voltagePosition = currentVoltage / medianVoltage - 1;
        float moveDirection = (float) voltagePosition;
        return moveDirection;
    }

    private void OnParticleCollision(GameObject other) {
        Debug.Log("Hit a particle!");
        //Iterate score here
    }

    
    
}
