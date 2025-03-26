using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Glasses Data", menuName = "MindGG/Glasses Data")]
public class GlassesDataSO : ScriptableObject
{
    public List<GlassData> glasses = new List<GlassData>();
    
    public GlassData GetGlass(GlassType type) => glasses.Find(glass => glass.type == type);
}
