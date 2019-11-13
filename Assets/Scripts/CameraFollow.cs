using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public GameObject objectToFollow;

    [SerializeField] public float offsetHorizontal;
    [SerializeField] public float offsetVertical;

    void Update()
    {
        transform.position = new Vector3(objectToFollow.transform.position.x + offsetHorizontal, Mathf.Clamp(objectToFollow.transform.position.y + offsetVertical, -2.6f, 20f), transform.position.z);
    }
}