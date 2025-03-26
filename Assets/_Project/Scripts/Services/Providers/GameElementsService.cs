using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Elements Service", menuName = "MindGG/Game Elements Service")]
public class GameElementsService : Service
{
    public List<BartenderDataSO> _bartenders = new List<BartenderDataSO>();
    public List<DrinkDataSO> _drinks = new List<DrinkDataSO>();

    public override void Init()
    {
        
    }

    public BartenderDataSO GetBartenderDataByName(string name)
    {
        return _bartenders.Find(x => x.name == name);
    }
}
