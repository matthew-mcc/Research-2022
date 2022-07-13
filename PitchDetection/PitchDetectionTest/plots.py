

from signal import valid_signals
from scipy import signal, fft
import numpy as np
import matplotlib.pyplot as plt
import math
vals2 = []
vals1 = []
f = open(R'C:\GIT\Research-2022\PitchDetection\PitchDetectionTest\UsefulData\A4_After_Packing.txt', 'r')


for val in f:
    #if(float(val) < 440 and float(val) > 430):
    
    # going to attempt some peak detection and only get those values
    
    #if(float(val) < 264):
    vals1.append(float(val))


#y_mean = [np.mean(vals)]*len(vals)

# f1 = open(R'C:\GIT\Research-2022\PitchDetection\PitchDetectionTest\UsefulData\corrected.mat', 'r')

# for val in f1:

#     vals2.append(float(val))


plt.title("A4 After Packing")
plt.ylabel("Sample Value")
plt.xlabel("# Samples (time)")
plt.xlim([0, 5000])
plt.plot(vals1, 'b', label = "Corrected2.txt",)
#plt.plot(vals2, 'r', label = "Corrected.mat")
#plt.plot(y_mean, color='red', label=y_mean[0])
plt.legend()
plt.show()
