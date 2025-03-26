using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BartendersController : MonoBehaviour
{
    [SerializeField] private List<Bartender> _bartenders = new List<Bartender>();

    public void Start()
    {
        LoadBartenders();
    }
    public void LoadBartenders()
    {
        GameElementsService gameElementsService = ServiceLocator.Get<GameElementsService>();
        SaveService saveService = ServiceLocator.Get<SaveService>();
        SaveData saveData = saveService.Load<SaveData>();
        if (saveData != null)
        {
            for (int i = 0; i < saveData.bartenderNanmes.Length; i++)
            {
                string bartenderName = saveData.bartenderNanmes[i];
                if (!string.IsNullOrEmpty(bartenderName))
                {
                    _bartenders[i].Setup(gameElementsService.GetBartenderDataByName(bartenderName));
                }
            }
        }
    }
}
