using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public GameObject left;
    public GameObject middle;
    public GameObject right;

    void Start()
    {
        left.transform.position = new Vector3(transform.position.x - ((gameObject.GetComponent<BoxCollider2D>().size.x * 0.5f) + 2.5f), 0f, 0f);
        middle.transform.localScale = new Vector3(gameObject.GetComponent<BoxCollider2D>().size.x * 0.05f, 2f, 0f);
        right.transform.position = new Vector3(transform.position.x + ((gameObject.GetComponent<BoxCollider2D>().size.x * 0.5f) + 2.5f), 0f, 0f);
    }
}
