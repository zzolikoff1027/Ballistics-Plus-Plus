using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calculateFiringSolution : MonoBehaviour
{
    public GameObject target;
    public float muzzleVelocity = 10f;
    Rigidbody rb;

    void Start()
    {
        target = GameObject.Find("Target");
        rb = GetComponent<Rigidbody>();
        Nullable<Vector3> aimVector = Calculate(transform.position, target.transform.position, muzzleVelocity, Physics.gravity);
        if (aimVector.HasValue)
        {
            rb.AddForce(aimVector.Value.normalized * muzzleVelocity, ForceMode.VelocityChange);
        }
    }

    public void Update()
    {
        
    }

    public Nullable<Vector3> Calculate(Vector3 start, Vector3 end, float muzzleV, Vector3 gravity)
    {
        Nullable<float> ttt = GetTimeToTarget(start, end, muzzleV, gravity);
        if (!ttt.HasValue)
        {
            return null;
        }
        //Debug.Log("Time to target: " + ttt);

        // return the firing vector
        Vector3 delta = end - start;
        //Debug.Log("Vector to target: " + delta);

        Vector3 n1 = delta * 2;
        Vector3 n2 = gravity * (ttt.Value * ttt.Value);
        float d = 2 * muzzleV * ttt.Value;
        Vector3 solution = (n1 - n2) / d;

        //Debug.Log("solution = " + n1 + " - " + n2 + " / " + d);
        //Debug.Log("solution = " + solution);

        return solution;
    }

    public Nullable<float> GetTimeToTarget(Vector3 start, Vector3 end, float muzzleV, Vector3 gravity)
    {
        // Calculate the vector from the target back to the start
        Vector3 delta = start - end;
        //Vector3 delta = end - start;

        // conventional quadratic equation
        float a = gravity.magnitude * gravity.magnitude;
        float b = -4 * (Vector3.Dot(gravity, delta) + muzzleV * muzzleV);
        float c = 4 * delta.magnitude * delta.magnitude;

        // check for no real solution
        float b2minus4ac = (b * b) - (4 * a * c);
        if (b2minus4ac < 0)
        {
            return null;
        }

        // find the candidate times
        float time0 = Mathf.Sqrt((-b + Mathf.Sqrt(b2minus4ac)) / (2 * a));
        float time1 = Mathf.Sqrt((-b - Mathf.Sqrt(b2minus4ac)) / (2 * a));

        // find the time to target
        Nullable<float> ttt;
        if (time0 < 0)
        {
            if (time1 < 0)
            {
                return null;
            }
            else
            {
                ttt = time1;
            }
        }
        else if (time1 < 0)
        {
            ttt = time0;
        }
        else
        {
            ttt = Mathf.Min(time0, time1);
        }

        return ttt;
    }
}