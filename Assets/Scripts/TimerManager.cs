using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public GameObject[] timers;

    private GameObject gameManager;
    private GameObject timer;
    private Text timerText;
    private GameObject goal;
    private Text goalText;
    private GameObject highscore;
    private Text highscoreText;
    private TimeManager timeManager;
    private bool timerActive = true;
    public bool deactivateOnTime;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        timeManager = gameManager.GetComponent<TimeManager>();

        for (int i = 0; i < timers.Length; i++)
        {
            if (!(timers[i].name.IndexOf("Time") == -1))
            {
                timer = timers[i];
                timerText = timer.GetComponent<Text>();
            } else if (!(timers[i].name.IndexOf("Goal") == -1))
            {
                goal = timers[i];
                goalText = goal.GetComponent<Text>();
            } else if (!(timers[i].name.IndexOf("Highscore") == -1))
            {
                highscore = timers[i];
                highscoreText = highscore.GetComponent<Text>();
            }
        }

        goalText.text = DisplayText(timeManager.goalSeconds, timeManager.goalMinutes, "Goal");
        highscoreText.text = DisplayText(timeManager.highscoreSeconds, timeManager.highscoreMinutes, "Highscore");
    }

    void Update()
    {
        if (Time.timeScale == 0.00f && timerActive == true)
        {
            if (deactivateOnTime)
            {
                TimerToggle();
            }
        }
        else if (Time.timeScale == 1.00f && timerActive == false)
        {
            if (deactivateOnTime)
            {
                TimerToggle();
            }
        }

        if (!(gameManager.GetComponent<FinishManager>().levelCompleted))
        {
            timerText.text = DisplayText(timeManager.timeSeconds, timeManager.timeMinutes, "Time");
        }

        if (gameManager.GetComponent<FinishManager>().levelCompleted)
        {
            highscoreText.text = DisplayText(timeManager.highscoreSeconds, timeManager.highscoreMinutes, "Highscore");
        }
    }

    private string DisplayText(int seconds, int minutes, string name)
    {
        string x = name + ": " + minutes.ToString("00") + ":" + seconds.ToString("00");
        return x;
    }

    private void TimerToggle()
    {
        if (timerActive == false)
        {
            for (int i = 0; i < timers.Length; i++)
            {
                timers[i].SetActive(true);
            }
            timerActive = true;
        } else
        {
            for (int i = 0; i < timers.Length; i++)
            {
                timers[i].SetActive(false);
            }
            timerActive = false;
        }
    }
}
