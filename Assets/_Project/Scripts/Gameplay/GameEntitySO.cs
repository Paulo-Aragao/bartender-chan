using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class GameEntitySO : ScriptableObject
{
    [BoxGroup("Dependencies")]
    public ColorPalleteSO colorPallete;
    [Space(5)]
    public string name;
    protected virtual IEnumerable<ValueDropdownItem<Color>> GetColorOptions()
    {
        if (colorPallete != null)
        {
            foreach (var colorData in colorPallete.colors)
            {
                yield return new ValueDropdownItem<Color>(colorData.label, colorData.color);
            }
        }
    }
}
