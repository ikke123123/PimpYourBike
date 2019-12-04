using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Parallax Object", fileName = "New Parallax Object")]
public class ParallaxObject : ScriptableObject
{
    [SerializeField] public GameObject parallaxObject;
    [SerializeField, Range(-1f, 1f)] public float hSpeed;
    [SerializeField, Range(-1f, 1f)] public float vSpeed;
    [SerializeField, Range(-1f, 1f)] public float constantSpeed;
}
