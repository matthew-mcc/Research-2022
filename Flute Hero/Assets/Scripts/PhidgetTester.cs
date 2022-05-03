using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phidget22;

public class PhidgetTester : MonoBehaviour
{

    VoltageInput ch;
    public int PhidgetChannel;
    private void Start() {
        ch = new VoltageInput();
        ch.Channel = PhidgetChannel;
        ch.Open(5000);

    }
    private void Update() {
        double currentVoltage = ch.Voltage;
        Debug.Log(ch.ChannelName);
        //Debug.Log(currentVoltage);
    }
}
