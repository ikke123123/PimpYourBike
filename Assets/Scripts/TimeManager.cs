using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int timeSeconds;
    public int timeMinutes;
    public int goalSeconds;
    public int goalMinutes;
    public int highscoreMinutes;
    public int highscoreSeconds;
    public int absoluteSeconds;
    public bool missedGoal = false;
    public bool timeStarted = false;

    private string currentLevel;
    private int levelHighscore;
    private float timeLevelJoin;
    private int absoluteGoal;

    void Start()
    {
        currentLevel = gameObject.GetComponent<DataArray>().currentLevel;

        levelHighscore = PlayerPrefs.GetInt("Highscore" + currentLevel);

        absoluteGoal = gameObject.GetComponent<DataArray>().currentGoal;

        goalSeconds = absoluteGoal % 60;
        goalMinutes = Mathf.FloorToInt(absoluteGoal / 60);

        highscoreSeconds = levelHighscore % 60;
        highscoreMinutes = Mathf.FloorToInt(levelHighscore / 60);
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
        }

        timeSeconds = absoluteSeconds % 60;
        timeMinutes = Mathf.FloorToInt(absoluteSeconds / 60);

        if (absoluteGoal <= absoluteSeconds)
        {
            missedGoal = true;
        }

        if (gameObject.GetComponent<FinishManager>().levelCompleted && (levelHighscore == 0 || levelHighscore > absoluteSeconds))
        {
            levelHighscore = absoluteSeconds;
            PlayerPrefs.SetInt("Highscore" + currentLevel, levelHighscore);
            PlayerPrefs.Save();
            highscoreSeconds = levelHighscore % 60;
            highscoreMinutes = Mathf.FloorToInt(levelHighscore / 60);
        }
    }
}
