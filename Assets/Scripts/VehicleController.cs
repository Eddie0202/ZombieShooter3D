using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Wheels Collider")]
    public WheelCollider frontRightWheelCollider;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider backRightWheelCollider;
    public WheelCollider backLeftWheelCollider;

    [Header("Wheels Transform")]
    public Transform frontRightWheelTransform;
    public Transform frontLeftWheelTransform;
    public Transform backRightWheelTransform;
    public Transform backLeftWheelTransform;
    public Transform vehicleDoor;

    [Header("Vehicle Engine")]
    public float accelerationForce = 100f;
    public float presentAcceleration = 0f;

    public void Update()
    {
        MoveVehicle();
    }

    void MoveVehicle()
    {
        frontRightWheelCollider.motorTorque = presentAcceleration;
        frontLeftWheelCollider.motorTorque = presentAcceleration;
        backRightWheelCollider.motorTorque -= presentAcceleration;
        backLeftWheelCollider.motorTorque += presentAcceleration;

        presentAcceleration = accelerationForce * - Input.GetAxis("Vertical");
    }
}
