using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SlotSelector : MonoBehaviour
{
    [BoxGroup("Depedencies")]
    [SerializeField] private StoreManager _storeManager;

    public void SelectSlot(int index)
    {
        _storeManager.BuyBartender(index);
        gameObject.SetActive(false);
    }
}
