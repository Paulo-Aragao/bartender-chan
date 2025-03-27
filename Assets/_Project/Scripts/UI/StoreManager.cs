using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [BoxGroup("Dependencies")]
    [SerializeField] private BartendersController _bartendersController;

    [SerializeField] private SlotSelector _slotSelector;
    
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private Image _previewImage;
    [SerializeField] private Image _dreesImage;
    [SerializeField] private Image _hairImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _skillsText;
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private Button _buyButton;
    
    [BoxGroup("Prefabs")]
    [SerializeField] private BartenderStoreItem _itemPrefab;
    
    private BartenderDataSO _selectedBartender;

    private void Start()
    {
        SetupBartendersItens();
    }

    private void SetupBartendersItens()
    {
        GameElementsService gameElementsService = ServiceLocator.Get<GameElementsService>();
        foreach (BartenderDataSO bartenderData in gameElementsService.bartenders)
        {
            Instantiate(_itemPrefab, _itemsParent).Setup(bartenderData, this);
        }
    }
    
    public void SetupBartenderPreview(BartenderDataSO bartenderData)
    {
        _selectedBartender = bartenderData;
        _dreesImage.color = bartenderData.dressColor;
        _hairImage.color = bartenderData.hairColor;
        _nameText.text = "Name: " + bartenderData.name;
        _skillsText.text = "Skills: " + bartenderData.skill.ToString();
        _speedText.text = "Speed: " + bartenderData.speed.ToString();
        _priceText.text = "Wage: " + bartenderData.wage.ToString();
        _buyButton.interactable = _selectedBartender.wage <= ServiceLocator.Get<MoneyService>().GetCurrentMoney();
    }

    public void TryBuyBartender()
    {
        
        if (_bartendersController.ActiveBartendersCount == 5)
        {
            _slotSelector.gameObject.SetActive(true);
        }
        else
        {
            BuyBartender(-1);
        }
    }
    public void BuyBartender(int index)
    {
        ServiceLocator.Get<MoneyService>().Spend(_selectedBartender.wage);;
        _bartendersController.AddBartender(index, _selectedBartender);
        gameObject.gameObject.SetActive(false);
    }
}
