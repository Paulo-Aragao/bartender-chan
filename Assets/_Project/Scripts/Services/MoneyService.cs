using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MoneyService", menuName = "MindGG/MoneyService")]
public class MoneyService : Service
{
    [ReadOnly]
    public int CurrentMoney;
    [HideInInspector]
    public UnityEvent<int> OnMoneyChanged;

    public void Add(int amount)
    {
        CurrentMoney += amount;
        OnMoneyChanged?.Invoke(CurrentMoney);
    }

    public bool Spend(int amount)
    {
        if (CurrentMoney >= amount)
        {
            CurrentMoney -= amount;
            OnMoneyChanged?.Invoke(CurrentMoney);
            return true;
        }
        return false;
    }

    public void Set(int amount)
    {
        CurrentMoney = amount;
        OnMoneyChanged?.Invoke(CurrentMoney);
    }
}