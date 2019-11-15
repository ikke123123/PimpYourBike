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
    [HideInInspector] private float currentSpeed;
    [HideInInspector] private float currentRotationSpeed;

    [Header("Crank")]
    [SerializeField, Range(10, 200)] private int speedCrank;
    [SerializeField, Range(5, 50)] private int torqueCrank;

    //Various
    [HideInInspector] private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        UpdateSpeed();
        UpdateRotationSpeed();
    }

    private void Update()
    {
        Throttle(Input.GetAxis("Horizontal"));
    }

    public void Throttle(float input)
    {
        if (input != 0)
        {
            input = Mathf.Clamp(input * -1.0f, -1.0f, 1.0f);

            rigidBody.AddTorque(currentRotationSpeed * -input);

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

    public void UpdateSpeed()
    {
        currentSpeed = GetComponent<VariableController>().currentSpeed;
    }

    public void UpdateRotationSpeed()
    {
        currentRotationSpeed = GetComponent<RotationController>().currentRotationSpeed;
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
