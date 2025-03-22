using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : MonoBehaviour
{
    [SerializeField] private List<Service> _services;
    
    private void Start()
    {
        foreach (var service in _services)
        {
            ServiceLocator.Register(service);
        }
    }
}
