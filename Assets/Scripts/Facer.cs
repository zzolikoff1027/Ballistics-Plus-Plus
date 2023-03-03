using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facer : Kinematic
{
    public Face myRotateType;

    void Start()
    {
        myRotateType = new Face();
        myRotateType.character = this;
        myRotateType.target = GameObject.Find("Target"); ;
    }

    protected override void Update()
    {
        this.transform.parent = null;
        steeringUpdate = new SteeringOutput();
        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}
