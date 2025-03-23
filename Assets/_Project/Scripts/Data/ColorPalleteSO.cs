using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Pallete Data", menuName = "MindGG/Pallete Data")]
public class ColorPalleteSO : ScriptableObject
{
    public List<ColorData> colors = new List<ColorData>();
}
