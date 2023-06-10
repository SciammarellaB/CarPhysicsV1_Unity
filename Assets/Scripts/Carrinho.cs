using UnityEngine;
using System.Collections;

public class Carrinho : MonoBehaviour {

	public WheelCollider[] wheelColliders = new WheelCollider[2];
	public Transform[] tireMeshes = new Transform[2];

	void FixedUpdate()
	{
		UpdateMeshesPositions();
	}

	void UpdateMeshesPositions()
	{
		for(int i = 0; i < 2; i ++)
		{
			Quaternion quat;
			Vector3 pos;
			wheelColliders[i].GetWorldPose(out pos, out quat);

			tireMeshes[i].position = pos;
			tireMeshes[i].rotation = quat;
		}
	}
}
