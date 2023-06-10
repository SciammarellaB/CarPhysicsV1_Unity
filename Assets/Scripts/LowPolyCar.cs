using UnityEngine;
using System.Collections;

public class LowPolyCar : MonoBehaviour {

	public WheelCollider[] wheelColliders;
	public Transform[] tireMeshes;

	public Transform centerOfMass;

	Rigidbody rb;

	public float steer;

	public float accelerate;


	void Start () 
	{
		rb = gameObject.GetComponent<Rigidbody>();

		rb.centerOfMass = new Vector3(0,-0.5f,0);


	}
	

	void Update ()
	{
		UpdateMeshesPositions();

		steer = Input.GetAxis("Horizontal");

		accelerate = Input.GetAxis("Vertical");

		Debug.Log(rb.velocity.magnitude * 3.4f);


	}

	void FixedUpdate()
	{
		wheelColliders[0].steerAngle = steer * 30;
		wheelColliders[1].steerAngle = steer * 30;

		wheelColliders[0].motorTorque = accelerate * 1000;
		wheelColliders[1].motorTorque = accelerate * 1000;
		wheelColliders[2].motorTorque = accelerate * 1000;
		wheelColliders[3].motorTorque = accelerate * 1000;
	}

	void UpdateMeshesPositions()
	{
		for(int i = 0; i < 4; i ++)
		{
			Quaternion quat;
			Vector3 pos;
			wheelColliders[i].GetWorldPose(out pos, out quat);

			tireMeshes[i].position = pos;
			tireMeshes[i].rotation = quat;
		}
	}
}
