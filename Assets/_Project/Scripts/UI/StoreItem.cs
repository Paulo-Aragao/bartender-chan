using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class StoreItem : MonoBehaviour
{
    [BoxGroup("Setup")]
    [SerializeField] protected Color _canBuyColor;
    [BoxGroup("Setup")]
    [SerializeField] protected Color _cannotBuyColor;
    
    [SerializeField] protected TextMeshProUGUI _price;
    [SerializeField] protected Image _border;
    [SerializeField] protected Image _priceConteiner;
    [SerializeField] protected Image _iconBase;
    
}
