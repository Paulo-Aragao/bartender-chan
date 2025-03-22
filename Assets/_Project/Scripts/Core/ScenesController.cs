using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    #region Singleton

    public static ScenesController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void LoadSceneAsync(string sceneName, Action onSceneLoaded = null)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName, onSceneLoaded));
    }

    public UnityEvent<float> OnSceneLoadProgress;

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName, Action onSceneLoaded)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOperation.isDone)
        {
            OnSceneLoadProgress?.Invoke(asyncOperation.progress);
            yield return null;
        }
        onSceneLoaded?.Invoke();
    }
    
}
