using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BartenderStoreItem : StoreItem
{
    [SerializeField] private Image _drees;
    [SerializeField] private Image _hair;
    
    private BartenderDataSO _bartenderData;
    private StoreManager _storeManager;

    private void OnEnable()
    {
        ServiceLocator.Get<MoneyService>().OnMoneyChanged.AddListener(UpdateIconBorderColorByMoneyChange);
    }
    private void OnDisable()
    {
        ServiceLocator.Get<MoneyService>().OnMoneyChanged.RemoveListener(UpdateIconBorderColorByMoneyChange);
    }

    private void Start()
    {
        UpdateIconBorderColorByMoneyChange(ServiceLocator.Get<MoneyService>().GetCurrentMoney());
    }

    private void UpdateIconBorderColorByMoneyChange(int amount)
    {
        if(amount >= _bartenderData.wage)
        {
            _border.color = _canBuyColor;
            _priceConteiner.color = _canBuyColor;
        }
        else
        {
            _border.color = _cannotBuyColor;
            _priceConteiner.color = _cannotBuyColor;
        }
    }
    public void Setup(BartenderDataSO bartenderData,StoreManager storeManager)
    {
        _drees.color = bartenderData.dressColor;
        _hair.color = bartenderData.hairColor;
        _price.text = bartenderData.wage.ToString();
        _bartenderData = bartenderData;
        _storeManager = storeManager;
    }
    public void SelectBartender()
    {
        _storeManager.SetupBartenderPreview(_bartenderData);
    }
    
}
