using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAThree : MonoBehaviour
{

    public WheelCollider[] carWheel;
    public Transform[] tireMesh;

    public GameObject target;
    public GameObject centerOfMass;

    public bool stop;
    public bool startProgram;


    Rigidbody rb;

    public float Steer;
    public float maxSteerAngle;
    public float speed;
    public float maxSpeed;
    public float stateReseter; //used to make the car drive again during a trafficlight bug
    public float waitTime; //used to make the car to look to the right direction before it starts moving


    void Start()
    {
        maxSteerAngle = 45f;

        rb = gameObject.GetComponent<Rigidbody>();

        rb.centerOfMass = new Vector3(0, -0.9f, 0);

    }

    void Update()
    {
        if(startProgram == false)
        {
            waitTime += Time.deltaTime;
        }

        if(waitTime > 5)
        {
            startProgram = true;
            waitTime = 0;
        }
        
        speed = rb.velocity.magnitude * 1.5f;
        speed = Mathf.Round(speed);
    }

    void FixedUpdate()
    {
        ApplySteer();
        UpdateMeshesPositions();
        if (startProgram == true)
        {
            Motor();
        }

    }

    void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(target.transform.position);
        relativeVector = relativeVector / relativeVector.magnitude;
        Steer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

        carWheel[0].steerAngle = Steer;
        carWheel[1].steerAngle = Steer;
    }

    void UpdateMeshesPositions()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            carWheel[i].GetWorldPose(out pos, out quat);

            tireMesh[i].position = pos;
            tireMesh[i].rotation = quat;
        }
    }

    void Motor()
    {
        if (speed < maxSpeed && stop == false)
        {
            carWheel[2].motorTorque = 2500;
            carWheel[3].motorTorque = 2500;

            carWheel[0].brakeTorque = 0;
            carWheel[1].brakeTorque = 0;
            carWheel[2].brakeTorque = 0;
            carWheel[3].brakeTorque = 0;
        }

        else if (stop == true)
        {
            carWheel[2].motorTorque = 0;
            carWheel[3].motorTorque = 0;

            carWheel[0].brakeTorque = 2000;
            carWheel[1].brakeTorque = 2000;
            carWheel[2].brakeTorque = 4000;
            carWheel[3].brakeTorque = 4000;

            stateReseter += Time.deltaTime;

            if (stateReseter > 15)
            {
                stop = false;
                stateReseter = 0;
            }
        }

        else
        {
            carWheel[2].motorTorque = 0;
            carWheel[3].motorTorque = 0;

            carWheel[0].brakeTorque = 500;
            carWheel[1].brakeTorque = 500;
            carWheel[2].brakeTorque = 500;
            carWheel[3].brakeTorque = 500;
        }

        if (Mathf.Abs(Steer) > 5)
        {
            maxSpeed = 10;
        }

        else
        {
            maxSpeed = 30;
        }
    }

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "TrafficLightRed")
        {
            stop = true;

        }

        else
        {
            stop = false;
            stateReseter = 0;
        }

        if (c.gameObject.tag == "Traffic")
        {
            stop = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Traffic")
        {
            stateReseter = 0;
            stop = false;
        }
    }
}
