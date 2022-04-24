/*
 * ConstantA.CS
 * Original code by Otto Daniels
 * 
 * This scirpt was intended to apply a constant force to the
 * object it was attached to to demonstrate the difference
 * between the relativistc physics speed cap and the Newtonian
 * physics lack of cap. 
 * 
 * force = force vector to be applied to the object's Rigidbody
 * on each update.
 * 
 * displayDebug = set to true to display debug information such
 * as the objects velocity, deltaV, and the current mass of its
 * Rigid body.
 * 
 * This script can be applied to any object with a Rigidbody,
 * regardless of if it has RelativityPhysics or not. If it
 * doesn't the object will accelerate constantly like in
 * Newtonian physics. Otherwise, acceleration will slow as
 * its velocity approaces c like in relativistic physics. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantA : MonoBehaviour
{
    Rigidbody rb;

    public Vector3 force = new Vector3();
    public bool displayDebug;

    float v = 0;
    float lastV;
    float deltaV;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.AddForce(force);

        if (displayDebug)
        {
            lastV = v;
            v = rb.velocity.magnitude;
            deltaV = v - lastV;

            Debug.Log("Velocity: " + v);
            Debug.Log("DeltaV:" + deltaV);
            Debug.Log("Mass: " + rb.mass);

        }
    }
}
