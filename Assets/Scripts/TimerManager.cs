using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Header("Timers")]
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI goal; 

    [HideInInspector] private TimeManager timeManager;
    [HideInInspector] private FinishManager finishManager;
    [HideInInspector] private bool timeStarted;
    [HideInInspector] private bool highScoreHasChanged;

    private void Start()
    {
        GameObject gameManager = GameObject.Find("Game Manager");
        timeManager = gameManager.GetComponent<TimeManager>();
        finishManager = gameManager.GetComponent<FinishManager>();

        timer.text = "Time: 00:00";
        highScore.text = TimeToString(timeManager.highscoreSeconds, timeManager.highscoreMinutes, "Highscore");
        goal.text = TimeToString(timeManager.timeGoalSeconds, timeManager.timeGoalMinutes, "Goal");
    }

    private void Update()
    {
        if (timeStarted)
        {
            timer.text = TimeToString(timeManager.timeSeconds, timeManager.timeMinutes, "Time");

            if (finishManager.levelCompleted || highScoreHasChanged == false)
            {
                highScoreHasChanged = true;
                highScore.text = TimeToString(timeManager.highscoreSeconds, timeManager.highscoreMinutes, "Highscore");
                return;
            }
            timer.text = TimeToString(timeManager.timeSeconds, timeManager.timeMinutes, "Time");
            return;
        }
        timeStarted = timeManager.timeStarted;
    }

    private string TimeToString(int seconds, int minutes, string name)
    {
        return name + ": " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
