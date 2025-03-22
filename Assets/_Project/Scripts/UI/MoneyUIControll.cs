using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MoneyUIControll : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    public void UpdateMoneyUI(int newAmount)
    {
        _moneyText.text = newAmount.ToString();
    }
}
