using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "Bartender Data", menuName = "MindGG/Bartender Data")]
public class BartenderDataSO : GameEntitySO
{
    [ValueDropdown("GetColorOptions")]
    public Color hairColor;
    [ValueDropdown("GetColorOptions")]
    public Color dressColor;
    public int speed;
    public int skill;
    public int wage;
}
