using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativityPhysics : MonoBehaviour
{ 

    public int mass; //Mass of item at rest

    //holds physics manager
    GameObject manager;

    //GO[] to hold well objects in scene. Tag with "Well" to get them included
    GameObject[] gravityWells;

    //holds the object's rigidbody
    Rigidbody rb;


    //testing vector
    public Vector3 initalV = new Vector3();

    float G; //gravitaional constant
    float c; //speed of causality(aka speed of light)
    float gamma; //relativistic inertia of the object


    //A var to hold the velocity's magnitude so the computer only has to calculate it once a update
    float vMag;

    void Start()
    {
        //retrieves the physics manager in the scene
        manager = GameObject.FindGameObjectWithTag("manager");

        //retrives all gravity well game objects and puts them in an array
        gravityWells = GameObject.FindGameObjectsWithTag("Well");

        //retrieves constants from the manager
        G = manager.GetComponent<PhysicsManager>().G;
        c = manager.GetComponent<PhysicsManager>().c;

        //gets this object's rigidbody
        rb = this.GetComponent<Rigidbody>();

        //adds testing force
        rb.AddForce(initalV);

        //sets the RB's mass to the onject's mass
        rb.mass = mass;

    }

    //finds and sets the gamma of the object
    float getGamma()
    {  
        float v = rb.velocity.sqrMagnitude; //Square V

        //if statement to make sure we get an real number as a return (aka avoid square rooting a negative)

        if (v / (c * c) < 1)
        {
            //calculates
            gamma = 1 / Mathf.Sqrt(1 - (v / (c * c)));
            Debug.Log("Gamma is " + gamma);
            // Debug.Log("V is " + v);
        } else
        {
            gamma = 99999999; //For now, this sets gamma to an arbitrarily high value
            //to keep the thing from accelerating. I will replace this with a more
            //elegant solution later
            //(Or one that works. Hopefully those align). 
        }


        return gamma;
        
    }

    //gets the heading of a game object relative to this thingy
    Vector3 getHeading(GameObject a)
    {
        Vector3 direction = new Vector3();
        direction = a.transform.position - this.transform.position;
        return direction;
    }

    //gets the force magnitude based on the shell theorem
    float getShellForce(GameObject a)
    {

        float distance = getHeading(a).sqrMagnitude; //uses square magnitude to improve performance
        //and to save math because it needs to be square anyway


        return (G * a.GetComponent<GravityWell>().mass) / distance;
    }

    //given a vector, this will calculate a unit vector in the same direction.
    Vector3 getUnitVector(Vector3 v)
    {
        float magnitude = vMag;

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
        float gamma = getGamma();
        rb.mass = mass * gamma;


    }



    // Update is called once per frame
    void FixedUpdate()
    {
        vMag = rb.velocity.magnitude; //sets the vMag

        //Vector 3 that represents the net force;
        Vector3 netForce = new Vector3(0, 0, 0);

        //Vector3 @ float to represent shit and shit idk, used each time by loop for each object
        Vector3 tempVector = new Vector3();
        float tempForce;

        //adds the forces from all wells together into 
        foreach (GameObject well in gravityWells)
        {

            tempForce = getShellForce(well);
            tempVector = getHeading(well);

            netForce += getForceVector(tempForce, tempVector);


        }

        //adds the force

        setRelaticisticMass();
        rb.AddForce(netForce);

        //addRelativityForce(netForce);


    }
}
