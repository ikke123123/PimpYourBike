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

    [HideInInspector] private SpriteRenderer[] parallaxRenderers;

    void Start()
    {
        lastCamPos = cam.GetComponent<Transform>().position;

        parallaxGameObjects = new GameObject[parallaxObjects.Length];
        parallaxRenderers = new SpriteRenderer[parallaxObjects.Length];

        for (int i = 0; i < parallaxGameObjects.Length; i++)
        {
            parallaxGameObjects[i] = Instantiate(parallaxObjects[i].parallaxObject);
            parallaxRenderers[i] = parallaxGameObjects[i].GetComponent<SpriteRenderer>();
        }
    }

    void FixedUpdate()
    {
        Vector3 currentCamPos = cam.GetComponent<Transform>().position;
        Vector3 camDeltaPos = lastCamPos - currentCamPos;

        if (camDeltaPos != new Vector3(0, 0, 0))
        {
            for (int i = 0; i < parallaxGameObjects.Length; i++)
            {
                Transform objectTransform = parallaxGameObjects[i].GetComponent<Transform>();
                Vector3 currentObjectPos = objectTransform.position;

                Vector3 newObjectPos = new Vector3(camDeltaPos.x * parallaxObjects[i].hSpeed + parallaxObjects[i].constantSpeed, camDeltaPos.y * parallaxObjects[i].vSpeed, 0f) + currentObjectPos;

                if (currentCamPos.x >= currentObjectPos.x + parallaxRenderers[i].bounds.size.x * 0.5f)
                {
                    newObjectPos = new Vector3(newObjectPos.x + parallaxRenderers[i].bounds.size.x, newObjectPos.y, newObjectPos.z);
                } else if (currentCamPos.x <= currentObjectPos.x - parallaxRenderers[i].bounds.size.x * 0.5f)
                {
                    newObjectPos = new Vector3(newObjectPos.x - parallaxRenderers[i].bounds.size.x, newObjectPos.y, newObjectPos.z);
                }

                objectTransform.position = newObjectPos;
            }
        }
        lastCamPos = currentCamPos;
    }
}