using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataUpdatePusher : MonoBehaviour
{
    [HideInInspector] private GameObject bikeRoot;

    private void Start()
    {
        bikeRoot = GameObject.Find("BikeRoot");
        if (bikeRoot == null)
        {
            Debug.LogError("Oopsie Woopsie, there is a reference problem");
        }
    }

    public void BikeUpdate()
    {
        bikeRoot.GetComponent<BikeUpdatePusher>().DataUpdatePuller();
    }
}
