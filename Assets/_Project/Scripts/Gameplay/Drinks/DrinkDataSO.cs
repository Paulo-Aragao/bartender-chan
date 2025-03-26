using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Drink Data", menuName = "MindGG/Drink Data")]
public class DrinkDataSO : GameEntitySO
{
    [BoxGroup("Dependencies")] 
    public GlassesDataSO GlassesData;
    
    [ValueDropdown("GetColorOptions")] 
    public Color drinkColor;
    public GlassType glassType;
    public int cost;
    [Range(0,1)]
    public float difficulty;
    public int price;
    public float timeToPrepare;
    
    public Sprite GetBaseGlassSprite(){
        return GlassesData.GetGlass(glassType).spriteBase;
    }
    public Sprite GetColoroableGlassSprite(){
        return GlassesData.GetGlass(glassType).spriteColorable;
    }
}
