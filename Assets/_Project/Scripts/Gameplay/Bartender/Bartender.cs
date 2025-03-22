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
    [Button]
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
