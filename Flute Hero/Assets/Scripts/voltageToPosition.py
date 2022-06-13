import time
from Phidget22.PhidgetException import *
from Phidget22.Phidget import *
from Phidget22.Devices.VoltageInput import *
from pyrsistent import b

ch_blue = VoltageInput()
ch_red = VoltageInput()
ch_blue.setChannel(1)
ch_blue.openWaitForAttachment(5000)

ch_red.setChannel(2)
ch_red.openWaitForAttachment(5000)

while True:

    

    
    
    ##current_voltage = ch.getVoltage()
    blue_voltage = ch_blue.getVoltage()
    red_voltage = ch_red.getVoltage()

    blue_voltage = str(blue_voltage)
    red_voltage = str(red_voltage)

    print("Blue: " + blue_voltage + " Red: " + red_voltage)
    
    time.sleep(0.5)
    #print(current_voltage)

    