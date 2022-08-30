# Flute Hero, a Flute Training / Exercise Game

Flute Hero is a Unity/C# based game that uses multiple .Net libraries in tandem. It is to be used with belts Designed by Lucie Jones, one of the contributors to this project. The belts are connected to a Phidget "microcontroller" using voltage sensors to measure the change in expansion along the Abdomen and Upper Ribs to measure breathing and performance. The goal of this project is to design a fun and usable tool that Flute students can use to improve on some more technical aspects of their playing.

This project was created for a research project at the University of Calgary, Summer 2022. It was developed for Lucie Jones, under the supervision of Professor Jeffrey Boyd.

# Software / libraries used:
- C#
- Unity Game Engine
- NAudio C# Library
- Phidget22 Microcontroller Library
- Python (for basic plots only)
- Google API (for logging only)

# ___USAGE___
For the Game:
1. Go to Test Builds.
2. Find the most up to date one.
3. Download the Zip and Run the Executable.
3.1 Note this game is very barebones without access to the above mentioned breathing belts and phidget hardware setup.

For Standalone Pitch Detection:
1. Navigate to PitchDetection/PitchDetectionTest.
2. Compile and Run the C# file Program.cs
3. Output will be seen in the terminal.

Usage Basics:
1. Calibrate both Abdomen and Rib Belts.
2. Select the Required Settings, specifically important for the pitch detection.
3. Try any of the 10 levels.

# Versions:
Currently in release version 2.0. Other versions can be found dating further back in this repository, all found in the Test Builds Folder.

# Sources:
https://www.phidgets.com/?view=articles&article=UsingPhidgetsWithUnity
https://github.com/naudio/NAudio
https://markheath.net/post/30-days-naudio-docs
https://www.youtube.com/channel/UCYbK_tjZ2OrIZFBvU6CCMiA


