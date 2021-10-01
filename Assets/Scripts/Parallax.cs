using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject cam;
    public GameObject[] background;
    public float[] speedHCoefficient;
    public float[] speedVCoefficient;

    private float lastCamX;
    private float lastCamY;

    void Start()
    {
        lastCamX = cam.GetComponent<Transform>().position.x;
        lastCamY = cam.GetComponent<Transform>().position.y;
    }

    void LateUpdate()
    {
        if (lastCamX != cam.GetComponent<Transform>().position.x)
        {
            //Horizontal
            for (int i = 0; i < background.Length; i++)
            {
                background[i].GetComponent<Transform>().position += new Vector3((cam.GetComponent<Transform>().position.x - lastCamX) * speedHCoefficient[i], 0f, 0f);
            }
        }
        if (lastCamY != cam.GetComponent<Transform>().position.y)
        {
            //Vertical
            for (int i = 0; i < background.Length; i++)
            {
                background[i].GetComponent<Transform>().position += new Vector3(0f, (cam.GetComponent<Transform>().position.y - lastCamY) * speedVCoefficient[i], 0f);
            }
        }

        RightBackground();

        LeftBackground();

        lastCamX = cam.GetComponent<Transform>().position.x;
        lastCamY = cam.GetComponent<Transform>().position.y;
    }

    private void LeftBackground()
    {
        for (int i = 0; i < background.Length; i++)
        {
            if (cam.GetComponent<Transform>().position.x <= background[i].GetComponent<Transform>().position.x - background[i].GetComponent<SpriteRenderer>().bounds.size.x * 0.5f)
            {
                background[i].GetComponent<Transform>().position = new Vector3(background[i].GetComponent<Transform>().position.x - background[i].GetComponent<SpriteRenderer>().bounds.size.x, background[i].GetComponent<Transform>().position.y, background[i].GetComponent<Transform>().position.z);
            }
        }
    }

    private void RightBackground()
    {
        for (int i = 0; i < background.Length; i++)
        {
            if (cam.GetComponent<Transform>().position.x >= background[i].GetComponent<Transform>().position.x + background[i].GetComponent<SpriteRenderer>().bounds.size.x * 0.5f)
            {
                background[i].GetComponent<Transform>().position = new Vector3(background[i].GetComponent<Transform>().position.x + background[i].GetComponent<SpriteRenderer>().bounds.size.x, background[i].GetComponent<Transform>().position.y, background[i].GetComponent<Transform>().position.z);
            }
        }
    }

}
