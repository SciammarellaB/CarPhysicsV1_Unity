using UnityEngine;
using System.Collections;

public class One : MonoBehaviour {

	public GameObject startPoint;
	public GameObject endPoint;
	public GameObject car;
	public GameObject pressEnter;

	public GameObject main;

	public bool hasStarted;
	public bool completed;

	void Start()
	{
		endPoint.SetActive(false);
		pressEnter.SetActive(false);
		car = GameObject.FindWithTag("car");
		startPoint.SetActive(true);
	}

	void Update () 
	{
		startPoint.transform.Rotate(Vector3.up);
		endPoint.transform.Rotate(Vector3.up);

		if(Vector3.Distance(car.transform.position,startPoint.transform.position) < 8)
		{
			pressEnter.SetActive(true);

			if(Input.GetKey(KeyCode.Return))
			{
				hasStarted = true;
				startPoint.SetActive(false);
			}
		}

		else
		{
			pressEnter.SetActive(false);
		}

		if(hasStarted == true)
		{
			endPoint.SetActive(true);
			if(Vector3.Distance(car.transform.position,endPoint.transform.position) < 8)
			{
				pressEnter.SetActive(true);

				if(Input.GetKey(KeyCode.Return))
				{
					completed = true;
					endPoint.SetActive(false);
					main.SetActive(false);
				}
			}

			else
			{
				pressEnter.SetActive(false);
			}
		}

	}
}
