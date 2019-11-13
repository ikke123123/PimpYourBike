using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //private string targetLevel;
    //private int targetNum;

    //private void Start()
    //{
    //    targetNum = PlayerPrefs.GetInt("targetLevel");
    //    targetLevel = gameObject.GetComponent<DataArray>().level[targetNum];
    //}

    //public void GoToTargetLevel()
    //{
    //    SceneManager.LoadScene(targetLevel, LoadSceneMode.Single);
    //}

    public void ToShowroom()
    {
        if (PlayerPrefs.GetString("hasVisitedControlRoom") == "true")
        {
            SceneManager.LoadScene("Showroom", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Control Room", LoadSceneMode.Single);
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void ToAbout()
    {
        SceneManager.LoadScene("About", LoadSceneMode.Single);
    }

    public void Respawn()
    {
        SceneManager.LoadScene(gameObject.GetComponent<DataArray>().currentLevel, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
