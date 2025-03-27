using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkPreview : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _base;
    public SpriteRenderer Base {
        get { return _base; }
        set { _base = value; }
    }
    
    [SerializeField] private SpriteRenderer _colorable;
    public SpriteRenderer Colorable {
        get { return _colorable; }
        set { _colorable = value; }
    }

    public void Setup(DrinkDataSO drinkData)
    {
        _base.sprite = drinkData.GetBaseGlassSprite();
        _colorable.sprite = drinkData.GetColoroableGlassSprite();
        _colorable.color = drinkData.drinkColor;
    }
}
