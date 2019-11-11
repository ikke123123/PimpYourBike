using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Importing Buttons
    public GameObject[] buttons;

    //Togglebutton to know to what the buttons have to be toggled to
    private bool buttonsActive = false;

    //To hide all buttons from the loading of the screen and make the time speed 1 again
    private void Start()
    {
        Time.timeScale = 1.00f;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            PauseGame();
        }
    }

    //This slows game time to 0 and toggles he buttons to be active and working
    public void PauseGame()
    {
        if (Time.timeScale == 1.00f)
        {
            Time.timeScale = 0.00f;
        }
        else if (Time.timeScale == 0.00f)
        {
            Time.timeScale = 1.00f;
        }
        else
        {
            Time.timeScale = 0.00f;
        }

        if (buttonsActive == true)
        {
            ButtonToggle(false);
        }
        else if (buttonsActive == false)
        {
            ButtonToggle(true);
        }
        else
        {
            Debug.Log("Error in UIManager");
        }
    }

    private void ButtonToggle(bool active)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(active);
        }
        buttonsActive = active;
    }
}