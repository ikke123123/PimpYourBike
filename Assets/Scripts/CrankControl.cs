using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankControl : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject parent;
    public GameObject grandParent;
    public float xOffset;
    public float yOffset;

    private WheelJoint2D[] wheelJoints;

    public void Start()
    {
        wheelJoints = grandParent.GetComponents<WheelJoint2D>();
    }

    void Update()
    {
        if (wheelJoints[0].motor.motorSpeed != 0f)
        {
            gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 0, wheelJoints[0].motor.motorSpeed*rotationSpeed));
        }
    }
}
