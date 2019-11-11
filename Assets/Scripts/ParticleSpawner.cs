using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject[] rain;
    public float frequency;

    private float timer;

    void Update()
    {
        if (timer - Time.time <= 0)
        {
            GameObject spawnedObject;
            timer = Time.time + frequency;
            spawnedObject = Instantiate(rain[Mathf.RoundToInt(Random.Range(0.49f, rain.Length) - 0.51f)]);
            spawnedObject.transform.position = new Vector3(Random.Range(transform.position.x - gameObject.GetComponent<BoxCollider2D>().size.x * 0.5f, transform.position.x + gameObject.GetComponent<BoxCollider2D>().size.x * 0.5f), 40f, 0f);
        }
    }
}
