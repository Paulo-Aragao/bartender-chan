using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Boot : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> dependenciesToBoot;
    [SerializeField] private string nextSceneName = "Game";
    
    private int remainingDependencies;

    private void OnEnable()
    {
        RegisterDependencies();
    }

    private void RegisterDependencies()
    {
        remainingDependencies = 0;
        
        foreach (MonoBehaviour dependency in dependenciesToBoot)
        {
            if (dependency is IBootDependency bootDependency)
            {
                remainingDependencies++;
                bootDependency.OnReady.AddListener(OnDependencyReady);
            }
            else
            {
                Debug.LogWarning($"{dependency.name} is not an IBootDependency.");
            }
        }

        // if not has dependencies, start the game
        if (remainingDependencies == 0)
        {
            LoadNextScene();
        }
    }

    private void OnDependencyReady()
    {
        remainingDependencies--;
        if (remainingDependencies <= 0)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        ScenesController.Instance.LoadSceneAsync(nextSceneName);
    }
}
