using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Tests : MonoBehaviour, IBootDependency
{
    private UnityEvent finished = new UnityEvent();
    public UnityEvent OnReady => finished;
    
    private void Start()
    {
        DOVirtual.DelayedCall(5, () => { finished?.Invoke(); });
    }

}
