using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    [SerializeField] public float nominalSpeed;
    
    [HideInInspector] public float currentSpeed;
    [HideInInspector] public float currentWeight;
    [HideInInspector] public float currentGrip;
    [HideInInspector] private GameObject gameManager;

    //Regular modifier
    [HideInInspector] private float speedModified;
    [HideInInspector] private float weightModified;
    [HideInInspector] private float gripModified;

    //Percentile penalty for weather
    [HideInInspector] private float dryModifier;
    [HideInInspector] private float rainModifier;
    [HideInInspector] private float stormModifier;
    [HideInInspector] private float windModifier;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager");

        UpdateValues();

        currentSpeed = speedModified;
        currentWeight = weightModified;
        currentGrip = gripModified;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wind"))
        {
            currentSpeed = speedModified * windModifier;
            currentGrip = gripModified * windModifier;
        }
        if (collision.CompareTag("Rain"))
        {
            currentSpeed = speedModified * rainModifier;
            currentGrip = gripModified * rainModifier;
        }
        if (collision.CompareTag("Storm"))
        {
            currentSpeed = speedModified * rainModifier;
            currentGrip = gripModified * rainModifier;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        currentSpeed = speedModified;
        currentGrip = gripModified;
    }

    public void UpdateValues()
    {
        BikeComponent[] selectedComponents = new BikeComponent[]
        {
            gameManager.GetComponent<DataArray>().wheels[gameManager.GetComponent<DataArray>().wheelSelected],
            gameManager.GetComponent<DataArray>().frames[gameManager.GetComponent<DataArray>().frameSelected],
            gameManager.GetComponent<DataArray>().handles[gameManager.GetComponent<DataArray>().handleSelected],
            gameManager.GetComponent<DataArray>().pedals[gameManager.GetComponent<DataArray>().pedalSelected]
        };

        ModifierCalculator(ref selectedComponents, ref speedModified, ref weightModified, ref gripModified, ref dryModifier, ref rainModifier, ref stormModifier, ref windModifier, gameManager.GetComponent<DataArray>().nominalRainPenalty, gameManager.GetComponent<DataArray>().nominalStormPenalty, nominalSpeed);
    }

    private void ModifierCalculator(ref BikeComponent[] components, ref float speed, ref float weight, ref float grip, ref float dry, ref float rain, ref float storm, ref float wind, float rainPenalty, float stormPenalty, float stockSpeed)
    {
        for (int i = 0; i < components.Length; i++) {
            speed += components[i].speed;
            weight += components[i].weight;
            grip += components[i].grip;

            dry += components[i].dryModifier;
            rain += ApplyPenalty(components[i].rainModifier, rainPenalty);
            storm += ApplyPenalty(components[i].stormModifier, stormPenalty);
            wind += components[i].windModifier;
        }

        //Remap variables for desired results
        Remap(ref speed, 4 * 1.0f, 4 * 4.0f, stockSpeed - 100, stockSpeed + 100);
        Remap(ref weight, 4 * 1.0f, 4 * 4.0f, 5, 25);
        Remap(ref grip, 4 * 1.0f, 4 * 4.0f, 0.1f, 0.5f);

        dry = Mathf.Clamp(dry, 0.7f, 1.3f);
        rain = Mathf.Clamp(rain, 0.7f, 1.3f);
        storm = Mathf.Clamp(storm, 0.7f, 1.3f);
        wind = Mathf.Clamp(wind, 0.7f, 1.3f);
    }

    private float ApplyPenalty(int input, float penalty)
    {
        if (input == 0)
        {
            return penalty;
        }
        return input * 1.0f;
    }

    private void Remap(ref float value, float min, float max, float tarMin, float tarMax)
    {
        value = value * ((tarMax - tarMin) / (max - min)) + tarMin;
    }
}