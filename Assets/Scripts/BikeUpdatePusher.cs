using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeUpdatePusher : MonoBehaviour
{
    //Forwards that there has been an update in the Data Array
    public void DataUpdatePuller()
    {
        GetComponent<VariableController>().UpdateValues();
    }

    public void UpdateSpeed(float speed)
    {
        GetComponent<BikeController>().UpdateSpeed(speed);
    }

    public void UpdateRotation(float rotationSpeed)
    {
        GetComponent<BikeController>().UpdateRotationSpeed(rotationSpeed);
    }
}
