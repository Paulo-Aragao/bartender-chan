using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, Service> _services = new();

    public static void Register(Service service)
    {
        Type key = service.GetType();
        if (!_services.ContainsKey(key))
        {
            _services.Add(key, service);
            Debug.Log("Serviço " + key + " registrado.");
        }
        else
        {
            Debug.LogWarning("Serviço " + key + " já está registrado.");
        }
    }

    public static T Get<T>() where T : Service
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
        {
            return (T)service;
        }

        Debug.LogError($"Service {type} not found.");
        return null;
    }

    public static void ClearAll()
    {
        _services.Clear();
    }
}