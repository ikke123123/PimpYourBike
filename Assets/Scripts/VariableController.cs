using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableController : MonoBehaviour
{
    //Standard Speed
    [Header("Statistics")]
    [SerializeField, Range(400, 1200)] private float nominalSpeed;

    //Current Floats
    [HideInInspector] public float currentSpeed;
    [HideInInspector] public float currentWeight;
    [HideInInspector] public float currentGrip;

    //Regular modifier
    [HideInInspector] private float speedModified;
    [HideInInspector] private float weightModified;
    [HideInInspector] private float gripModified;

    //Percentile penalty for weather
    [HideInInspector] private float dryModifier;
    [HideInInspector] private float rainModifier;
    [HideInInspector] private float stormModifier;
    [HideInInspector] private float windModifier;

    //Various
    [HideInInspector] private GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager");

        UpdateValues();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Wind":
                currentSpeed = speedModified * windModifier;
                currentGrip = gripModified * windModifier;
                break;
            case "Rain":
                currentSpeed = speedModified * rainModifier;
                currentGrip = gripModified * rainModifier;
                break;
            case "Storm":
                currentSpeed = speedModified * rainModifier;
                currentGrip = gripModified * rainModifier;
                break;
        }
        GetComponent<BikeUpdatePusher>().UpdateSpeed();
        GetComponent<BikeUpdatePusher>().UpdateGrip();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        currentSpeed = speedModified;
        currentGrip = gripModified;
        GetComponent<BikeUpdatePusher>().UpdateSpeed();
        GetComponent<BikeUpdatePusher>().UpdateGrip();
    }

    public void UpdateValues()
    {
        BikeComponent[] selectedComponents = new BikeComponent[]
        {
            gameManager.GetComponent<DataArray>().wheelSelected,
            gameManager.GetComponent<DataArray>().frameSelected,
            gameManager.GetComponent<DataArray>().handleSelected,
            gameManager.GetComponent<DataArray>().pedalSelected
        };

        ModifierCalculator(selectedComponents, ref speedModified, ref weightModified, ref gripModified, ref dryModifier, ref rainModifier, ref stormModifier, ref windModifier, gameManager.GetComponent<DataArray>().nominalRainPenalty, gameManager.GetComponent<DataArray>().nominalStormPenalty, nominalSpeed);

        currentSpeed = speedModified;
        currentWeight = weightModified;
        currentGrip = gripModified;

        GetComponent<BikeUpdatePusher>().UpdateSpeed();
        GetComponent<BikeUpdatePusher>().UpdateGrip();
        GetComponent<BikeUpdatePusher>().UpdateWeight();
    }

    private void ModifierCalculator(BikeComponent[] components, ref float speed, ref float weight, ref float grip, ref float dry, ref float rain, ref float storm, ref float wind, float rainPenalty, float stormPenalty, float stockSpeed)
    {
        foreach (BikeComponent bikeComponent in components)
        {
            speed += bikeComponent.speed;
            weight += bikeComponent.weight;
            grip += bikeComponent.grip;

            dry += bikeComponent.dryModifier;
            rain += ApplyPenalty(bikeComponent.rainModifier, rainPenalty);
            storm += ApplyPenalty(bikeComponent.stormModifier, stormPenalty);
            wind += bikeComponent.windModifier;
        }

        //Remap variables for desired results
        Remap(ref speed, components.Length, Mathf.Pow(components.Length, 2), stockSpeed - 100, stockSpeed + 100);
        Remap(ref weight, components.Length, Mathf.Pow(components.Length, 2), 5, 25);
        Remap(ref grip, components.Length, Mathf.Pow(components.Length, 2), 0.1f, 0.5f);

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