import time
from Phidget22.PhidgetException import *
from Phidget22.Phidget import *
from Phidget22.Devices.VoltageInput import *

ch = VoltageInput()

ch.setChannel(1)
ch.openWaitForAttachment(5000)

while True:

    

    
    print(ch.getDataInterval())
    current_voltage = ch.getVoltage()
    
    time.sleep(0.25)
    #print(current_voltage)

    