using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ServicesInitializer : MonoBehaviour, IBootDependency
{
    
    [SerializeField] private List<Service> _services;
    
    [SerializeField] private UnityEvent allServicesRegisteds;
    public UnityEvent OnReady => allServicesRegisteds;

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

        allServicesRegisteds?.Invoke();
    }
}
