using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Game Elements Service", menuName = "MindGG/Game Elements Service")]
public class GameElementsService : Service
{
    public List<BartenderDataSO> bartenders = new List<BartenderDataSO>();
    public List<DrinkDataSO> drinks = new List<DrinkDataSO>();

    public override void Init()
    {
        
    }

    public BartenderDataSO GetBartenderDataByName(string name)
    {
        return bartenders.Find(x => x.name == name);
    }
}
