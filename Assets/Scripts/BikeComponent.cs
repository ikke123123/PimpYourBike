using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Bike/BikeComponent", fileName = "New Bike Component")]
public class BikeComponent : ScriptableObject
{
    [SerializeField] public Sprite image;
    [SerializeField] public float speed;
    [SerializeField] public float grip;
}
