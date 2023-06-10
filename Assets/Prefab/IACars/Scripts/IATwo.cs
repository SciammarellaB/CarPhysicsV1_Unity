using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATwo : MonoBehaviour {

    public WheelCollider[] carWheelPhysics;
    public Transform[] carWheel;
    public float maxVelocity;
    public float steerAngle;
    public GameObject steerCapture;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        UpdateMeshesPositions();
        CarMotion();
    }

    void UpdateMeshesPositions()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            carWheelPhysics[i].GetWorldPose(out pos, out quat);

            carWheel[i].position = pos;
            carWheel[i].rotation = quat;
        }
    }

    void CarMotion()
    {
        carWheelPhysics[2].motorTorque = 1000;
        carWheelPhysics[3].motorTorque = 1000;

        steerAngle = steerCapture.transform.rotation.y;

        carWheelPhysics[0].steerAngle = steerAngle * 50;
        carWheelPhysics[0].steerAngle = steerAngle * 50;

    }
}
