/*
 * PhysicsManager.cs
 * Original code by Otto Daniels.
 * 
 * Attach this script to ONE empty gameObject and set universal
 * constants nessisary for your simulation.
 * 
 * For the realitiviy simulation to work, you must have exactly
 * one object with this script. Will break if zero objects have
 * this script. Unknown behavior if multiple objects have this
 * script.
 * 
 * G = universal gravitational constant.
 * 
 * c = speed of light.
 * 
 * Gives attached object the "manager" tag on startup.
 * 
 * Load this script and PhysicsManager.cs before you load any other scripts
 * in this repository. 
 * 
 * Script execution order can be found under Edit > Project Settings.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public float G;
    public float c;

    private void Start()
    {
        this.tag = "manager";
    }
}
