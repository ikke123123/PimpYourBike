using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainAudio : MonoBehaviour
{
    public WheelJoint2D backWheel;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float wheelSpeed = Mathf.Abs(backWheel.motor.motorSpeed) / 860f;
        audioSource.volume = wheelSpeed;
    }
}
