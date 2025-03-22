using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceInitializer : MonoBehaviour
{
    
    [SerializeField] private List<Service> _services;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private void OnEnable()
    {
        InitializeServices();
    }

    private void InitializeServices()
    {
        foreach (var service in _services)
        {
            ServiceLocator.Register(service);
        }
    }
}
