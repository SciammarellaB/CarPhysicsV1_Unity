using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public GameObject pressEnter;
	public float timePressEnter;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(pressEnter.activeSelf == true)
		{
			timePressEnter += Time.deltaTime;
		}

		if(timePressEnter >= 3)
		{
			pressEnter.SetActive(false);
			timePressEnter = 0;
		}
	}

	public void Restart(string restart)
	{
		Application.LoadLevel("CarPreparation");
	}

	public void Menu(string menu)
	{
		Application.LoadLevel("Menu");
	}
}
