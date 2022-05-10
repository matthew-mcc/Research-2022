

from matplotlib import pyplot as plt
nums = []

f = open(R"C:\GIT\Research-2022\20220316112102AbBelt.txt", "r")

lines = f.readlines()


for line in lines:
    test = line.split()
    if len(nums) > 1000:
        break

    # rounding to 3 decimals is promising...
    if len(test) == 2:
        #3.74 and 3.67 are the ranges for the first 1k
        
        if float(test[1]) > 3.4:
            #temp = round(float(test[1]), 3)
            temp = float(test[1])
            nums.append(temp)

        else:
            print("Found: " + test[1])
print(nums)
plt.plot(nums)
plt.show()