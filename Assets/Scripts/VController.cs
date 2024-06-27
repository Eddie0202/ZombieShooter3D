using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VController : MonoBehaviour
{
    [Header("Sounds")]
    public AudioSource engineStartClip;
    public AudioSource engineAudioSource;

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
    public float accelerationForce = 300f;
    public float breakForce = 200f;
    private float presentBreakForce = 0f;
    public float presentAcceleration = 0f;

    [Header("Vehicle Steering")]
    public float wheelsTorque = 20f;
    private float presentTurnAngle = 0f;

    [Header("Vehicle Security")]
    public PlayerMovementScript player;
    private float radius = 5f;
    private bool isOpen = false;

    [Header("Disable Things")]
 //   public GameObject AimCam;
 //   public GameObject AimCanvas;
 //   public GameObject ThirdPersonCam;
 //   public GameObject ThirdPersonCanvas;
    public GameObject PlayerCharacter;
    public GameObject VehicleCamera;

    public void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isOpen = true;
                radius = 5000f;

                // Disable component camera 
                //PlayerCharacter.GetComponentInChildren<Camera>().enabled = false;
                PlayerCharacter.gameObject.SetActive(false);

                // Enable gameobject "VehicleCamera"
                VehicleCamera.gameObject.SetActive(true);

                engineStartClip.Play();
                engineAudioSource.Play();
                

            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                player.transform.position = vehicleDoor.transform.position;
                isOpen = false;
                radius = 5f;

                // Enable gameobject "VehicleCamera"
                VehicleCamera.gameObject.SetActive(false);

                // Enable
                //PlayerCharacter.GetComponentInChildren<Camera>().enabled = true;
                PlayerCharacter.gameObject.SetActive(true);

                engineAudioSource.Stop();
                engineStartClip.Stop();
                
            }
        }

        if (isOpen == true) 
        {

            MoveVehicle();
            VehicleSteering();
            ApplyBreaks();
        }
        else if(isOpen == false)
        {

        }


        
    }

    void MoveVehicle()
    {
        frontRightWheelCollider.motorTorque = presentAcceleration;
        frontLeftWheelCollider.motorTorque = presentAcceleration;
        backRightWheelCollider.motorTorque -= presentAcceleration;
        backLeftWheelCollider.motorTorque += presentAcceleration;

        presentAcceleration = accelerationForce * -Input.GetAxis("Vertical");
    }

    void VehicleSteering()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        frontRightWheelCollider.steerAngle = presentTurnAngle;
        frontLeftWheelCollider.steerAngle = presentTurnAngle;

        //animate the wheels
        SteeringWheels(frontRightWheelCollider, frontRightWheelTransform);
        SteeringWheels(frontLeftWheelCollider, frontLeftWheelTransform);
        SteeringWheels(backRightWheelCollider, backRightWheelTransform);
        SteeringWheels(backLeftWheelCollider, backLeftWheelTransform);

    }

    void SteeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;

        WC.GetWorldPose(out position, out rotation);

        WT.position = position;
        WT.rotation = rotation;
    }

    void ApplyBreaks()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            presentBreakForce = breakForce;
        }
        else
        {
            presentBreakForce = 0f;
        }

        frontRightWheelCollider.brakeTorque = presentBreakForce;
        frontLeftWheelCollider.brakeTorque = presentBreakForce;
        backRightWheelCollider.brakeTorque = presentBreakForce;
        backLeftWheelCollider.brakeTorque = presentBreakForce;
    }
}
