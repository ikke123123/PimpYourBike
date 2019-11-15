using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeUpdatePusher : MonoBehaviour
{
    //Forwards that there has been an update in the Data Array
    public void DataUpdatePuller()
    {
        GetComponent<VariableController>().UpdateValues();
        GetComponent<SpriteSelector>().UpdateSprites();
    }
    public void UpdateSpeed()
    {
        GetComponent<BikeController>().UpdateSpeed();
    }

    public void UpdateGrip()
    {
        
    }

    public void UpdateWeight()
    {
        
    }
}
