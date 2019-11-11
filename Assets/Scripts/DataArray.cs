using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class DataArray : MonoBehaviour
{
    public BikeComponent[] wheels;

    public Data wheel;
    public Data handle;
    public Data frame;
    public Data crank;
    public Data pedal;
    public bool push;
    public bool pull;
    public WeatherExceptions[] weather;
    public float nominalRainPenalty;
    public float nominalStormPenalty;

    public string[] level;
    public int[] goal;
    public string currentLevel;
    public int currentGoal;
    public bool hasVisitedControls;

    public float speedModifier;
    public float weightModifier;
    public float gripModifier;

    public float dryModifier;
    public float rainModifier;
    public float stormModifier;
    public float windModifier;

    private void Start()
    {
        Time.timeScale = 1.00f;

        PlayerPrefs.SetInt("targetLevel", 0);

        if (pull == true)
        {
            wheel.selected = PlayerPrefs.GetInt("wheel");
            handle.selected = PlayerPrefs.GetInt("handle");
            frame.selected = PlayerPrefs.GetInt("frame");
            pedal.selected = PlayerPrefs.GetInt("pedal");

            speedModifier = 2f * (wheel.Speed[wheel.selected] + pedal.Speed[pedal.selected] + handle.Speed[handle.selected] + frame.Speed[frame.selected] - 100f);

            weightModifier = 0.125f * (-pedal.Weight[pedal.selected] - frame.Weight[frame.selected] - wheel.Weight[wheel.selected] - handle.Weight[handle.selected]);

            gripModifier = 0.1f * (wheel.Grip[wheel.selected] + pedal.Grip[pedal.selected] + handle.Grip[handle.selected] + frame.Grip[frame.selected] - 40f);

            dryModifier = DryModifier();
            rainModifier = RainModifier();
            stormModifier = StormModifier();
            windModifier = WindModifier();
        }

        currentLevel = SceneManager.GetActiveScene().name;

        if (currentLevel == "Control Room")
        {
            hasVisitedControls = true;
        }
        else
        {
            hasVisitedControls = false;
        }

        for (int i = 0; i < level.Length; i++)
        {
            if (currentLevel == level[i])
            {
                currentGoal = goal[i];
            }
        }
    }

    private void Update()
    {
        if (push == true)
        {
            PlayerPrefs.SetInt("wheel", wheel.selected);
            PlayerPrefs.SetInt("handle", handle.selected);
            PlayerPrefs.SetInt("frame", frame.selected);
            PlayerPrefs.SetInt("pedal", pedal.selected);

            weightModifier = 0.125f * (-pedal.Weight[pedal.selected] - frame.Weight[frame.selected] - wheel.Weight[wheel.selected] - handle.Weight[handle.selected]);

            speedModifier = 2f * (wheel.Speed[wheel.selected] + pedal.Speed[pedal.selected] + handle.Speed[handle.selected] + frame.Speed[frame.selected] - 100f);

            gripModifier = 0.1f * (wheel.Grip[wheel.selected] + pedal.Grip[pedal.selected] + handle.Grip[handle.selected] + frame.Grip[frame.selected] - 40f);

            dryModifier = DryModifier();
            rainModifier = RainModifier();
            stormModifier = StormModifier();
            windModifier = WindModifier();

            PlayerPrefs.Save();
        }
    }

    public void WheelUpButton()
    {
        wheel.selected = UpButton(wheel.selected);
    }

    public void WheelDownButton()
    {
        wheel.selected = DownButton(wheel.selected);
    }

    public void HandleUpButton()
    {
        handle.selected = UpButton(handle.selected);
    }

    public void HandleDownButton()
    {
        handle.selected = DownButton(handle.selected);
    }

    public void PedalUpButton()
    {
        pedal.selected = UpButton(pedal.selected);
    }

    public void PedalDownButton()
    {
        pedal.selected = DownButton(pedal.selected);
    }

    public void FrameUpButton()
    {
        frame.selected = UpButton(frame.selected);
    }

    public void FrameDownButton()
    {
        frame.selected = DownButton(frame.selected);
    }

    private int UpButton(int x)
    {
        if (x < wheel.sprite.Length - 1)
        {
            x += 1;
        }
        else
        {
            x = 0;
        }
        return x;
    }

    private int DownButton(int x)
    {
        if (x > 0)
        {
            x -= 1;
        }
        else
        {
            x = wheel.sprite.Length - 1;
        }
        return x;
    }

    private float DryModifier()
    {
        float x = 100;
        for (int i = 0; i < weather.Length; i++)
        {
            if (weather[i].dry != 0 && SelectedMatch(i))
            {
                x += weather[i].dry;
            }
        }
        x = Clamp(0.7f, 1.3f, x * 0.01f);
        return x;
    }

    private float RainModifier()
    {
        float x = 100;
        float unChangedNumbers = wheel.sprite.Length;
        for (int i = 0; i < weather.Length; i++)
        {
            if (weather[i].rain != 0 && SelectedMatch(i))
            {
                x += weather[i].rain;
                unChangedNumbers--;
            }
        }
        x += unChangedNumbers * nominalRainPenalty;
        x = Clamp(0.7f, 1.3f, x * 0.01f);
        return x;
    }

    private float StormModifier()
    {
        float x = 100;
        float unChangedNumbers = wheel.sprite.Length;
        for (int i = 0; i < weather.Length; i++)
        {
            if (weather[i].storm != 0 && SelectedMatch(i))
            {
                x += weather[i].storm;
                unChangedNumbers--;
            }
        }
        x += unChangedNumbers * nominalStormPenalty;
        x = Clamp(0.7f, 1.3f, x * 0.01f);
        return x;
    }

    private float WindModifier()
    {
        float x = 100;
        for (int i = 0; i < weather.Length; i++)
        {
            if (weather[i].wind != 0 && SelectedMatch(i))
            {
                x += weather[i].wind;
            }
        }
        x = Clamp(0.7f, 1.3f, x * 0.01f);
        return x;
    }

    private float Clamp(float min, float max, float input)
    {
        float output;
        if (input > max)
        {
            output = max;
        } else if (input < min)
        {
            output = min;
        } else
        {
            output = input;
        }
        return output;
    }

    private bool SelectedMatch(int x)
    {
        bool y = false;
        if (!(weather[x].elementType.IndexOf("Wheel") == -1))
        {
            if (wheel.selected == weather[x].elementNo)
            {
                y = true;
            }
        }
        else if (!(weather[x].elementType.IndexOf("Handle") == -1))
        {
            if (handle.selected == weather[x].elementNo)
            {
                y = true;
            }
        }
        else if (!(weather[x].elementType.IndexOf("Frame") == -1))
        {
            if (frame.selected == weather[x].elementNo)
            {
                y = true;
            }
        }
        else if (!(weather[x].elementType.IndexOf("Pedal") == -1))
        {
            if (pedal.selected == weather[x].elementNo)
            {
                y = true;
            }
        }
        return y;
    }
}

[System.Serializable]
public class Data
{
    public int selected;
    public Sprite[] sprite;
    public float[] Speed;
    public float[] Grip;
    public float[] Weight;
}

[System.Serializable]
public class WeatherExceptions
{
    public string elementType;
    public int elementNo;
    public float dry;
    public float rain;
    public float storm;
    public float wind;
}