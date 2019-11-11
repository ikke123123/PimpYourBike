using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFrequency : MonoBehaviour
{
    public float soundFrequency;
    private float soundTimer;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        soundTimer = soundFrequency;
    }

    void Update()
    {
        if (soundTimer - Time.time <= 0)
        {
            soundTimer = Time.time + soundFrequency;
            if (source.isPlaying == false)
            {
                source.Play();
            }
        }
    }
}
