using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BikeController : MonoBehaviour
{
    [Header("Wheel Joints")]
    [SerializeField] private WheelJoint2D backWheel;
    [SerializeField] private WheelJoint2D crank;

    [Header("Rigidbodies")]
    [SerializeField] private Rigidbody2D[] wheelRB;

    [Header("Movement")]
    [SerializeField, Range(100, 500)] private int torque;
    [SerializeField, Range(10, 200)] private float rotationSpeed;
    [HideInInspector] private float currentSpeed;

    [Header("Crank")]
    [SerializeField, Range(100, 300)] private int speedCrank;
    [SerializeField, Range(5, 50)] private int torqueCrank;

    [Header("Unflip")]
    [SerializeField, Range(0f, 10f)] private float unflipTime;
    [SerializeField] private GameObject unflipTimer;
    [HideInInspector] private float timer;

    //Various
    [HideInInspector] private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Throttle(-Input.GetAxis("Horizontal"));
    }

    private void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");
        rigidBody.AddTorque(rotationSpeed * input);
    }

    public void Throttle(float input)
    {
        if (Input.GetKey("r"))
        {
            if (unflipTime <= timer)
            {
                timer = 0f;
                transform.position = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z);
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                return;
            }
            FreezeRotation(wheelRB, true);
            timer += Time.deltaTime;
        } else if (input != 0)
        {
            if (ActivateBrake(wheelRB, rigidBody.velocity.x, input, 0.5f) == false)
            {
                ActivateMotor(backWheel, currentSpeed * input, torque);
                ActivateMotor(crank, speedCrank * input, torqueCrank);
            }
            return;
        }
        ActivateMotor(backWheel, 0.0f, 0.0f);
        ActivateMotor(crank, 0.0f, 0.0f);
    }

    public void UpdateSpeed(float speed)
    {
        currentSpeed = speed;
    }

    private bool ActivateBrake(Rigidbody2D[] rigidBodies, float velocity, float axisInput, float speedSwitch)
    {
        if ((axisInput > 0 && velocity < speedSwitch) || (axisInput < 0 && velocity > -speedSwitch))
        {
            FreezeRotation(rigidBodies, false);
            return false;
        }
        FreezeRotation(rigidBodies, true);
        return true;
    }

    private void Unflip()
    {

    }

    private void ActivateMotor(WheelJoint2D motor, float speed, float torque)
    {
        JointMotor2D tempMotor = new JointMotor2D
        {
            motorSpeed = speed,
            maxMotorTorque = torque
        };
        motor.motor = tempMotor;
    }

    private void FreezeRotation(Rigidbody2D[] rigidbodies, bool freeze)
    {
        foreach (Rigidbody2D rb in rigidbodies)
        {
            rb.freezeRotation = freeze;
        }
    }
}
