using System;
using System.Diagnostics;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using UnityEditor;
using Debug = UnityEngine.Debug;

public class PerformanceDebugTool : OdinEditorWindow
{
    #region Debug Info

    [BoxGroup("Debug Info")]
    [LabelText("Current Money")]
    [ReadOnly]
    public int currentMoney;

    [BoxGroup("Debug Info")]
    [LabelText("Money Per Minute")]
    [ReadOnly]
    public float moneyPerMinute;

    [BoxGroup("Debug Info")]
    [LabelText("FPS")]
    [ReadOnly]
    public float frameRate;

    #endregion

    // Variables for calculating money per minute
    private int previousMoney;
    private float moneyTimer;

    [MenuItem("Window/Performance Debug Tool")]
    private static void OpenWindow()
    {
        GetWindow<PerformanceDebugTool>().Show();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EditorApplication.update += EditorUpdate;

        if (Application.isPlaying)
        {
            currentMoney = ServiceLocator.Get<MoneyService>().GetCurrentMoney();
            previousMoney = currentMoney;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EditorApplication.update -= EditorUpdate;
    }

    private void EditorUpdate()
    {
        if (Application.isPlaying)
        {
            // Update FPS using Time.deltaTime (assumes game is playing)
            frameRate = 1f / Time.deltaTime;

            // Update current money from MoneyService
            currentMoney = ServiceLocator.Get<MoneyService>().GetCurrentMoney();

            // Accumulate deltaTime to compute money per minute
            moneyTimer += Time.deltaTime;
            if (moneyTimer >= 1.5f)
            {
                int diff = currentMoney - previousMoney;
                // Calculate per minute value: diff per second * 60
                moneyPerMinute = (diff / moneyTimer) * 60f;
                previousMoney = currentMoney;
                moneyTimer = 0f;
            }

            Repaint();
        }
    }

    #region Debug Actions

    [BoxGroup("Debug Actions")]
    [Button("Save and Measure Performance (Save)")]
    public void SaveAndMeasurePerformance()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Game is not running. Enter Play Mode to measure performance.");
            return;
        }
        
        SaveService saveService = ServiceLocator.Get<SaveService>();
        MoneySaveData data = new MoneySaveData { currentMoney = currentMoney };

        Stopwatch stopwatch = Stopwatch.StartNew();
        saveService.Save(data, "MoneySaveData");
        stopwatch.Stop();

        Debug.Log("Save operation took: " + stopwatch.ElapsedMilliseconds + " ms");
    }

    [BoxGroup("Debug Actions")]
    [Button("Load and Measure Performance (Load)")]
    public void LoadAndMeasurePerformance()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Game is not running. Enter Play Mode to measure performance.");
            return;
        }
        
        SaveService saveService = ServiceLocator.Get<SaveService>();

        Stopwatch stopwatch = Stopwatch.StartNew();
        MoneySaveData data = saveService.Load<MoneySaveData>("MoneySaveData");
        stopwatch.Stop();

        Debug.Log("Load operation took: " + stopwatch.ElapsedMilliseconds + " ms");
    }

    #endregion

    [Obsolete("Obsolete")]
    protected override void OnGUI()
    {
        
        base.OnGUI();
    }
}
