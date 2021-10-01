using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeSound : MonoBehaviour
{
    public Sounds[] sounds;

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            //Setting Defaults and checking if everything is viable
            sounds[i].volume = Clamp(0f, 1f, sounds[i].volume);
            if (sounds[i].volume == 0)
            {
                sounds[i].volume = sounds[i].source.volume;
            }
            if (sounds[i].rampOffStep == 0)
            {
                sounds[i].rampOffStep = 0.01f;
            }
        }

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].dryPlay)
            {
                PlaySound(sounds[i].source, sounds[i].volume);
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].rampOff)
            {
                sounds[i].rampOff = Ramp(sounds[i].source, sounds[i].rampOffStep);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        CheckOnTrigger(collision);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].dryPlay == false)
            {
                sounds[i].rampOff = true;
            }
            else
            {
                PlaySound(sounds[i].source, sounds[i].volume);
            }
        }
    }

    private void CheckOnTrigger(Collider2D collision)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (!(sounds[i].rainPlay || sounds[i].stormPlay || sounds[i].windPlay))
            {
                sounds[i].rampOff = true;
            }
        }

        if (collision.tag == "Rain")
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                PlayConditions(i, sounds[i].rainPlay);
            }
        }
        else if (collision.tag == "Storm")
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                PlayConditions(i, sounds[i].stormPlay);
            }
        }
        else if (collision.tag == "Wind")
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                PlayConditions(i, sounds[i].windPlay);
            }
        }
    }

    private void PlayConditions(int i, bool input)
    {
        if (input)
        {
            PlaySound(sounds[i].source, sounds[i].volume);
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

    private void TriggerCheck(Collider2D collision, Sounds[] sounds)
    {
        if (collision.tag == "Rain")
        {

        }
        else if (collision.tag == "Storm")
        {

        }
        else if (collision.tag == "Wind")
        {

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

[System.Serializable]
public class Sounds
{
    public string name;
    public AudioSource source;
    public float volume;
    public bool dryPlay;
    public bool rainPlay;
    public bool stormPlay;
    public bool windPlay;
    public float rampOffStep;
    [HideInInspector]
    public bool rampOff;
    [HideInInspector]
    public bool disabled;
}