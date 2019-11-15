using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightController : MonoBehaviour
{
    [HideInInspector] private Rigidbody2D rigidBody;

    public void UpdateWeight(float weight)
    {
        rigidBody.mass = weight;
    }
}
