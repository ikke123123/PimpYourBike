using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeSound : MonoBehaviour
{
    [Header("Sounds Prefabs")]
    [SerializeField] private Sounds[] sounds;

    //Sound Index
    [HideInInspector] private List<SoundObjects> drySounds;
    [HideInInspector] private List<SoundObjects> rainSounds;
    [HideInInspector] private List<SoundObjects> stormSounds;
    [HideInInspector] private List<SoundObjects> windSounds;
    [HideInInspector] private List<SoundObjects> rampOff;

    private void Start()
    {
        int length = sounds.Length;
        GameObject[] soundGameObjects = new GameObject[length];
        SoundObjects[] soundObjects = new SoundObjects[length];
        Debug.Log(length);

        for (int i = 0; i < length; i++)
        {
            soundGameObjects[i] = Instantiate(sounds[i].source);
            soundGameObjects[i].GetComponent<Transform>().SetParent(transform);
            Debug.Log("What?");
        }

        for (int i = 0; i < soundGameObjects.Length; i++)
        {
            Debug.Log("BRUUUUHHHH");
            soundObjects[i].audioSource = soundGameObjects[i].GetComponent<AudioSource>();
            soundObjects[i].sounds = sounds[i];
        }

        AddToList(soundObjects);
    }

    private void FixedUpdate()
    {
        foreach (SoundObjects soundObject in rampOff)
        {
            if (RampOff(soundObject.audioSource, soundObject.sounds.rampOffStep) == false)
            {
                rampOff.Remove(soundObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ForEachRampOff(drySounds);
        switch (collision.tag)
        {
            case "Rain":
                ForEachPlay(rainSounds);
                break;
            case "Storm":
                ForEachPlay(stormSounds);
                break;
            case "Wind":
                ForEachPlay(windSounds);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ForEachPlay(drySounds);
        switch (collision.tag)
        {
            case "Rain":
                ForEachRampOff(rainSounds);
                break;
            case "Storm":
                ForEachRampOff(stormSounds);
                break;
            case "Wind":
                ForEachRampOff(windSounds);
                break;
        }
    }

    private void ForEachPlay(List<SoundObjects> input)
    {
        foreach (SoundObjects source in input)
        {
            rampOff.Remove(source);
            PlaySound(source.audioSource, source.sounds.volume);
        }
    }

    private void ForEachRampOff(List<SoundObjects> input)
    {
        foreach (SoundObjects source in input)
        {
            rampOff.Add(source);
        }
    }

    private void AddToList(SoundObjects[] soundObjects)
    {
        for (int i = 0; i < soundObjects.Length; i++)
        {
            AddSoundObject(soundObjects[i], drySounds, soundObjects[i].sounds.dryPlay);
            AddSoundObject(soundObjects[i], rainSounds, soundObjects[i].sounds.rainPlay);
            AddSoundObject(soundObjects[i], stormSounds, soundObjects[i].sounds.stormPlay);
            AddSoundObject(soundObjects[i], windSounds, soundObjects[i].sounds.windPlay);
        }
    }

    private void AddSoundObject(SoundObjects input, List<SoundObjects> outputList, bool add)
    {
        if (add)
        {
            outputList.Add(input);
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

    private bool RampOff(AudioSource audio, float step)
    {
        if (audio.volume == 0)
        {
            audio.Stop();
            return false;
        }
        audio.volume = Clamp(0f, 1f, audio.volume - step);
        return true;
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

public class SoundObjects
{
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public Sounds sounds;
}
