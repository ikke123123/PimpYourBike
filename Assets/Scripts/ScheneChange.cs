using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScheneChange : MonoBehaviour
{
    public string scene;

    public void SceneChangeSingle()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void SceneChangeAdditive()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
