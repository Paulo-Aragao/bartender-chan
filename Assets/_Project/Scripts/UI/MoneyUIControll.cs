using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoneyUIControll : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    
    [SerializeField] private float numberTweenDuration = 0.5f;
    [SerializeField] private float popDuration = 0.2f;
    [SerializeField] private float popScale = 1.2f;

    private void Start()
    {
        _moneyText.text = ServiceLocator.Get<MoneyService>().GetCurrentMoney().ToString();
    }

    public void UpdateMoneyUI(int newAmount)
    {
        int currentAmount = 0;
        int.TryParse(_moneyText.text, out currentAmount);
        DOTween.To(() => currentAmount, x => {
                currentAmount = x;
                _moneyText.text = currentAmount.ToString();
            }, newAmount, numberTweenDuration)
            .SetEase(Ease.OutCubic);
        
        _moneyText.transform.DOScale(popScale, popDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
                _moneyText.transform.DOScale(1f, popDuration)
                    .SetEase(Ease.InBack)
            );
    }

    public void OnDisable()
    {
        SaveMoney();
    }

    [Button]
    public void SaveMoney()
    {
        ServiceLocator.Get<MoneyService>().Save();
    }
    
    [Button]
    public void SetRandomMoney()
    {
        int amount = Random.Range(0, 10000000);
        ServiceLocator.Get<MoneyService>().Set(amount);
        ServiceLocator.Get<MoneyService>().Save();
    }
}