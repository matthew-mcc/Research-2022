import time
from Phidget22.PhidgetException import *
from Phidget22.Phidget import *
from Phidget22.Devices.VoltageInput import *

ch = VoltageInput()
ch.openWaitForAttachment(5000)
ch.open()

while True:

    minVoltage = ch.getMinVoltage() # 0
    maxVoltage = ch.getMaxVoltage() # 5

    medianVoltage = minVoltage + maxVoltage / 2

    current_voltage = ch.getVoltage()

    #print(current_voltage)

    #between 0.1 and -0.1 should be mid value
    threshold = 1
    factor = 480 / medianVoltage
    voltagePosition = (current_voltage) * factor /2
    

    if voltagePosition <= threshold and voltagePosition >= -threshold:
        print('mid: ' + str(voltagePosition))

    else:
        if voltagePosition > 0:
            print('up: ' + str(voltagePosition))
        if voltagePosition < 0:
            print('down: ' + str(voltagePosition))
    

    time.sleep(1)

    