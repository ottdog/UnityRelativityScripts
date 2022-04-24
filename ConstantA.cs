using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantA : MonoBehaviour
{
    

    Rigidbody rb;

    public Vector3 force = new Vector3();

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

        lastV = v;
        v = rb.velocity.magnitude;
        deltaV = v - lastV;

        Debug.Log("Velocity: " + v);
        Debug.Log("DeltaV:" + deltaV);
        Debug.Log("Mass: " + rb.mass);

    }
}
