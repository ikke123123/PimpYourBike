using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //Time keeping
    [HideInInspector] private float timeLevelJoin;
    [HideInInspector] public int timeSeconds;
    [HideInInspector] public int timeMinutes;
    [HideInInspector] public int absoluteSeconds;
    [HideInInspector] private int lastNum;
    [HideInInspector] public bool timeStarted = false;

    //Time Goal
    [HideInInspector] private int absoluteTimeGoal;
    [HideInInspector] public int timeGoalSeconds;
    [HideInInspector] public int timeGoalMinutes;
    [HideInInspector] public bool missedTimeGoal = false;

    //Highscores
    [HideInInspector] private int levelHighscore;
    [HideInInspector] public int highscoreMinutes;
    [HideInInspector] public int highscoreSeconds;

    void Start()
    {
        absoluteTimeGoal = GetComponent<DataArray>().absoluteTimeGoal;

        levelHighscore = PlayerPrefs.GetInt("Highscore" + gameObject.GetComponent<DataArray>().currentLevel);

        timeGoalSeconds = 0;
        timeGoalMinutes = 0;

        ToSeconds(ref levelHighscore);
        ToMinutes(ref levelHighscore);
    }

    void Update()
    {
        if (timeStarted == true)
        {
            if (timeLevelJoin == 0)
            {
                timeLevelJoin = Time.time;
            }

            absoluteSeconds = Mathf.FloorToInt(Time.time - timeLevelJoin);

            if (lastNum + 1 <= absoluteSeconds)
            {
                ToSeconds(ref absoluteSeconds);
                ToMinutes(ref absoluteSeconds);
                lastNum = absoluteSeconds;
            }

            if (absoluteTimeGoal <= absoluteSeconds)
            {
                missedTimeGoal = true;
            }

            if (gameObject.GetComponent<FinishManager>().levelCompleted && (levelHighscore == 0 || levelHighscore > absoluteSeconds))
            {
                levelHighscore = absoluteSeconds;
                PlayerPrefs.SetInt("Highscore" + gameObject.GetComponent<DataArray>().currentLevel, levelHighscore);
                PlayerPrefs.Save();
                ToSeconds(ref levelHighscore);
                ToMinutes(ref levelHighscore);
            }
        }
    }

    private void ToSeconds(ref int timeInput)
    {
        timeInput %= 60;
    }

    private void ToMinutes(ref int timeInput)
    {
        timeInput = Mathf.FloorToInt(timeInput / 60);
    }
}
