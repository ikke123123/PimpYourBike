using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableController : MonoBehaviour
{
    [Header("Statistics")]
    [SerializeField, Range(400, 1200)] private float nominalSpeed;
    [SerializeField, Range(50, 300)] private float rotationSpeed;

    [Header("FPS Time")]
    [SerializeField, Range(0.5f, 10)] private float refreshTime;
    [HideInInspector] private int frameCounter = 0;
    [HideInInspector] private float timeCounter = 0.0f;
    [HideInInspector] public float lastFramerate = 60;

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
    [HideInInspector] public float currentRotationSpeed;

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

    private void Update()
    {
        //FPS Timer
        if (timeCounter < refreshTime)
        {
            timeCounter += Time.deltaTime;
            frameCounter++;
        }
        else
        {
            lastFramerate = frameCounter / timeCounter;
            frameCounter = 0;
            timeCounter = 0.0f;

            currentRotationSpeed = (60 / lastFramerate) * rotationSpeed;
            GetComponent<BikeUpdatePusher>().UpdateRotation(currentRotationSpeed);
        }
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
        GetComponent<BikeUpdatePusher>().UpdateSpeed(currentSpeed);
        UpdateGrip();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        currentSpeed = speedModified;
        currentGrip = gripModified;
        GetComponent<BikeUpdatePusher>().UpdateSpeed(currentSpeed);
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
        currentRotationSpeed = rotationSpeed;

        //Applying Changes to Bike
        UpdateGrip();
        UpdateWeight();
        GetComponent<BikeUpdatePusher>().UpdateSpeed(currentSpeed);
    }

    public void UpdateSprites(BikeComponent[] components)
    {
        foreach (SpriteRenderer spriteRenderer in wheelRenderers)
        {
            TrySpriteChange(spriteRenderer, components[0].image);
        }
        TrySpriteChange(frameRenderer, components[1].image);
        TrySpriteChange(crankRenderer, components[1].altImage);
        TrySpriteChange(handleRenderer, components[2].image);
        TrySpriteChange(pedalRenderer, components[3].image);
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