import time
from Phidget22.PhidgetException import *
from Phidget22.Phidget import *
from Phidget22.Devices.VoltageInput import *




"""
Our phidget pot is on voltage input 0 - we can check using the phidget control panel 
So far this program only works for input 0 - let us see how we can determine diff input things
"""


def onAttachHandler(self):
    print("Phidget Attached!")

def onVoltageChange(self, voltage):
    print("Voltage: " + str(voltage))


def main():
    ch = VoltageInput()

    ch.setOnAttachHandler(onAttachHandler)
    ch.openWaitForAttachment(5000)

   
    #voltage = ch.getVoltage() # correctly gets voltage
    
    ch.setOnVoltageChangeHandler(onVoltageChange)
    
    
    ch.open()
    while True:

        voltage = ch.getVoltage()
        print("Voltage: " + str(voltage))
        time.sleep(1)


    

 


main()
    