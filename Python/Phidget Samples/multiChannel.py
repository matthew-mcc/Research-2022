import time
from Phidget22.PhidgetException import *
from Phidget22.Phidget import *
from Phidget22.Devices.VoltageInput import *



def onAttachHandler(self):
    print("Phidget Attached!")

def onVoltageChange(self, voltage):
    print("Voltage: " + str(voltage))


def main():
    ch = [VoltageInput() for i in range(0, 8)]
    for i in range(0, 8):
        ch[i].setChannel(i)
        ch[i].openWaitForAttachment(5000) # not really sure what this line does yet

    #ch[0].setOnVoltageChangeHandler(onVoltageChange)
    #ch[1].setOnVoltageChangeHandler(onVoltageChange)

    
    while True:
        voltage1 = ch[0].getVoltage()
        voltage2 = ch[1].getVoltage()

        print("Voltage1: " + str(voltage1) + " Voltage2: " + str(voltage2))
        
        time.sleep(1)

main()