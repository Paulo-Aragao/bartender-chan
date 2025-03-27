using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    [BoxGroup("Setup")] 
    [SerializeField] private float _successDuration = 2;
    [BoxGroup("Setup")] 
    [SerializeField] private float _failedDuration = 2;
    
    private string _bartenderName;
    public string BartenderName => _bartenderName;
    private BartenderState _currentState;
    public BartenderState CurrentState => _currentState;
    private DrinkDataSO _drinkInProcess;
    private BartenderDataSO _bartenderData;
    
    #region Monobehaviour

    void Start()
    {
        ChangeState(new IdleState());
    }

    #endregion
    

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

    public void Setup( BartenderDataSO data )
    {
        _bartenderData = data;
        _bartenderName = _bartenderData.name;
        _dressSprite.color = _bartenderData.dressColor;
        _hairSprite.color = _bartenderData.hairColor;
        _animator.speed = _bartenderData.speed;
    }

    public void PrepareOrder(DrinkDataSO drinkData)
    {
        _drinkInProcess = drinkData;
        ChangeState(new ShakingState());

        float timeToPrepare = CalculateTimeToPrepare(_drinkInProcess);
        DOVirtual.DelayedCall(timeToPrepare, OnPreparationComplete);
    }

    private float CalculateTimeToPrepare(DrinkDataSO drinkData)
    {
        return drinkData.timeToPrepare/_bartenderData.speed;
    }
    
    private void OnPreparationComplete()
    {
        float chance = CalculateSuccessChance(_drinkInProcess.difficulty, _bartenderData.skill);
        bool success = Random.value <= chance;

        if (success)
        {
            HandleOrderSuccess();
        }
        else
        {
            HandleOrderFailure();
        }
    }

    private void HandleOrderSuccess()
    {
        ServiceLocator.Get<MoneyService>().Add(_drinkInProcess.price);
        ServiceLocator.Get<SoundService>().PlaySFX("Success");
        ChangeState(new SuccessState());
        DOVirtual.DelayedCall(_successDuration, () => ChangeState(new IdleState()));
    }

    private void HandleOrderFailure()
    {
        ChangeState(new FailedState());
        ServiceLocator.Get<SoundService>().PlaySFX("Failed");
        DOVirtual.DelayedCall(_failedDuration, () => ChangeState(new IdleState()));
    }
    public static float CalculateSuccessChance(float drinkDifficulty, float bartenderSkill)
    {
        float normalizedSkill = bartenderSkill;
        float successChance = normalizedSkill * (1f - drinkDifficulty);
        return Mathf.Clamp01(successChance);
    }
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void OnStateChange(string stateName)
    {
        if (stateName == "Success")
        {
            _drinkPreview.gameObject.SetActive(true);
        }
        else
            _drinkPreview.gameObject.SetActive(false);
    }
    public void ChangeState(BartenderState newState)
    {
        _currentState = newState;
        _currentState.Handle(this,OnStateChange);
    }

    public void SetAnimation(string animName)
    {
        _animator.Play(animName);
    }

    #endregion
    
}
