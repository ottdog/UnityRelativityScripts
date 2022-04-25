# UnityRelativityScripts

This is the remains of a project I started working on in late 2019. 

Latest version was tested on Unity 2021 LTS. Inital code was written on 2019 LTS
and has been very minimally modified, so should also be compatible with 2019 and 2020.

## A note to RPI OSS graders
See the `InitalCode` branch to see the orginal state of my code before I cleaned it up
for OSS class.

## Current features:
- Configurable universal constants
- Objects can be given a gravitational pull
- Objects gain inertia as they approach the speed of light/causality.

## Features this doesn't include (yet?):
- Lorentz contraction simulation
- Dopplershift of light
- True Einstien field equation calculation for gravitational force
- Other effects of general relatiity 

## Known issues:
- When a non-gravity well object collides with a gravity well with a RigidBody
 it creates a perpetual motion machine.
- An object can accelerate past C if given a constant acceleration. In this case,
the object is given a rigidbody mass of infinity. 

## How to use

(Video tutorial coming soon)

### Install scripts and set up the scene
1. Click on the green code button, download the zip file, and extract the files.
2. Drag the files into your Unity instance (preferably in a new folder to seperate
these from your other scripts.
3. and the tags `manager` and `well` to your scene. 
4. In `Edit > Project Settings` go to `Script Execution Order`. Configure `PhysicsManager.cs`
and `GravityWell.cs` to load before `RelativityPhysics.cs`.
5. Create a new empty GameObject and attach the `PhysicsManager.cs` script to it.
Enter values for `G` (universial gravitational constant) and `c` (speed of light/causality).
The script will automatically give the manager the `manager` tag.

### Create an object with a gravitational pull

1. Attach `GravityWell.cs` to any GameObject you want to have.
2. Enter the mass you wish the object to have. Do this in the `Gravity Well` component.
Objects with the script attaced will be given the 

### Create an object affected by gravity

1. Attach a RigidBody component to the GameObject you want to be affected by gravity.
2. Set both `Use Gravity` and `Is Kenimatic` to false. Set other properties as you please,
just note `mass` will be overridden.
3. Attach `RealativityPhysics.cs` to your object
4. Set `mass` (Object's effective mass at rest) and `Inital F` (a force vector applied
on startup to optionally give your object an inital 'nudge').

The script works by overriding the RigidBody component's mass, so forces from collisons
will aslo be affected by relativistic inertia.

Objects can both be a gravity well and be affected by gravity.
