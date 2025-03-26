using System;
using UnityEngine;

[Serializable]
public class GlassData 
{
    public GlassType type;
    public Sprite spriteBase;
    public Sprite spriteColorable;
}

public enum GlassType 
{ 
    Martine, 
    PocoGrande,
    Highball
}
