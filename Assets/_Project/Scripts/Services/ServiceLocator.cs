using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, Service> _services = new();

    public static void Register<T>(T service) where T : Service
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            Debug.LogWarning($"The service {type} was already registered, it will be replaced.");
        }

        _services[type] = service;
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