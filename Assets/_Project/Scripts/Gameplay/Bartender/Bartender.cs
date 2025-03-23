using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Bartender : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private BartenderState currentState;

    void Start()
    {
        ChangeState(new IdleState());
    }

    #region Debug

    [Button][FoldoutGroup("Debug")]
    public void Shake() => ChangeState(new ShakingState());
    [Button][FoldoutGroup("Debug")]
    public void Success() => ChangeState(new SuccessState());
    [Button][FoldoutGroup("Debug")]
    public void Failed() => ChangeState(new FailedState());
    [Button][FoldoutGroup("Debug")]
    public void Idle() => ChangeState(new IdleState());

    #endregion
    
    public void ChangeState(BartenderState newState)
    {
        currentState = newState;
        currentState.Handle(this);
    }

    public void SetAnimation(string animName)
    {
        _animator.Play(animName);
    }
}
