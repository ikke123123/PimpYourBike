using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteSelector : MonoBehaviour
{
    [Header("Bike Parts")]
    [SerializeField] private SpriteRenderer[] wheelRenderers;
    [SerializeField] private SpriteRenderer frameRenderers;
    [SerializeField] private SpriteRenderer crankRenderers;
    [SerializeField] private SpriteRenderer handleRenderers;
    [SerializeField] private SpriteRenderer pedalRenderers;

    //Various
    [HideInInspector] private DataArray dataArray;
    

    private void Start()
    {
        dataArray = GameObject.Find("Game Manager").GetComponent<DataArray>();

        UpdateSprites();
    }

    public void UpdateSprites()
    {
        foreach (SpriteRenderer spriteRenderer in wheelRenderers)
        {
            TrySpriteChange(spriteRenderer, dataArray.wheelSelected.image);
        }
        TrySpriteChange(frameRenderers, dataArray.frameSelected.image);
        TrySpriteChange(crankRenderers, dataArray.crankSelected.image);
        TrySpriteChange(handleRenderers, dataArray.handleSelected.image);
        TrySpriteChange(pedalRenderers, dataArray.pedalSelected.image);
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