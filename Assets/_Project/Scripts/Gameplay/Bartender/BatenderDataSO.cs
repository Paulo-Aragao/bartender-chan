using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Bartender Data", menuName = "MindGG/Bartender Data")]
public class BatenderDataSO : ScriptableObject
{
    public string Name;
    public Color HairColor;
    public Color DressColor;
    public int Speed;
    public int Skill;
    public int Wage;
}
