using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Sounds", fileName = "New Sound Object")]
public class Sounds : ScriptableObject
{
    [SerializeField] public GameObject source;
    [SerializeField] public float volume;
    [SerializeField] public bool dryPlay;
    [SerializeField] public bool rainPlay;
    [SerializeField] public bool stormPlay;
    [SerializeField] public bool windPlay;
    [SerializeField] public float rampOffStep;
}
