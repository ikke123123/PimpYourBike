using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class DataArray : MonoBehaviour
{
    [Header("Bike Components")]
    [SerializeField] public BikeComponent[] wheels;
    [SerializeField] public BikeComponent[] frames;
    [SerializeField] public BikeComponent[] cranks;
    [SerializeField] public BikeComponent[] handles;
    [SerializeField] public BikeComponent[] pedals;

    //Selected Components
    [HideInInspector] public int wheelSelected;
    [HideInInspector] public int frameSelected;
    [HideInInspector] public int handleSelected;
    [HideInInspector] public int pedalSelected;

    [Header("Weather Modifier")]
    [SerializeField, Range(-2, 0)] public int nominalRainPenalty;
    [SerializeField, Range(-2, 0)] public int nominalStormPenalty;

    [Header("Push and/or Pull")]
    [SerializeField] private bool push = false;
    [SerializeField] private bool pull = false;

    [Header("Level and Time Goal")]
    [SerializeField] public Levels[] levels;
    [HideInInspector] public string currentLevel;
    [HideInInspector] public int currentLevelNum;
    [HideInInspector] public int absoluteTimeGoal;

    private void Start()
    {
        Time.timeScale = 1.00f;

        if (pull == true)
        {
            wheelSelected = PlayerPrefs.GetInt("wheel");
            handleSelected = PlayerPrefs.GetInt("handle");
            frameSelected = PlayerPrefs.GetInt("frame");
            pedalSelected = PlayerPrefs.GetInt("pedal");
        }

        currentLevel = SceneManager.GetActiveScene().name;

        if (currentLevel == "Control Room")
        {
            PlayerPrefs.SetString("hasVisitedControlRoom", "true");
            PlayerPrefs.Save();
        }

        for (int i = 0; i < levels.Length; i++)
        {
            if (GetComponent<DataArray>().currentLevel == levels[i].scene.name)
            {
                currentLevelNum = i;
                absoluteTimeGoal = levels[i].absoluteTimeGoal;
                break;
            }
        }
    }

    private void OnDestroy()
    {
        if (push == true)
        {
            PlayerPrefs.SetInt("wheel", wheelSelected);
            PlayerPrefs.SetInt("handle", handleSelected);
            PlayerPrefs.SetInt("frame", frameSelected);
            PlayerPrefs.SetInt("pedal", pedalSelected);

            PlayerPrefs.Save();
        }
    }

    public void WheelButton(bool up)
    {
        if (up) {
            NextSelection(ref wheelSelected, wheels.Length);
        } else {
            PrevSelection(ref wheelSelected, wheels.Length);
        }
    }

    public void HandleButton(bool up)
    {
        if (up)
        {
            NextSelection(ref handleSelected, handles.Length);
        } else {
            PrevSelection(ref handleSelected, handles.Length);
        }
    }

    public void PedalButton(bool up)
    {
        if (up)
        {
            NextSelection(ref pedalSelected, pedals.Length);
        }
        else
        {
            PrevSelection(ref pedalSelected, pedals.Length);
        }
    }

    public void FrameButton(bool up)
    {
        if (up)
        {
            NextSelection(ref frameSelected, frames.Length);
        }
        else
        {
            PrevSelection(ref frameSelected, frames.Length);
        }
    }

    private void NextSelection(ref int selection, int max)
    {
        selection++;
        if (selection >= max)
        {
            selection = 0;
        }
    }

    private void PrevSelection(ref int selection, int max)
    {
        selection--;
        if (selection < 0)
        {
            selection = max - 1;
        }
    }
}