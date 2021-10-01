using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    public GameObject pedalObject;
    public GameObject frameObject;
    public GameObject crankObject;
    public GameObject wheelBackObject;
    public GameObject wheelFrontObject;
    public GameObject handleObject;

    public bool moveObjectShowroom;
    public bool reloadSelected;
    public bool oneWheel;

    private SpriteRenderer pedal;
    private SpriteRenderer frame;
    private SpriteRenderer crank;
    private SpriteRenderer wheelBack;
    private SpriteRenderer wheelFront;
    private SpriteRenderer handle;
    private GameObject gameManager;
    private Data pedalData;
    private Data frameData;
    private Data crankData;
    private Data wheelData;
    private Data handleData;
    private int selectedPedal;
    private int selectedFrame;
    private int selectedCrank;
    private int selectedWheel;
    private int selectedHandle;
    private Collider2D[] wheelCollider;
    private PhysicsMaterial2D wheelMaterial;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        pedal = pedalObject.GetComponent<SpriteRenderer>();
        frame = frameObject.GetComponent<SpriteRenderer>();
        crank = crankObject.GetComponent<SpriteRenderer>();
        wheelBack = wheelBackObject.GetComponent<SpriteRenderer>();
        if (gameManager.GetComponent<DataArray>().currentLevel != "Showroom")
        {
            wheelCollider = new Collider2D[2];
            wheelCollider[0] = wheelBackObject.GetComponent<Collider2D>();
        }
        if (!(oneWheel))
        {
            wheelFront = wheelFrontObject.GetComponent<SpriteRenderer>();
            if (gameManager.GetComponent<DataArray>().currentLevel != "Showroom")
            {
                wheelCollider[1] = wheelFrontObject.GetComponent<Collider2D>();
            }
        }
        handle = handleObject.GetComponent<SpriteRenderer>();
        pedalData = gameManager.GetComponent<DataArray>().pedal;
        frameData = gameManager.GetComponent<DataArray>().frame;
        crankData = gameManager.GetComponent<DataArray>().crank;
        wheelData = gameManager.GetComponent<DataArray>().wheel;
        handleData = gameManager.GetComponent<DataArray>().handle;

        ChangeBike();
    }

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
        selectedPedal = pedalData.selected;
        selectedFrame = frameData.selected;
        selectedCrank = selectedFrame;
        selectedWheel = wheelData.selected;
        selectedHandle = handleData.selected;

        SpriteSelector();

        if (!(gameManager.GetComponent<DataArray>().currentLevel.IndexOf("Level") == -1))
        {
            ChangeGrip();
        }
    }

    private void ChangeGrip()
    {
        for (int i = 0; i < wheelCollider.Length; i++)
        {
            if (wheelCollider[i].friction != gameManager.GetComponent<DataArray>().gripModifier)
            {
                wheelMaterial = new PhysicsMaterial2D
                {
                    friction = gameManager.GetComponent<DataArray>().gripModifier, bounciness = 0.1f
                };
                wheelCollider[i].GetComponent<CircleCollider2D>().sharedMaterial = wheelMaterial;
            }
        }
    }

    public void SpriteSelector()
    {
        //Pedal Sprite Changer
        if (!(pedal.sprite = pedalData.sprite[selectedPedal]))
        {
            pedal.sprite = pedalData.sprite[selectedPedal];
        }

        //Frame Sprite Changer
        if (!(frame.sprite = frameData.sprite[selectedFrame]))
        {
            frame.sprite = frameData.sprite[selectedFrame];
        }

        //Crank Sprite Changer
        if (!(crank.sprite = crankData.sprite[selectedCrank]))
        {
            crank.sprite = crankData.sprite[selectedCrank];
        }

        //Wheel back and front Sprite Changer
        if (!(wheelBack.sprite = wheelData.sprite[selectedWheel]))
        {
            wheelBack.sprite = wheelData.sprite[selectedWheel];
        }
        if (!(oneWheel) && !(wheelFront.sprite = wheelData.sprite[selectedWheel]))
        {
            wheelFront.sprite = wheelData.sprite[selectedWheel];
        }

        //Handle Sprite Changer
        if (!(handle.sprite = handleData.sprite[selectedHandle]))
        {
            handle.sprite = handleData.sprite[selectedHandle];
        }

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