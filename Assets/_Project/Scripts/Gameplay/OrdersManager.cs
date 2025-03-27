using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    [BoxGroup("Dependencies")]
    [SerializeField] private BartendersController _bartenderController;
    
    [BoxGroup("Setup")]
    [SerializeField] private float frequency = 1f;
   
    private List<DrinkDataSO> _orders = new List<DrinkDataSO>();
    private float _timer;
   
    private void Start()
    {
        _orders = ServiceLocator.Get<GameElementsService>().drinks;
        _timer = 0f;
    }
   
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= frequency)
        {
            _timer -= frequency;
            SendOrder();
        }
    }
   
    public void SendOrder()
    {
        _bartenderController.ReceiveOrder(GetRandomOrder());
    }
   
    private DrinkDataSO GetRandomOrder()
    {
        return _orders[Random.Range(0, _orders.Count)];
    }
}