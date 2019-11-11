using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishManager : MonoBehaviour
{
    public bool levelCompleted = false;

    private bool isInLevel;
    private int timeEnd;
    private GameObject bikeRoot;
    private GameObject finish;
    private GameObject start;

    void Start()
    {
        if (!(gameObject.GetComponent<DataArray>().currentLevel.IndexOf("Level") == -1))
        {
            isInLevel = true;
            bikeRoot = GameObject.Find("BikeRoot");
            finish = GameObject.Find("Finish");
            start = GameObject.Find("Start");
            if (finish == null)
            {
                Debug.Log("Please place a Finish in the game <3");
            }
            if (start == null)
            {
                Debug.Log("Please place a Start in the game <3");
            }
        }
    }

    void Update()
    {
        if (isInLevel)
        {
            if (bikeRoot.transform.position.x + 2.5f >= start.transform.position.x)
            {
                gameObject.GetComponent<TimeManager>().timeStarted = true;
            }

            if ((bikeRoot.transform.position.x + 2.5f >= finish.transform.position.x - 0.5f) && gameObject.GetComponent<TimeManager>().missedGoal == false)
            {
                levelCompleted = true;
            }

            if (levelCompleted)
            {
                if (timeEnd == 0)
                {
                    timeEnd = gameObject.GetComponent<TimeManager>().absoluteSeconds + 3;
                }
                else if (timeEnd == gameObject.GetComponent<TimeManager>().absoluteSeconds)
                {
                    Time.timeScale = 0.00f;
                    GetComponent<GameManager>().ToMainMenu();
                }
            }
        }
    }
}
