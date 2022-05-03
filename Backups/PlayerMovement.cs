using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phidget22;

public class PlayerMovement : MonoBehaviour

{
    public float xMoveSpeed = 5f;
    public float moveSpeed;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;

    VoltageInput ch;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        ch = new VoltageInput();
        ch.Open(5000);
    }
    

    // Update is called once per frame
    void Update()
    {
        //moveDirection = Input.GetAxis("Horizontal"); // 1 goes to right, -1 goes to left. We need to convert the voltage numbers to be -1 and 1

        double currentVoltage = ch.Voltage; // gives us a value between 0 and 5
        double maxVoltage = 5;
        double minVoltage = 0;

        double medianVoltage = (maxVoltage + minVoltage) /2;

        double voltagePosition = currentVoltage / medianVoltage - 1;
        voltagePosition = voltagePosition * -1;
        moveDirection = (float)voltagePosition;
        //sDebug.Log(voltagePosition);
        // if the voltage is greater than median, move right
        transform.position += Vector3.right * Time.deltaTime * xMoveSpeed;
        
        // if the voltage is less than median, move left
        //Debug.Log(voltagePosition);

        rb.velocity = new Vector2(rb.velocity.x, moveDirection * moveSpeed);
        
    }
}
