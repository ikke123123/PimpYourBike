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

        ToSeconds(ref timeGoalSeconds, absoluteTimeGoal);
        ToMinutes(ref timeGoalMinutes, absoluteTimeGoal);

        levelHighscore = PlayerPrefs.GetInt("Highscore" + gameObject.GetComponent<DataArray>().currentLevel.ToString());

        ToSeconds(ref highscoreSeconds, levelHighscore);
        ToMinutes(ref highscoreMinutes, levelHighscore);

        Debug.Log(highscoreSeconds);
        Debug.Log(highscoreMinutes);
    }

    private void Update()
    {
        if (timeStarted)
        {
            if (timeLevelJoin == 0)
            {
                timeLevelJoin = Time.time;
            }

            absoluteSeconds = Mathf.FloorToInt(Time.time - timeLevelJoin);

            if (lastNum + 1 <= absoluteSeconds)
            {
                ToSeconds(ref timeSeconds, absoluteSeconds);
                ToMinutes(ref timeMinutes, absoluteSeconds);
                lastNum = absoluteSeconds;
            }

            if (absoluteTimeGoal <= absoluteSeconds)
            {
                missedTimeGoal = true;
            }

            if (GetComponent<FinishManager>().levelCompleted && (levelHighscore == 0 || levelHighscore > absoluteSeconds))
            {
                levelHighscore = absoluteSeconds;
                PlayerPrefs.SetInt("Highscore" + gameObject.GetComponent<DataArray>().currentLevel, levelHighscore);
                PlayerPrefs.Save();
                ToSeconds(ref highscoreSeconds, levelHighscore);
                ToMinutes(ref highscoreMinutes, levelHighscore);
            }
            return;
        }
        timeStarted = GetComponent<TimeManager>().timeStarted;
    }

    private void ToSeconds(ref int timeOutput, int timeInput)
    {
        timeOutput %= 60;
    }

    private void ToMinutes(ref int timeOutput, int timeInput)
    {
        timeOutput = Mathf.FloorToInt(timeInput / 60);
    }
}
