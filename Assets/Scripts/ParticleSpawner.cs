using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [Header("Rain Particles")]
    [SerializeField] private GameObject[] rain;

    [Header("Spawn Time")]
    [SerializeField] private float spawnFrequency;
    [SerializeField] private int activationDistance;

    //Sizes
    [HideInInspector] private float leftBorder;
    [HideInInspector] private float rightBorder;

    //Various
    [HideInInspector] private float timer;
    [HideInInspector] private Transform bikeRootTransform;

    private void Start()
    {
        bikeRootTransform = GameObject.Find("BikeRoot").GetComponent<Transform>();
        leftBorder = transform.position.x - gameObject.GetComponent<BoxCollider2D>().size.x * 0.5f;
        rightBorder = transform.position.x + gameObject.GetComponent<BoxCollider2D>().size.x * 0.5f;
    }

    private void FixedUpdate()
    {
        float bikeX = bikeRootTransform.position.x;

        if (bikeX >= leftBorder - activationDistance && bikeX <= rightBorder + activationDistance)
        {
            if (timer <= Time.time)
            {
                timer = Time.time + spawnFrequency;

                GameObject spawnedObject;
                spawnedObject = Instantiate(rain[Mathf.RoundToInt(Random.Range(0, rain.Length))]);
                spawnedObject.transform.position = new Vector3(Random.Range(leftBorder, rightBorder), 25f, 0f);
            }
        }
    }
}
