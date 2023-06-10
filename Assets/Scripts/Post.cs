using UnityEngine;
using System.Collections;

public class Post : MonoBehaviour {

	public Light yellow;
	public float counter;

	void Start () 
	{
	
	}
	

	void Update () 
	{
		counter += Time.deltaTime;

		if(counter < 1)
		{
			yellow.gameObject.SetActive(true);
		}

		else
		{
			yellow.gameObject.SetActive(false);
		}

		if(counter > 2)
		{
			counter = 0;
		}
	}
}
