using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataUpdatePusher : MonoBehaviour
{
    [SerializeField] private GameObject[] bikeToPushTo;

    private void Start()
    {
        for (int i = 0; i < bikeToPushTo.Length; i++)
        {
            if (bikeToPushTo[i] == null)
            {
                Debug.LogError("Reference Error in DataUpdatePusher");
                return;
            }
        }
    }

    public void BikeUpdate()
    {
        foreach (GameObject bikeObject in bikeToPushTo)
        {
            bikeObject.GetComponent<BikeUpdatePusher>().DataUpdatePuller();
        }
    }
}
