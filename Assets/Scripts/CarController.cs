using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class CarController : MonoBehaviour
{

    public GameObject shiftHint;

    public Text speedText;
    public Text gears;
    public float currentSpeed;

    public float topSpeed;
    public float minimumSpeed;
    public float engineStartPitch;
    public AudioSource aSource;
    public float pitchAdd;


    public int gear;

    public float friction;

    public float currentTorque;
    public float steerForce;

    public Transform centerOfMass;

    float steer;
    public bool accelerate;
    public bool brake;

    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public Transform[] tireMeshes = new Transform[4];

    private Rigidbody m_rigidBody;

    public bool automatic;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.centerOfMass = centerOfMass.localPosition;
        gear = 1;
    }

    void Update()
    {
        steer = Input.GetAxis("Horizontal");
        accelerate = Input.GetKey(KeyCode.UpArrow);
        brake = Input.GetKey(KeyCode.DownArrow);

        UpdateMeshesPositions();

        if (automatic == false)
        {
            Manualgears();
        }

        else
        {
            AutomaticGears();
        }


        currentSpeed = (m_rigidBody.velocity.magnitude * 1.5f);
        currentSpeed = Mathf.Round(currentSpeed);
        speedText.text = "Km/h:" + currentSpeed.ToString();
    }

    void FixedUpdate()
    {
        float finalAngle = steer * 20f;

        wheelColliders[0].steerAngle = finalAngle;
        wheelColliders[1].steerAngle = finalAngle;

        for (int i = 0; i < 4; i++)
        {
            if (currentSpeed < topSpeed)
            {
                if (accelerate == true)
                {
                    wheelColliders[i].motorTorque = currentTorque;
                }
                wheelColliders[i].brakeTorque = 0;
            }

            else
            {
                wheelColliders[i].motorTorque = 0;
                wheelColliders[i].brakeTorque = 1000;
            }

            if (accelerate == false)
            {
                wheelColliders[i].motorTorque = 0;
                wheelColliders[i].brakeTorque = 500;
            }

            if (brake == true)
            {
                wheelColliders[i].motorTorque = 0;
                wheelColliders[i].brakeTorque = 4000;
            }

            wheelColliders[i].attachedRigidbody.AddForce(-transform.up * 100 * wheelColliders[i].attachedRigidbody.velocity.magnitude);
        }
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

    void Manualgears()
    {
        if (gear < 5)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gear++;
            }
        }

        if (gear > 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                gear--;
            }
        }

        if (gear == 0)
        {
            currentTorque = -1500;
            gears.text = "R";
            topSpeed = 40;
            aSource.pitch = ((currentSpeed / topSpeed) / 2) + 0.1f;
        }

        if (gear == 1)
        {
            currentTorque = 0;
            gears.text = "N";
            topSpeed = 160;
            aSource.pitch = 0.1f;

        }

        if (gear == 2)
        {
            currentTorque = 1500;
            gears.text = "1";
            topSpeed = 50;
            aSource.pitch = ((currentSpeed / topSpeed) / 2) + 0.1f;


        }

        if (gear == 3)
        {
            currentTorque = 1300;
            gears.text = "2";
            topSpeed = 85;
            aSource.pitch = ((currentSpeed / topSpeed) / 2) + 0.1f;
        }

        if (gear == 4)
        {
            currentTorque = 1100;
            gears.text = "3";
            topSpeed = 135;
            aSource.pitch = ((currentSpeed / topSpeed) / 2) + 0.1f;
        }

        if (gear == 5)
        {
            currentTorque = 900;
            gears.text = "4";
            topSpeed = 160;
            aSource.pitch = ((currentSpeed / topSpeed) / 2) + 0.1f;

        }

        if (currentSpeed >= topSpeed - 10)
        {
            shiftHint.SetActive(true);
        }

        else
        {
            shiftHint.SetActive(false);
        }

    }

    void AutomaticGears()
    {
        if (currentSpeed > topSpeed - 5)
        {
            if (gear < 5)
            {
                gear++;
            }
        }

        if (gear == 1)
        {
            currentTorque = 0;
            gears.text = "N";
            topSpeed = 160;
            minimumSpeed = 10;
            aSource.pitch = 0.1f;

            if (accelerate == false)
            {
                gear = 1;
            }

            else
            {
                gear = 2;
            }
        }

        if (gear == 2)
        {
            currentTorque = 2000;
            gears.text = "1";
            topSpeed = 20;
            minimumSpeed = 3;
            aSource.pitch = ((currentSpeed / topSpeed) / 2) + 0.1f;
            if (currentSpeed < minimumSpeed)
            {
                gear = 1;
            }

        }

        if (gear == 3)
        {
            currentTorque = 1700;
            gears.text = "2";
            topSpeed = 40;
            minimumSpeed = 15;
            aSource.pitch = ((currentSpeed / topSpeed) / 2) + 0.1f;
            if (currentSpeed < minimumSpeed)
            {
                gear = 2;
            }

        }

        if (gear == 4)
        {
            currentTorque = 1500;
            gears.text = "3";
            topSpeed = 60;
            minimumSpeed = 35;
            aSource.pitch = ((currentSpeed / topSpeed) / 2) + 0.1f;
            if (currentSpeed < minimumSpeed)
            {
                gear = 3;
            }
        }

        if (gear == 5)
        {
            currentTorque = 1300;
            gears.text = "4";
            topSpeed = 90;
            minimumSpeed = 55;
            aSource.pitch = ((currentSpeed / topSpeed) / 2) + 0.1f;
            if (currentSpeed < minimumSpeed)
            {
                gear = 4;
            }
        }

        if (gear == 0)
        {
            currentTorque = -1200;
            gears.text = "R";
            topSpeed = 40;
            aSource.pitch = ((currentSpeed / topSpeed) / 2) + 0.1f;
        }

        if (currentSpeed >= topSpeed - 5)
        {
            shiftHint.SetActive(true);
        }

        else
        {
            shiftHint.SetActive(false);
        }
    }
}
