using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBikeController : MonoBehaviour
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

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Motor(-speed*0.5f, torque, -speedCrank, torqueCrank, 0f);
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
}

