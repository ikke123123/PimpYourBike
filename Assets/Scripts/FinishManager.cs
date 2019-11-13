using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishManager : MonoBehaviour
{
    [HideInInspector] public bool levelCompleted = false;

    [HideInInspector] private bool isInLevel;
    [HideInInspector] private int timeEnd;
    [HideInInspector] private GameObject bikeRoot;
    [HideInInspector] private GameObject finish;
    [HideInInspector] private GameObject start;

    void Start()
    {
        if (gameObject.GetComponent<DataArray>().absoluteTimeGoal != 0)
        {
            isInLevel = true;
            bikeRoot = GameObject.Find("BikeRoot");
            finish = GameObject.Find("Finish");
            start = GameObject.Find("Start");
            if (finish == null)
            {
                Debug.Log("Please place a finish in the game <3");
            }
            if (start == null)
            {
                Debug.Log("Please place a start in the game <3");
            }
            if (bikeRoot == null)
            {
                Debug.Log("Please place a bike in the game <3");
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

                if (levelCompleted == true || (bikeRoot.transform.position.x + 2.5f >= finish.transform.position.x - 0.5f) && gameObject.GetComponent<TimeManager>().missedTimeGoal == false)
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
                    levelCompleted = true;
                }
            }
        }
    }
}
