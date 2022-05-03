using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phidget22;

public class InitialPhidgetTest : MonoBehaviour
{
    
    VoltageInput ch;
    private void Start() {
        ch = new VoltageInput();
        ch.Open(5000);
    }
    private void Update() {
        double Voltage = ch.Voltage;
        Debug.Log(Voltage);
    }
    

			

}

