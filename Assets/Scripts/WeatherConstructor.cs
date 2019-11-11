using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherConstructor : MonoBehaviour
{
    public Constructor[] rain;
    public Constructor[] storm;
    public Constructor[] wind;

    public GameObject rainObject;
    public GameObject stormObject;
    public GameObject windObject;

    void Start()
    {
        for (int i = 0; i < rain.Length; i++)
        {
            WeatherObjectCreator(rainObject, rain[i].leftBorder, rain[i].rightBorder);
        }
        for (int i = 0; i < storm.Length; i++)
        {
            WeatherObjectCreator(stormObject, storm[i].leftBorder, storm[i].rightBorder);
        }
        for (int i = 0; i < wind.Length; i++)
        {
            WeatherObjectCreator(windObject, wind[i].leftBorder, wind[i].rightBorder);
        }
    }

    private void WeatherObjectCreator(GameObject prefab, float left, float right)
    {
        GameObject createdObject;
        createdObject = Instantiate(prefab, new Vector3((left + right) * 0.5f, 0f, 0f), new Quaternion());
        createdObject.GetComponent<BoxCollider2D>().size = new Vector2(right - left, 40f);
    }
}

[System.Serializable]
public class Constructor
{
    public float leftBorder;
    public float rightBorder;
}
