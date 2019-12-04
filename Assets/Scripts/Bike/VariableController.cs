using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableController : MonoBehaviour
{
    [Header("Statistics")]
    [SerializeField, Range(400, 1200)] private float nominalSpeed;

    [Header("Bike Parts")]
    [SerializeField] private CircleCollider2D[] wheelColliders;
    [SerializeField] private SpriteRenderer[] wheelRenderers;
    [SerializeField] private SpriteRenderer frameRenderer;
    [SerializeField] private SpriteRenderer crankRenderer;
    [SerializeField] private SpriteRenderer handleRenderer;
    [SerializeField] private SpriteRenderer pedalRenderer;
    [HideInInspector] private Rigidbody2D bikeRigidBody;

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
    [HideInInspector] private DataArray dataArray;

    private void Start()
    {
        dataArray = GameObject.Find("Game Manager").GetComponent<DataArray>();
        bikeRigidBody = GetComponent<Rigidbody2D>();

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
        GetComponent<BikeController>().UpdateSpeed(currentSpeed);
        UpdateGrip();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        currentSpeed = speedModified;
        currentGrip = gripModified;
        GetComponent<BikeController>().UpdateSpeed(currentSpeed);
        UpdateGrip();
    }

    public void UpdateValues()
    {
        BikeComponent[] selectedComponents = new BikeComponent[]
        {
            dataArray.wheelSelected,
            dataArray.frameSelected,
            dataArray.handleSelected,
            dataArray.pedalSelected
        };

        UpdateSprites(selectedComponents);

        ModifierCalculator(selectedComponents, ref speedModified, ref weightModified, ref gripModified, ref dryModifier, ref rainModifier, ref stormModifier, ref windModifier, dataArray.nominalRainPenalty, dataArray.nominalStormPenalty, nominalSpeed);

        //Set Current Values to New Values
        currentSpeed = speedModified;
        currentWeight = weightModified;
        currentGrip = gripModified;

        //Applying Changes to Bike
        UpdateGrip();
        UpdateWeight();
        GetComponent<BikeController>().UpdateSpeed(currentSpeed);
    }

    private void UpdateSprites(BikeComponent[] components)
    {
        foreach (SpriteRenderer spriteRenderer in wheelRenderers)
        {
            if (TrySpriteChange(spriteRenderer, components[0].image) == false) Debug.LogError("Sprite Attempt Failed");
        }
        if (TrySpriteChange(frameRenderer, components[1].image) == false) Debug.LogError("Sprite Attempt Failed");
        if (TrySpriteChange(crankRenderer, components[1].altImage) == false) Debug.LogError("Sprite Attempt Failed");
        if (TrySpriteChange(handleRenderer, components[2].image) == false) Debug.LogError("Sprite Attempt Failed");
        if (TrySpriteChange(pedalRenderer, components[3].image) == false) Debug.LogError("Sprite Attempt Failed");
    }

    private void UpdateGrip()
    {
        foreach (CircleCollider2D wheelCollider in wheelColliders)
        {
            if (wheelCollider.sharedMaterial == null || wheelCollider.sharedMaterial.friction != currentGrip)
            {
                wheelCollider.sharedMaterial = new PhysicsMaterial2D
                {
                    friction = currentGrip,
                    bounciness = 0.1f
                };
            }
        }
    }

    private void UpdateWeight()
    {
        bikeRigidBody.mass = currentWeight;
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
        Remap(ref grip, components.Length, Mathf.Pow(components.Length, 2), 0.5f, 1f);

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

    private bool TrySpriteChange(SpriteRenderer spriteObject, Sprite sprite)
    {
        if (spriteObject != null)
        {
            spriteObject.sprite = sprite;
            return true;
        }
        return false;
    }
}