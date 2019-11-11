using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BikeController : MonoBehaviour
{
    public WheelJoint2D backWheel;
    JointMotor2D motorBack;
    [Header("Rigidbodies")]
    public Rigidbody2D backRB;
    public Rigidbody2D frontRB;
    public Rigidbody2D crankRB;
    [Header("Movement")]
    [SerializeField] private float speed = 0;
    [SerializeField] private float torque = 0;
    [SerializeField] private float rotationSpeed = 0;

    public WheelJoint2D crank;
    JointMotor2D motorCrank;
    public float speedCrank;
    public float torqueCrank;

    public float windSpeed;
    public float windFrequency;

    private Rigidbody2D rb;
    private float weightModifier;
    private float speedModifier;
    private GameObject gameManager;
    private float stormActual;
    private float rainActual;
    private float windActual;
    private float dryActual;

    public bool isInRain = false;
    public bool isInStorm = false;
    public bool isInWind = false;

    private int windGustTimer;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        rb = gameObject.GetComponent<Rigidbody2D>();

        speedModifier = gameManager.GetComponent<DataArray>().speedModifier;
        weightModifier = gameManager.GetComponent<DataArray>().weightModifier;
        rb.mass += weightModifier;

        stormActual = (speed + speedModifier) * gameManager.GetComponent<DataArray>().stormModifier;
        rainActual = (speed + speedModifier) * gameManager.GetComponent<DataArray>().rainModifier;
        windActual = (speed + speedModifier) * gameManager.GetComponent<DataArray>().windModifier;
        dryActual = (speed + speedModifier) * gameManager.GetComponent<DataArray>().dryModifier;
    }

    void Update()
    {
        float speedActual;
        FreezeRotation(false);

        if (isInStorm)
        {
            speedActual = stormActual;
            WindGust();
        } else if (isInRain)
        {
            speedActual = rainActual;
        } else if (isInWind)
        {
            speedActual = windActual;
            WindGust();
        } else
        {
            speedActual = dryActual;
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (rb.velocity.x < 0.5)
            {
                Motor(speedActual, torque, speedCrank, torqueCrank, -rotationSpeed);
            }
            else
            {
                FreezeRotation(true);

                Motor(0f, 0f, 0f, 0f, -rotationSpeed);
            }
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (rb.velocity.x > -0.5)
            {
                Motor(-speedActual, torque, -speedCrank, torqueCrank, rotationSpeed);
            }
            else
            {
                FreezeRotation(true);

                Motor(0f, 0f, 0f, 0f, rotationSpeed);
            }
        }
        else
        {
            Motor(0f, 0f, 0f, 0f, 0f);
        }
    }

    public void FreezeRotation(bool Freeze)
    {
        frontRB.freezeRotation = Freeze;
        backRB.freezeRotation = Freeze;
    }

    public void Motor(float wheelSpeed, float motorTorque, float crankSpeed, float crankTorque, float rotation)
    {
        motorBack.motorSpeed = wheelSpeed;
        motorBack.maxMotorTorque = motorTorque;
        backWheel.motor = motorBack;

        motorCrank.motorSpeed = crankSpeed;
        motorCrank.maxMotorTorque = crankTorque;
        crank.motor = motorCrank;

        rb.AddTorque(rotation);
    }

    public void WindGust()
    {
        if (Time.time >= windGustTimer)
        {
            rb.AddForce(new Vector2(-windSpeed, 0.0f));
            windGustTimer = Mathf.RoundToInt(Time.time + windFrequency);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bridge")
        {
            gameManager.GetComponent<GameManager>().Respawn();
        }
        if (collision.tag == "Wind")
        {
            windGustTimer = 0;
            isInWind = true;
        }
        if (collision.tag == "Rain")
        {
            isInRain = true;
        }
        if (collision.tag == "Storm")
        {
            windGustTimer = 0;
            isInStorm = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wind")
        {
            isInWind = false;
        }
        if (collision.tag == "Rain")
        {
            isInRain = false;
        }
        if (collision.tag == "Storm")
        {
            isInStorm = false;
        }
    }
}
