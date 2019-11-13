using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selector : MonoBehaviour
{
    [Header("Bike Parts")]
    [SerializeField] private GameObject pedalObject;
    [SerializeField] private GameObject frameObject;
    [SerializeField] private GameObject crankObject;
    [SerializeField] private GameObject wheelBackObject;
    [SerializeField] private GameObject wheelFrontObject;
    [SerializeField] private GameObject handleObject;

    //Sprite Renderers
    [SerializeField] public SpriteRenderer pedalRenderer;
    [SerializeField] public SpriteRenderer frameRenderer;
    [SerializeField] public SpriteRenderer crankRenderer;
    [SerializeField] public SpriteRenderer wheelBackRenderer;
    [SerializeField] public SpriteRenderer wheelFrontRenderer;
    [SerializeField] public SpriteRenderer handleRenderer;

    [HideInInspector] private GameObject gameManager;


    private void Start()
    {
        gameManager = GameObject.Find("Game Manager");


    }

    public bool moveObjectShowroom;
    public bool reloadSelected;
    public bool oneWheel;

    private int selectedPedal;
    private int selectedFrame;
    private int selectedCrank;
    private int selectedWheel;
    private int selectedHandle;
    private Collider2D[] wheelCollider;
    private PhysicsMaterial2D wheelMaterial;

    //Give This a Place::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //speedModifier = 2f * (wheel.Speed[wheelSelected] + pedal.Speed[pedalSelected] + handle.Speed[handleSelected] + frame.Speed[frameSelected] - 100f);

    //weightModifier = 0.125f * (-pedal.Weight[pedalSelected] - frame.Weight[frameSelected] - wheel.Weight[wheelSelected] - handle.Weight[handleSelected]);

    //gripModifier = 0.1f * (wheel.Grip[wheelSelected] + pedal.Grip[pedalSelected] + handle.Grip[handleSelected] + frame.Grip[frameSelected] - 40f);
    public float speedModifier;
    public float weightModifier;
    public float gripModifier;

    public float dryModifier;
    public float rainModifier;
    public float stormModifier;
    public float windModifier;



    //private float DryModifier()
    //{
    //    float x = 100;
    //    for (int i = 0; i < weather.Length; i++)
    //    {
    //        if (weather[i].dry != 0 && SelectedMatch(i))
    //        {
    //            x += weather[i].dry;
    //        }
    //    }
    //    x = Clamp(0.7f, 1.3f, x * 0.01f);
    //    return x;
    //}

    //private float RainModifier()
    //{
    //    float x = 100;
    //    float unChangedNumbers = wheel.sprite.Length;
    //    for (int i = 0; i < weather.Length; i++)
    //    {
    //        if (weather[i].rain != 0 && SelectedMatch(i))
    //        {
    //            x += weather[i].rain;
    //            unChangedNumbers--;
    //        }
    //    }
    //    x += unChangedNumbers * nominalRainPenalty;
    //    x = Clamp(0.7f, 1.3f, x * 0.01f);
    //    return x;
    //}

    //private float StormModifier()
    //{
    //    float x = 100;
    //    float unChangedNumbers = wheel.sprite.Length;
    //    for (int i = 0; i < weather.Length; i++)
    //    {
    //        if (weather[i].storm != 0 && SelectedMatch(i))
    //        {
    //            x += weather[i].storm;
    //            unChangedNumbers--;
    //        }
    //    }
    //    x += unChangedNumbers * nominalStormPenalty;
    //    x = Clamp(0.7f, 1.3f, x * 0.01f);
    //    return x;
    //}

    //private float WindModifier()
    //{
    //    float x = 100;
    //    for (int i = 0; i < weather.Length; i++)
    //    {
    //        if (weather[i].wind != 0 && SelectedMatch(i))
    //        {
    //            x += weather[i].wind;
    //        }
    //    }
    //    x = Clamp(0.7f, 1.3f, x * 0.01f);
    //    return x;
    //}

    //void Start()
    //{
    //    wheelBack = wheelBackObject.GetComponent<SpriteRenderer>();
    //    if (gameManager.GetComponent<DataArray>().currentLevel != "Showroom")
    //    {
    //        wheelCollider = new Collider2D[2];
    //        wheelCollider[0] = wheelBackObject.GetComponent<Collider2D>();
    //    }
    //    if (!(oneWheel))
    //    {
    //        wheelFront = wheelFrontObject.GetComponent<SpriteRenderer>();
    //        if (gameManager.GetComponent<DataArray>().currentLevel != "Showroom")
    //        {
    //            wheelCollider[1] = wheelFrontObject.GetComponent<Collider2D>();
    //        }
    //    }
    //    handle = handleObject.GetComponent<SpriteRenderer>();
    //    ChangeBike();
    //}

    void Update()
    {
        //Checks whether the selected settings should be reloaded every frame
        if (reloadSelected == true)
        {
            ChangeBike();
        }
    }

    private void ChangeBike()
    {
        //selectedPedal = pedalData.selected;
        //selectedFrame = frameData.selected;
        //selectedCrank = selectedFrame;
        //selectedWheel = wheelData.selected;
        //selectedHandle = handleData.selected;

        SpriteSelector();

        if (!(gameManager.GetComponent<DataArray>().currentLevel.IndexOf("Level") == -1))
        {
            ChangeGrip();
        }
    }

    private void ChangeGrip()
    {
        //for (int i = 0; i < wheelCollider.Length; i++)
        //{
        //    if (wheelCollider[i].friction != gameManager.GetComponent<DataArray>().gripModifier)
        //    {
        //        wheelMaterial = new PhysicsMaterial2D
        //        {
        //            friction = gameManager.GetComponent<DataArray>().gripModifier, bounciness = 0.1f
        //        };
        //        wheelCollider[i].GetComponent<CircleCollider2D>().sharedMaterial = wheelMaterial;
        //    }
        //}
    }

    public void SpriteSelector()
    {
        ////Pedal Sprite Changer
        //if (!(pedal.sprite = pedalData.sprite[selectedPedal]))
        //{
        //    pedal.sprite = pedalData.sprite[selectedPedal];
        //}

        ////Frame Sprite Changer
        //if (!(frame.sprite = frameData.sprite[selectedFrame]))
        //{
        //    frame.sprite = frameData.sprite[selectedFrame];
        //}

        ////Crank Sprite Changer
        //if (!(crank.sprite = crankData.sprite[selectedCrank]))
        //{
        //    crank.sprite = crankData.sprite[selectedCrank];
        //}

        ////Wheel back and front Sprite Changer
        //if (!(wheelBack.sprite = wheelData.sprite[selectedWheel]))
        //{
        //    wheelBack.sprite = wheelData.sprite[selectedWheel];
        //}
        //if (!(oneWheel) && !(wheelFront.sprite = wheelData.sprite[selectedWheel]))
        //{
        //    wheelFront.sprite = wheelData.sprite[selectedWheel];
        //}

        ////Handle Sprite Changer
        //if (!(handle.sprite = handleData.sprite[selectedHandle]))
        //{
        //    handle.sprite = handleData.sprite[selectedHandle];
        //}

        //To make the handle simage centered
        if (moveObjectShowroom == true)
        {
            if (selectedHandle == 0)
            {
                handleObject.transform.position = new Vector3(7.6f, handleObject.transform.position.y, handleObject.transform.position.z);
            }
            else
            {
                handleObject.transform.position = new Vector3(8.4f, handleObject.transform.position.y, handleObject.transform.position.z);
            }
        }
    }
}