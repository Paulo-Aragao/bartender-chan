using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [BoxGroup("references")]
    [SerializeField] private MoneyUIControll moneyUIControll;
    private void OnEnable()
    {
        ServiceLocator.Get<MoneyService>().OnMoneyChanged.AddListener(moneyUIControll.UpdateMoneyUI);
    }

    private void OnDisable()
    {
        ServiceLocator.Get<MoneyService>().OnMoneyChanged.RemoveListener(moneyUIControll.UpdateMoneyUI);
    }

    
}
