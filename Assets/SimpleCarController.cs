using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 * By GameDevChef
 * Simple Car Controller in Unity Tutorial
 * https://www.youtube.com/watch?v=Z4HA8zJhGEk
 * Viewed:22/03/2022 
 */
public class SimpleCarController : MonoBehaviour {
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public int downForce = 0;

    private void Start()
    {
        Rigidbody rb = gameObject.transform.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0,downForce, 0);
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
            
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }
}
    
[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}