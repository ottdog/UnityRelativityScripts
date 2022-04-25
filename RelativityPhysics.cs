/*
 * RealitivityPhysics.cs
 * Original code by Otto Daniels.
 * 
 * Attach this script to a GameObject that you want to experience
 * relativistic acceleation/pull from objects with GravityWell.cs.
 * 
 * The object this script is attached to also needs a RigidBody.
 * 
 * mass = the mass of the object at rest. 
 * 
 * initalF = A force vector that's applied to the object on startup
 *          if you'd like to start your scene by giving objects a
 *          little nudge.
 *          
 * displayDebug = set to true to view debug information
 * 
 * In general relativity, the intertia of an object isn'r proportional
 * to its mass like in Newtonian physics. Instead it increases as the
 * object's velocity approaches the speed of light. This is modeled
 * by increasing the obect's RigidBody mass to reflect the 
 *
 * For this to work, this script must load after both GravityWell.cs
 * and PhysicsManager.cs
 * 
 * Script execution order can be found under Edit > Project Settings.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativityPhysics : MonoBehaviour
{ 
    public float mass;
    public Vector3 initalF = new Vector3();
    public bool displayDebug;
    public bool destroyOnImpact;
    public bool destroyOnImpactWithWell;

    GameObject manager;         //holds physics manager
    Rigidbody rb;               //holds the object's rigidbody

    //GO[] to hold well objects in scene. Tag with "Well" to get them included
    GameObject[] gravityWells;

    float G;                //gravitaional constant
    float c;                //speed of light/causality
    float lFactor;          //Lorentz factor of the object

    void Start()
    {
        //retrieves the physics manager in the scene and sets the values of constants
        manager = GameObject.FindGameObjectWithTag("manager");
        G = manager.GetComponent<PhysicsManager>().G;
        c = manager.GetComponent<PhysicsManager>().c;

        //retrives all gravity well game objects and puts them in an array
        gravityWells = GameObject.FindGameObjectsWithTag("well");

        //gets this object's rigidbody and adds inital force
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(initalF);

        //sets the RB's mass to the object's mass
        rb.mass = mass;
    }


    //finds and sets the Lorentz factor (gamma) of the object
    float getLFactor()
    {  
        float v = rb.velocity.sqrMagnitude; //v^2

        //if statement to make sure we get an real number as a return
        if (v / (c * c) < 1)
        {
            //calculates
            lFactor = 1 / Mathf.Sqrt(1 - (v / (c * c)));

            if (displayDebug)
            {
                string debug = this.ToString() +
                        "\nLorentz Factor: " + lFactor;
                Debug.Log(debug);
            }
        } else
        {
            lFactor = Mathf.Infinity; //For now, this sets gamma to an arbitrarily high value
            //to keep the thing from accelerating. I will replace this with a more
            //elegant solution later
            //(Or one that works. Hopefully those align). 
        }

        return lFactor;
    }


    //gets the relative heading of another GameObject
    Vector3 getHeading(GameObject a)
    {
        Vector3 direction = new Vector3();
        direction = a.transform.position - this.transform.position;
        return direction;
    }


    //gets magnitude of force from a gravity well. 
    //currently uses Newtonian gravitational force inestead of EFE
    float getForce(GameObject a)
    {
        //saftey check to see if object is a gravity well
        if(a.tag != "well") { return 0; }

        float distance = getHeading(a).sqrMagnitude; //r ^2
        return (G * a.GetComponent<GravityWell>().mass) / distance;
    }


    //given a vector, this will calculate a unit vector in the same direction.
    Vector3 getUnitVector(Vector3 v)
    {
        float magnitude = v.magnitude;

        float x = v.x / magnitude;
        float y = v.y / magnitude;
        float z = v.z / magnitude;

        return new Vector3(x, y, z);
    }


    //Takes the magnitude of the force and then multiplies it by a unit vector
    Vector3 getForceVector(float force, Vector3 heading)
    {
        Vector3 a = getUnitVector(heading);

        return a * force;
    }


    //Gets a "relativisitc mass" which is the objects inital mass times the
    //added inerta from relativity (gamma)
    void setRelaticisticMass()
    {
        float lFactor = getLFactor();
        rb.mass = mass * lFactor;
    }


    //adds the relativistic gravitational forces to the object
    void applyGravityForce()
    {
        Vector3 netForce = new Vector3(0, 0, 0);

        Vector3 tempVector = new Vector3();
        float tempForce;

        //adds the forces from all wells together into one vector
        foreach (GameObject well in gravityWells)
        {
            //skips if the object attached to this script is
            //a gravity well
            if(well == this.gameObject){
                continue;
            }
            tempForce = getForce(well);
            tempVector = getHeading(well);
            netForce += getForceVector(tempForce, tempVector);
        }

        //adds the force
        setRelaticisticMass();
        rb.AddForce(netForce);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        applyGravityForce();
    }

}
