using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MoneyUIControll : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;

    private void Start()
    {
        _moneyText.text = ServiceLocator.Get<MoneyService>().GetCurrentMoney().ToString();
    }

    public void UpdateMoneyUI(int newAmount)
    {
        _moneyText.text = newAmount.ToString();
    }
    [Button]
    public void SaveMoney()
    {
        ServiceLocator.Get<MoneyService>().Save();
    }
    [Button]
    public void SetRandomMoney()
    {
        int amount = Random.Range(0, 10000000);
        ServiceLocator.Get<MoneyService>().Set(amount);
        ServiceLocator.Get<MoneyService>().Save();
    }
}
