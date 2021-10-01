using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;

    public float offsetX;
    public float offsetY;

    void Update()
    {
        transform.position = new Vector3(objectToFollow.transform.position.x + offsetX, Clamp(-2.6f, 20f,objectToFollow.transform.position.y + offsetY), transform.position.z);

        //if (transform.position.x < objectToFollow.transform.position.x)
        //{
        //    if (objectToFollow.transform.position.x < transform.position.x + 0.5f)
        //    {
        //        //transform.position = new Vector3(objectToFollow.transform.position.x, transform.position.y, transform.position.z);
        //        //rb.velocity = new Vector2(0, 0);
        //        //modifier = rb.velocity;
        //        modifier = new Vector2(0, 0);
        //    }
        //    else
        //    {
        //        modifier.x = +smoothingSpeed;
        //    }
        //} else if (transform.position.x > objectToFollow.transform.position.x)
        //{
        //    if (objectToFollow.transform.position.x > transform.position.x - 0.5f)
        //    {
        //        //transform.position = new Vector3(objectToFollow.transform.position.x, transform.position.y, transform.position.z);
        //        //rb.velocity = new Vector2(0, 0);
        //        //modifier = rb.velocity;
        //        modifier = new Vector2(0, 0);
        //    }
        //    else
        //    {
        //        modifier.x =- smoothingSpeed;
        //    }
        //}

        ////for applying modifier
        //rb.AddForce(new Vector2(modifier.x, modifier.y));

    }

    private float Clamp(float min, float max, float input)
    {
        float output;
        if (input > max)
        {
            output = max;
        }
        else if (input < min)
        {
            output = min;
        }
        else
        {
            output = input;
        }
        return output;
    }
}