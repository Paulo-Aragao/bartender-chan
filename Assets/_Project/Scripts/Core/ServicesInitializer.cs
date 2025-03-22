using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ServicesInitializer : MonoBehaviour, IBootDependency
{
    [InfoBox("The order of this list is the order in which the services are started, services that are essential to others must be started first.")]
    [SerializeField] private List<Service> _services;
    
    [SerializeField] private UnityEvent allServicesRegisteds;
    public UnityEvent OnReady => allServicesRegisteds;

    private void Start()
    {
        InitializeServices();
    }

    private void InitializeServices()
    {
        foreach (Service service in _services)
        {
            ServiceLocator.Register(service);
            service.Init();
        }

        Debug.Log("All services are initialized.");
        allServicesRegisteds?.Invoke();
    }
}
