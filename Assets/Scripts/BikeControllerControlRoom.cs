using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BikeControllerControlRoom : MonoBehaviour
{
    public WheelJoint2D backWheel;
    JointMotor2D motorBack;
    public Rigidbody2D backRB;
    public Rigidbody2D frontRB;
    public Rigidbody2D crankRB;
    public float speed;
    public float torque;

    public WheelJoint2D crank;
    JointMotor2D motorCrank;
    public float speedCrank;
    public float torqueCrank;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FreezeRotation(false);

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (rb.velocity.x < 0.5)
            {
                Motor(speed, torque, speedCrank, torqueCrank, 0f);
            }
            else
            {
                FreezeRotation(true);

                Motor(0f, 0f, 0f, 0f, 0f);
            }
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (rb.velocity.x > -0.5)
            {
                Motor(-speed, torque, -speedCrank, torqueCrank, 0f);
            }
            else
            {
                FreezeRotation(true);

                Motor(0f, 0f, 0f, 0f, 0f);
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

    private void Motor(float wheelSpeed, float motorTorque, float crankSpeed, float crankTorque, float rotation)
    {
        motorBack.motorSpeed = wheelSpeed;
        motorBack.maxMotorTorque = motorTorque;
        backWheel.motor = motorBack;

        motorCrank.motorSpeed = crankSpeed;
        motorCrank.maxMotorTorque = crankTorque;
        crank.motor = motorCrank;

        rb.AddTorque(rotation);
    }
}
