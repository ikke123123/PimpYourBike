using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBikeChanger : MonoBehaviour
{
    public GameObject bike;

    void Update()
    {
        //if (bike.GetComponent<Transform>().position.x > 15)
        //{
        //    gameObject.GetComponent<DataArray>().frame.selected = Mathf.RoundToInt(Random.Range(-0.49f, 3.49f));
        //    gameObject.GetComponent<DataArray>().wheel.selected = Mathf.RoundToInt(Random.Range(-0.49f, 3.49f));
        //    gameObject.GetComponent<DataArray>().pedal.selected = Mathf.RoundToInt(Random.Range(-0.49f, 3.49f));
        //    gameObject.GetComponent<DataArray>().handle.selected = Mathf.RoundToInt(Random.Range(-0.49f, 3.49f));
        //    bike.GetComponent<Transform>().position = new Vector3(-15f, 0f, 0f);
        //} else if (bike.GetComponent<Transform>().position.x < -15)
        //{
        //    gameObject.GetComponent<DataArray>().frame.selected = Mathf.RoundToInt(Random.Range(-0.49f, 3.49f));
        //    gameObject.GetComponent<DataArray>().wheel.selected = Mathf.RoundToInt(Random.Range(-0.49f, 3.49f));
        //    gameObject.GetComponent<DataArray>().pedal.selected = Mathf.RoundToInt(Random.Range(-0.49f, 3.49f));
        //    gameObject.GetComponent<DataArray>().handle.selected = Mathf.RoundToInt(Random.Range(-0.49f, 3.49f));
        //    bike.GetComponent<Transform>().position = new Vector3(15f, 0f, 0f);
        //}
    }
}
