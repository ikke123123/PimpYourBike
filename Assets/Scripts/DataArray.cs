using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class DataArray : MonoBehaviour
{
    [Header("Bike Components")]
    [SerializeField] public BikeComponent[] wheels;
    [SerializeField] public BikeComponent[] frames;
    [SerializeField] public BikeComponent[] handles;
    [SerializeField] public BikeComponent[] pedals;

    [Header("Weather Modifier")]
    [SerializeField, Range(-2, 0)] public int nominalRainPenalty;
    [SerializeField, Range(-2, 0)] public int nominalStormPenalty;

    [Header("Push and/or Pull")]
    [SerializeField] private bool push = false;
    [SerializeField] private bool pull = false;

    [Header("Level and Time Goal")]
    [SerializeField] public Levels[] levels;
    [SerializeField] public int currentLevel;
    [HideInInspector] public int absoluteTimeGoal;
    [HideInInspector] private string currentSceneName;

    //Selected Components
    [HideInInspector] public BikeComponent wheelSelected;
    [HideInInspector] public BikeComponent frameSelected;
    [HideInInspector] public BikeComponent handleSelected;
    [HideInInspector] public BikeComponent pedalSelected;

    //Selected Components Identifier
    [HideInInspector] private int wheelSelectedInt;
    [HideInInspector] private int frameSelectedInt;
    [HideInInspector] private int handleSelectedInt;
    [HideInInspector] private int pedalSelectedInt;

    private void Start()
    {
        Time.timeScale = 1.00f;

        if (pull == true)
        {
            wheelSelectedInt = PlayerPrefs.GetInt("wheel");
            handleSelectedInt = PlayerPrefs.GetInt("handle");
            frameSelectedInt = PlayerPrefs.GetInt("frame");
            pedalSelectedInt = PlayerPrefs.GetInt("pedal");
        }

        wheelSelected = StoreSelectedComponent(wheelSelectedInt, wheels);
        handleSelected = StoreSelectedComponent(handleSelectedInt, handles);
        pedalSelected = StoreSelectedComponent(pedalSelectedInt, pedals);
        frameSelected = StoreSelectedComponent(frameSelectedInt, frames);

        currentSceneName = SceneManager.GetActiveScene().name;

        if (LevelNameToNum(ref currentLevel, currentSceneName))
        {
            absoluteTimeGoal = levels[currentLevel].absoluteTimeGoal;
        }

        if (currentSceneName == "Control Room")
        {
            PlayerPrefs.SetString("hasVisitedControlRoom", "true");
            PlayerPrefs.Save();
        }
    }

    private void OnDestroy()
    {
        if (push == true)
        {
            PlayerPrefs.SetInt("wheel", wheelSelectedInt);
            PlayerPrefs.SetInt("handle", handleSelectedInt);
            PlayerPrefs.SetInt("frame", frameSelectedInt);
            PlayerPrefs.SetInt("pedal", pedalSelectedInt);

            PlayerPrefs.Save();
        }
    }

    public void WheelButton(bool up)
    {
        if (up)
        {
            NextSelection(ref wheelSelectedInt, wheels.Length);
        }
        else
        {
            PrevSelection(ref wheelSelectedInt, wheels.Length);
        }
        wheelSelected = StoreSelectedComponent(wheelSelectedInt, wheels);
        PushBikeUpdate();
    }

    public void HandleButton(bool up)
    {
        if (up)
        {
            NextSelection(ref handleSelectedInt, handles.Length);
        }
        else
        {
            PrevSelection(ref handleSelectedInt, handles.Length);
        }
        handleSelected = StoreSelectedComponent(handleSelectedInt, handles);
        PushBikeUpdate();
    }

    public void PedalButton(bool up)
    {
        if (up)
        {
            NextSelection(ref pedalSelectedInt, pedals.Length);
        }
        else
        {
            PrevSelection(ref pedalSelectedInt, pedals.Length);
        }
        pedalSelected = StoreSelectedComponent(pedalSelectedInt, pedals);
        PushBikeUpdate();
    }

    public void FrameButton(bool up)
    {
        if (up)
        {
            NextSelection(ref frameSelectedInt, frames.Length);
        }
        else
        {
            PrevSelection(ref frameSelectedInt, frames.Length);
        }
        frameSelected = StoreSelectedComponent(frameSelectedInt, frames);
        PushBikeUpdate();
    }

    private void PushBikeUpdate()
    {
        GetComponent<DataUpdatePusher>().BikeUpdate();
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

    private BikeComponent StoreSelectedComponent(int selected, BikeComponent[] origin)
    {
        return origin[selected];
    }

    private bool LevelNameToNum(ref int target, string levelName)
    {
        if (levelName.IndexOf("Level ") != -1)
        {
            target = Convert.ToInt32(levelName.Replace("Level ", ""));
            target--;
            return true;
        }
        target--;
        return false;
    }
}