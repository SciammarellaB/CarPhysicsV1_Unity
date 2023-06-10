using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLights : MonoBehaviour
{

    public int x;

    public bool vertical;   //Semaforo 2 e 4
    public bool horizontal; //Semaforo 1 e 3
       

    public Light[] Sem1 = new Light[3]; //Semaforo 1
    public Light[] Sem2 = new Light[3]; //Semaforo 2
    public Light[] Sem3 = new Light[3]; //Semaforo 3
    public Light[] Sem4 = new Light[3]; //Semaforo 4

    public GameObject[] iaDetectors = new GameObject[4];


    void Start()
    {
        vertical = true;
        horizontal = false;
    }

    void Update()
    {
        x ++;
        Blink();
    }

    void Blink()
    {
        switch (x)
        {
            case 1000:
                Sem1[0].enabled = !Sem1[0].enabled;
                Sem1[1].enabled = !Sem1[1].enabled;
                Sem1[2].enabled = !Sem1[2].enabled;

                Sem2[0].enabled = !Sem2[0].enabled;
                Sem2[1].enabled = !Sem2[1].enabled;
                Sem2[2].enabled = !Sem2[2].enabled;

                Sem3[0].enabled = !Sem3[0].enabled;
                Sem3[1].enabled = !Sem3[1].enabled;
                Sem3[2].enabled = !Sem3[2].enabled;

                Sem4[0].enabled = !Sem4[0].enabled;
                Sem4[1].enabled = !Sem4[1].enabled;
                Sem4[2].enabled = !Sem4[2].enabled;

                vertical = !vertical;
                horizontal = !horizontal;

                x = 0;
                break;

            case 800:
                Sem1[1].enabled = !Sem1[1].enabled;

                Sem2[1].enabled = !Sem2[1].enabled;

                Sem3[1].enabled = !Sem3[1].enabled;

                Sem4[1].enabled = !Sem4[1].enabled;
                break;
        }

        if(vertical == true)
        {
            iaDetectors[0].tag = "TrafficLightRed";
            iaDetectors[1].tag = "TrafficLightGreen";
            iaDetectors[2].tag = "TrafficLightRed";
            iaDetectors[3].tag = "TrafficLightGreen";
        }

        if(horizontal == true)
        {
            iaDetectors[0].tag = "TrafficLightGreen";
            iaDetectors[1].tag = "TrafficLightRed";
            iaDetectors[2].tag = "TrafficLightGreen";
            iaDetectors[3].tag = "TrafficLightRed";
        }
    }
}
