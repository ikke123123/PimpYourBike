using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotationController : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private float refreshTime;
    [HideInInspector] private int frameCounter = 0;
    [HideInInspector] private float timeCounter = 0.0f;
    [HideInInspector] public float lastFramerate = 60;

    [Header("Statistics")]
    [SerializeField, Range(50, 300)] private float rotationSpeed;
    [HideInInspector] public float currentRotationSpeed;

    private void Start()
    {
        currentRotationSpeed = rotationSpeed;
    }

    private void Update()
    {
        if (timeCounter < refreshTime)
        {
            timeCounter += Time.deltaTime;
            frameCounter++;
        }
        else
        {
            lastFramerate = frameCounter / timeCounter;
            frameCounter = 0;
            timeCounter = 0.0f;

            currentRotationSpeed = (60 / lastFramerate) * rotationSpeed;
            GetComponent<BikeController>().UpdateRotationSpeed();
        }
    }
}
