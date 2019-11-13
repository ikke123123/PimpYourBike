using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Bike Component", fileName = "New Bike Component")]
public class BikeComponent : ScriptableObject
{
    [SerializeField] public Sprite image;
    [SerializeField, Range(1, 4)] public int speed;
    [SerializeField, Range(1, 4)] public int grip;
    [SerializeField, Range(1, 4)] public int weight;
    [SerializeField, Range(-5, 5)] public int dryModifier;
    [SerializeField, Range(-5, 5)] public int rainModifier;
    [SerializeField, Range(-5, 5)] public int stormModifier;
    [SerializeField, Range(-5, 5)] public int windModifier;
}
