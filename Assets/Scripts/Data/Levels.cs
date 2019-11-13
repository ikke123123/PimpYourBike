using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Custom/Level", fileName = "New Level")]
public class Levels : ScriptableObject
{
    [SerializeField] public Object scene;
    [SerializeField] public int absoluteTimeGoal;
}