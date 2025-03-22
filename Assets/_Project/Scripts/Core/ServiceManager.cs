using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : MonoBehaviour
{
    #region Singleton
    public static ServiceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    
    [SerializeField] private List<Service> _services;
    
    private void OnEnable()
    {
        foreach (var service in _services)
        {
            ServiceLocator.Register(service);
            service.Init();
        }
    }
}
