using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityPlay : MonoBehaviour
{
    public float range;

    private GameObject bikeRoot;
    private AudioSource source;
    private float volume;
    private bool rampOff;

    void Start()
    {
        bikeRoot = GameObject.Find("BikeRoot");
        source = GetComponent<AudioSource>();
        volume = source.volume;
    }

    void Update()
    {
        if (bikeRoot.transform.position.x > -range + transform.position.x && bikeRoot.transform.position.x < range + transform.position.x)
        {
            PlaySound(source, volume);
        }
        else if (rampOff == true)
        {
            rampOff = Ramp(source, 0.01f);
        }
    }

    private void PlaySound(AudioSource audio, float volume)
    {
        if (audio.isPlaying == false)
        {
            audio.volume = volume;
            audio.Play();
        }
    }

    private bool Ramp(AudioSource audio, float step)
    {
        if (audio.volume == 0)
        {
            audio.Stop();
            return false;
        }
        else
        {
            audio.volume = Clamp(0f, 1f, audio.volume - step);
            return true;
        }
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
