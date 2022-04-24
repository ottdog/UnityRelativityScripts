/*
 * GravityWell.cs
 * Original code by Otto Daniels
 * 
 * Attach this script to objects you want to act as gravity wells.
 * 
 * mass = the mass of the gravity well.
 * 
 * Gives objects the "well" tag automaticially on startup.
 * 
 * Load this script and PhysicsManager.cs before you load any other scripts
 * in this repository. 
 * 
 * Script execution order can be found under Edit > Project Settings
 * 
 * This gravity well script is seperate from the main physics script because
 * I originally made this for an RTS game idea I had that would have a whole
 * bunch of ships. While those ships realistically whill exert a pull on each
 * other and celestial bodies, the pull would be negligable. This scirpt is
 * intended to be added to non-negligable sources of gravity to aid performance
 * so the computer doesn't need to calcualte and sum the force vectors from every
 * single object in the scene. 
 * 
 * Don't add the RealitivityPhysics.cs script to the same object as this if you
 * want to keep your gravity wells stationary/give them a pre-programmed path
 * to save frame rates.
 * 
 * As of 2022-04-23 this script is incompatible with RealitivityPhysics.cs when
 * applied to the same object. It is intended that both scripts can be added to
 * the same object if you want gravity wells that are affected by other gravity
 * wells.
 * 
 * Multiple gravity wells in one scene does work, however. They just don't
 * interact with each other.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour
{
    public float mass;

    private void Start()
    {
        this.tag = "well";
    }

}
