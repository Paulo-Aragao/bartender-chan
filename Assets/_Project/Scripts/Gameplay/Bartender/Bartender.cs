using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Bartender : MonoBehaviour
{
    [BoxGroup("References")]
    [SerializeField] private Animator _animator;
    [BoxGroup("References")]
    [SerializeField] private SpriteRenderer _baseSprite;
    [BoxGroup("References")]
    [SerializeField] private SpriteRenderer _dressSprite;
    [BoxGroup("References")]
    [SerializeField] private SpriteRenderer _hairSprite;
    [BoxGroup("References")]
    [SerializeField] private DrinkPreview _drinkPreview;
    [BoxGroup("References")]
    [SerializeField] private SpriteRenderer _clientSprite;
    
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

    #region Methods

    public void Setup( BatenderDataSO data )
    {
        _dressSprite.color = data.dressColor;
        _hairSprite.color = data.hairColor;
        //_clientSprite.sprite = data.ClientSprite; TODO:client sprite
    }
    public void ChangeState(BartenderState newState)
    {
        currentState = newState;
        currentState.Handle(this);
    }

    public void SetAnimation(string animName)
    {
        _animator.Play(animName);
    }

    #endregion
    
}
