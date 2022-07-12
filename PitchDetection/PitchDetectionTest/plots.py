

from signal import valid_signals
from scipy import signal
import numpy as np
import matplotlib.pyplot as plt
import math
xvals = []
vals = []
f = open(R'C:\GIT\Research-2022\PitchDetection\PitchDetectionTest\UsefulData\Guitarc4.txt', 'r')


for val in f:
    #if(float(val) < 440 and float(val) > 430):
    
    # going to attempt some peak detection and only get those values
    
    #if(float(val) < 264):
    vals.append(float(val))

y_mean = [np.mean(vals)]*len(vals)

print(y_mean)

plt.title("C4 - 261.626hz")
plt.ylabel("Input Recording(Hz)")
plt.xlabel("# Samples (time)")
plt.plot(vals)
plt.plot(y_mean, color='red', label=y_mean[0])
plt.legend()
plt.show()
