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
        Debug.Log("Money Service is initialized.");
    }

    public int GetCurrentMoney()
    {
        return _currentMoney;
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
        SaveService saveService = ServiceLocator.Get<SaveService>();
        MoneySaveData data = saveService.Load<MoneySaveData>("MoneySaveData");

        if (data != null)
        {
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
        SaveService saveService = ServiceLocator.Get<SaveService>();
        MoneySaveData data = new MoneySaveData { currentMoney = _currentMoney };
        saveService.Save(data, "MoneySaveData");
    }
    #endregion
}