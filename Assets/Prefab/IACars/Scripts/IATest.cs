using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATest : MonoBehaviour {

    public GameObject target; //the car follows this object 
    public bool drive; //allows the script to run
    public float speed; //I don't know why I used this
    public float accelerator; //factor to make the car 

    public bool slowDown; //just slow the car when an obstacle is in front
    public bool brake; // stop the car to avoid crash
    
    public float distance; //distance between an obstacle
       
	void Start ()
    {
		
	}

    void Update()
    {
        Motor();
    }

    void Motor()
    {
        if(accelerator < 1 && brake == false && slowDown == false & drive == true)
        {
            accelerator += 0.2f * Time.deltaTime;
        }

        if(accelerator > 0.5f && slowDown == true)
        {
            accelerator -= 0.3f * Time.deltaTime;
        }

        if(accelerator > 0.0005f && brake == true)
        {
            accelerator -= 0.4f * Time.deltaTime;
        }

        if (drive == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * accelerator * Time.deltaTime);

            transform.rotation = target.transform.rotation;
        }
    }

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Obstacle")
        {
            Debug.Log(Vector3.Distance(transform.position, c.transform.position));

            distance = Vector3.Distance(transform.position, c.transform.position);

            if(distance < 60)
            {
                brake = true;
            }
            else
            {
                brake = false;
            }
        }

        if(c.gameObject.tag == "TrafficLightRed")
        {
            brake = true;
        }


        if (c.gameObject.tag == "TrafficLightGreen")
        {
            brake = false;
        }

        else
        {
            brake = false;
        }


    }
}
