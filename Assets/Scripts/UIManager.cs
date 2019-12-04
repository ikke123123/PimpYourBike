using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject[] objectsToDisableOnPause;

    private void Start()
    {
        SetButtons(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (Time.timeScale == 1.00f)
        {
            Time.timeScale = 0.00f;
            SetButtons(true);
            SetObjects(false);
        }
        else
        {
            Time.timeScale = 1.00f;
            SetButtons(false);
            SetObjects(true);
        }
    }

    private void SetButtons(bool active)
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(active);
        }
    }

    private void SetObjects(bool active)
    {
        foreach (GameObject gameObject in objectsToDisableOnPause)
        {
            gameObject.SetActive(active);
        }
    }
}