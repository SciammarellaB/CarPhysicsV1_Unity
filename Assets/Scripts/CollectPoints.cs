using UnityEngine;
using System.Collections;

public class CollectPoints : MonoBehaviour {

	public GameObject[] points;
	public GameObject car;
	public GameObject pressEnter;
	bool endOne;
	bool endTwo;


	void Start () 
	{
		car = GameObject.FindWithTag("car");
		pressEnter.SetActive(false);
	}
	

	void Update ()
	{
		if(endOne == false)
		{
			One();
		}
	}

	void One()
	{
		points[0].gameObject.transform.Rotate(Vector3.up);
		points[1].gameObject.transform.Rotate(Vector3.up);

		if((Vector3.Distance(car.transform.position,points[0].transform.position) < 8) && points[0].activeSelf == true)
		{
			pressEnter.SetActive(true);
			if(Input.GetKey(KeyCode.Return))
			{
				points[2].SetActive(false);
				points[0].gameObject.transform.position = new Vector3(0,0,0);
				points[0].gameObject.SetActive(false);
				points[1].gameObject.SetActive(true);
				pressEnter.SetActive(false);
			}
		}

		else
		{
			pressEnter.SetActive(false);
		}

		if(Vector3.Distance(car.transform.position,points[1].transform.position) < 8)
		{
			pressEnter.SetActive(true);

			if(Input.GetKey(KeyCode.Return))
			{
				points[1].gameObject.transform.position = new Vector3(0,0,0);
				points[1].gameObject.SetActive(false);
				pressEnter.SetActive(false);
				endOne = true;
				points[2].SetActive(true);
			}
		}
	}
}
