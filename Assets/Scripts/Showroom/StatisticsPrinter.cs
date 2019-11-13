using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsPrinter : MonoBehaviour
{
    //public GameObject[] bars;
    public Transform speed;
    public Transform weight;
    public Transform grip;
    private float originalSize;
    private GameObject gameManager;
    private float speedBarSize;
    private float weightBarSize;
    private float gripBarSize;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager");

        //for (int i = 0; i < bars.Length; i++)
        //{
        //    if (!(bars[i].name.IndexOf("Speed") == -1))
        //    {
        //        speed = bars[i].GetComponent<Transform>();
        //    } else if (!(bars[i].name.IndexOf("Weight") == -1))
        //    {
        //        weight = bars[i].GetComponent<Transform>();
        //    } else if (!(bars[i].name.IndexOf("Grip") == -1))
        //    {
        //        grip = bars[i].GetComponent<Transform>();
        //    }
        //}

        originalSize = speed.localScale.x;
    }

    //void Update()
    //{
    //    speedBarSize = ((gameManager.GetComponent<DataArray>().speedModifier + 120) * 0.291666f + 30f) * 0.01f;
    //    weightBarSize = ((gameManager.GetComponent<DataArray>().weightModifier + 20) * 4.666667f + 30f) * 0.01f;
    //    gripBarSize = (gameManager.GetComponent<DataArray>().gripModifier * 5.833333f + 30f) * 0.01f;

    //    speed.localScale = new Vector3(originalSize * speedBarSize, speed.localScale.y);
    //    weight.localScale = new Vector2(originalSize * weightBarSize, speed.localScale.y);
    //    grip.localScale = new Vector2(originalSize * gripBarSize, speed.localScale.y);
    //}
}
