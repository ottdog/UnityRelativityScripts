# UnityRelativityScripts

This is the remains of a project I started working on in late 2019. 

Latest version was tested on Unity 2021 LTS. Inital code was written on 2019 LTS
and has been very minimally modified, so should also be compatible with 2019 and 2020.

## A note to RPI OSS graders
See the `InitalCode` branch to see the orginal state of my code before I cleaned it up
for OSS class.

## How to use

(Video tutorial coming soon)

### install scripts
1. Click on the green code button, download the zip file, and extract the files.
2. Drag the files into your Unity instance (preferably in a new folder to seperate
these from your other scripts.
3. and the tags `manager` and `well` to your scene. 
4. Create a new empty GameObject and attach the `PhysicsManager.cs` script to it.
Enter values for G (universial gravitational constant) and c (speed of light/causality).
The script will automatically give the manager the `manager` tag.
5. Attach GravityWell.cs 
