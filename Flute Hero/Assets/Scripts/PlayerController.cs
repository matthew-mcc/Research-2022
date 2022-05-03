using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phidget22;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public int phidgetChannel = 0;
    

    VoltageInput ch;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ch = new VoltageInput();
        ch.Channel = phidgetChannel;
        ch.Open(5000);
    }

    // Update is called once per frame
    void Update()
    {
        //float moveDirection = Input.GetAxisRaw("Vertical"); //-1 is down + 1 is up
        float moveDirection = VoltageToMovement();
        
        rb.velocity = new Vector2(0, moveDirection * moveSpeed);
    }

    float VoltageToMovement(){
        
        double currentVoltage = ch.Voltage;
        double maxVoltage = 5;
        double minVoltage = 0;
        double medianVoltage = (maxVoltage + minVoltage) /2;
        double voltagePosition = currentVoltage / medianVoltage - 1;
        float moveDirection = (float) voltagePosition;
        return moveDirection;
    }
}
