using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unflip : MonoBehaviour
{
    [SerializeField] private GameObject bikeObject;
    [SerializeField] private float unflipTime;
    [HideInInspector] private float timer;


    void Update()
    {
        if (unflipTime <= timer)
        {
            timer = 0.0f;
            Transform bikeTransform = bikeObject.GetComponent<Transform>();
            Rigidbody2D bikeRB = bikeObject.GetComponent<Rigidbody2D>();
            bikeTransform.position = new Vector3(bikeTransform.position.x, bikeTransform.position.y + 5.0f, bikeTransform.position.z);
            bikeRB.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            bikeRB.rotation = 0.0f;
            bikeRB.angularVelocity = 0.0f;
            return;
        }
        timer += Time.deltaTime;
    }
}
