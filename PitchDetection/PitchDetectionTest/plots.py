

from scipy import signal
import numpy as np
import matplotlib.pyplot as plt
import math
xvals = []
vals = []
f = open(R'C:\GIT\Research-2022\PitchDetection\PitchDetectionTest\new.txt', 'r')


for val in f:
    #if(float(val) < 440 and float(val) > 430):
    
    # going to attempt some peak detection and only get those values

    vals.append(float(val))
    



vals = np.array(vals)
peaks = signal.find_peaks(vals, 430, 5, 1)
peaks = peaks[1]['peak_heights']

for i in range(len(peaks)):
    xvals.append(i)

peaks = peaks.tolist()

def convertListToMidi(arr):
    midiArr = []
    for val in arr:
        var = 69 + 12*math.log2(val/440)
        midiArr.append(var)
    return midiArr


plt.ylabel("A440hz - recording (freq)")
plt.xlabel("time")
plt.scatter(xvals, peaks)
plt.ylim([380, 440])
plt.show()
# midivals = convertListToMidi(vals)
# plt.ylabel("A440hz - recording (midi)")
# plt.xlabel("time")
# plt.plot(xvals, midivals)
# plt.ylim([68.5, 69])
# plt.show()