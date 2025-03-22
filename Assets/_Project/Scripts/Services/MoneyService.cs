using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MoneyService", menuName = "MindGG/MoneyService")]
public class MoneyService : Service
{
    [ReadOnly] 
    private int _currentMoney;
    [HideInInspector]
    public UnityEvent<int> OnMoneyChanged;

    public override void Init()
    {
        Load();
    }

    public void Add(int amount)
    {
        _currentMoney += amount;
        OnMoneyChanged?.Invoke(_currentMoney);
    }

    public bool Spend(int amount)
    {
        if (_currentMoney >= amount)
        {
            _currentMoney -= amount;
            OnMoneyChanged?.Invoke(_currentMoney);
            return true;
        }
        return false;
    }

    public void Set(int amount)
    {
        _currentMoney = amount;
        OnMoneyChanged?.Invoke(_currentMoney);
    }

    #region SaveSystem
    public void Load()
    {
        if (SaveSystem.Exists("MoneySaveData"))
        {
            MoneySaveData data = SaveSystem.Load<MoneySaveData>("MoneySaveData");
            _currentMoney = data.currentMoney;
            OnMoneyChanged?.Invoke(_currentMoney);
        }
        else
        {
            _currentMoney = 0;
            OnMoneyChanged?.Invoke(_currentMoney);
        }
    }

    public void Save()
    {
        MoneySaveData data = new MoneySaveData { currentMoney = _currentMoney };
        SaveSystem.Save(data, "MoneySaveData");
    }
    #endregion
}