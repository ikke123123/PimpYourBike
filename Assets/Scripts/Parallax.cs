using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private GameObject cam;

    [Header("Objects")]
    [SerializeField] private ParallaxObject[] parallaxObjects;
    [HideInInspector] private GameObject[] parallaxGameObjects;

    //Vector3 of last cam position
    [HideInInspector] private Vector3 lastCamPos;

    void Start()
    {
        lastCamPos = cam.GetComponent<Transform>().position;

        parallaxGameObjects = new GameObject[parallaxObjects.Length];

        for (int i = 0; i < parallaxGameObjects.Length; i++)
        {
            parallaxGameObjects[i] = Instantiate(parallaxObjects[i].parallaxObject);
            parallaxGameObjects[i].transform.SetParent(gameObject.transform);
        }
    }

    void Update()
    {
        Vector3 currentCamPos = cam.GetComponent<Transform>().position;
        Vector3 camDeltaPos = lastCamPos - currentCamPos;

        if (camDeltaPos != new Vector3(0, 0, 0))
        {
            for (int i = 0; i < parallaxGameObjects.Length; i++)
            {
                Vector3 currentObjectPos = parallaxGameObjects[i].GetComponent<Transform>().position;

                currentObjectPos += new Vector3(camDeltaPos.x * parallaxObjects[i].hSpeed, camDeltaPos.y * parallaxObjects[i].vSpeed, 0f);

                //float currentObjectDeltaX = Mathf.Clamp(-1.0f * (currentCamPos.x / (0.5f * parallaxGameObjects[i].GetComponent<SpriteRenderer>().bounds.size.x)), -1.0f, 1.0f);

                //if (currentObjectDeltaX == 1.0f)
                //{
                //    currentObjectPos = new Vector3(currentObjectPos.x + (parallaxGameObjects[i].GetComponent<SpriteRenderer>().bounds.size.x * currentObjectDeltaX), currentObjectPos.y, currentObjectPos.z);
                //}

                parallaxGameObjects[i].GetComponent<Transform>().position = currentObjectPos;
            }
        }
        lastCamPos = currentCamPos;
    }
}