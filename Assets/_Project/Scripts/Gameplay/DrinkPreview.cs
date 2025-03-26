using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkPreview : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _base;
    [SerializeField] private SpriteRenderer _colorable;
    
    public void Setup(DrinkDataSO data)
    {
        _base.sprite = data.GetBaseGlassSprite();
        _colorable.sprite = data.GetColoroableGlassSprite();
    }
}
