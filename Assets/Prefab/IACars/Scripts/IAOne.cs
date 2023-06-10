using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAOne : MonoBehaviour {

    public Transform path;
    private List<Transform> nodes;
    public int currentNode = 0;
    private float maxSteerAngle = 50f;

    public WheelCollider[] wheelColliders;
    
    float maxMotorTorque = 1500;
    float maxSpeed = 30;
    float maxSteerSpeed = 0;
    public float currentSpeed;


    public Transform centerOfMass;
    private Rigidbody m_rigidBody;

    public Transform[] tireMeshes;

    void Start ()
    {
        m_rigidBody = GetComponent<Rigidbody>();

        m_rigidBody.centerOfMass = centerOfMass.localPosition;

        Transform[] pathTransform = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransform.Length; i++)
        {
            if (pathTransform[i] != path.transform)
            {
                nodes.Add(pathTransform[i]);
            }
        }
    }
	
    void Update()
    {
        currentSpeed = (m_rigidBody.velocity.magnitude * 1.5f);
        currentSpeed = Mathf.Round(currentSpeed);

    }

	private void FixedUpdate ()
    {
        ApplySteer();
        Drive();
        CheckWaypointDistance();
        UpdateMeshesPositions();

    }

    void UpdateMeshesPositions()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);

            tireMeshes[i].position = pos;
            tireMeshes[i].rotation = quat;
        }
    }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

        wheelColliders[0].steerAngle = newSteer;
        wheelColliders[1].steerAngle = newSteer;
    }

    private void CheckWaypointDistance()
    {
        //Debug.Log(Vector3.Distance(transform.position, nodes[currentNode].position));
        Debug.Log(Mathf.Abs(wheelColliders[0].steerAngle));

        if(Vector3.Distance(transform.position, nodes[currentNode].position) < 20)
        {
            if(currentNode == nodes.Count -1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }

    }

    private void Drive()
    {
        if (currentSpeed < maxSpeed && Mathf.Abs(wheelColliders[0].steerAngle) < 20)
        {
            wheelColliders[0].motorTorque = maxMotorTorque;
            wheelColliders[1].motorTorque = maxMotorTorque;
            wheelColliders[2].motorTorque = maxMotorTorque;
            wheelColliders[3].motorTorque = maxMotorTorque;

            wheelColliders[0].brakeTorque = 0;
            wheelColliders[1].brakeTorque = 0;
            wheelColliders[2].brakeTorque = 0;
            wheelColliders[3].brakeTorque = 0;
        }

        else
        {
            wheelColliders[0].motorTorque = 0;
            wheelColliders[1].motorTorque = 0;
            wheelColliders[2].motorTorque = 0;
            wheelColliders[3].motorTorque = 0;

            wheelColliders[0].brakeTorque = 1000;
            wheelColliders[1].brakeTorque = 1000;
            wheelColliders[2].brakeTorque = 1000;
            wheelColliders[3].brakeTorque = 1000;
        }
    }


}
