using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public void City(string city)
	{
		Application.LoadLevel("City1");
	}

	public void Arena(string arena)
	{
		Application.LoadLevel("CarPreparation");
	}
}
