using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y <= -20)
        {
            Destroy(gameObject);
            Debug.Log("SUICIDE!");
        }
    }
}
