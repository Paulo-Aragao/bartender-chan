using System.Collections.Generic;
using UnityEngine;

public class BartendersController : MonoBehaviour
{
    [SerializeField] private List<Bartender> _bartenders = new List<Bartender>();
    
    [SerializeField] private float spacing = 3.5f;
    
    private int _activeBartendersCount;
    
    public int ActiveBartendersCount => _activeBartendersCount;

    public void Start()
    {
        LoadBartenders();
    }

    public void ReceiveOrder(DrinkDataSO order)
    {
        foreach (Bartender bartender in _bartenders)
        {
            //compare current state type
            if (bartender.gameObject.activeSelf == false)
                continue;
            if (bartender.CurrentState.GetType() == typeof(IdleState))
            {
                bartender.PrepareOrder(order);
                break;
            }
        }
    }

    public void LoadBartenders()
    {
        GameElementsService gameElementsService = ServiceLocator.Get<GameElementsService>();
        SaveService saveService = ServiceLocator.Get<SaveService>();
        BartenderSaveData bartenderData = saveService.Load<BartenderSaveData>("BartenderSaveData");
        _activeBartendersCount = 0;
        if (bartenderData != null)
        {
            if (bartenderData.bartenderNanmes != null)
            {
                if (string.IsNullOrEmpty(bartenderData.bartenderNanmes[0]))
                {
                    bartenderData.bartenderNanmes = new string[_bartenders.Count];
                    _bartenders[0].Setup(gameElementsService.GetBartenderDataByName("Lucy"));
                    SaveBartenders();
                    _bartenders[0].SetActive(true);
                    bartenderData = saveService.Load<BartenderSaveData>("BartenderSaveData");
                }
                for (int i = 0; i < bartenderData.bartenderNanmes.Length; i++)
                {
                    string bartenderName = bartenderData.bartenderNanmes[i];
                    if (!string.IsNullOrEmpty(bartenderName))
                    {
                        _bartenders[i].Setup(gameElementsService.GetBartenderDataByName(bartenderName));
                        _bartenders[i].SetActive(true);
                        _activeBartendersCount++;
                    }
                    else
                    {
                        _bartenders[i].SetActive(false);
                    }
                }
            }
            else
            {
                bartenderData.bartenderNanmes = new string[_bartenders.Count];
                _bartenders[0].Setup(gameElementsService.GetBartenderDataByName("Lucy"));
                SaveBartenders();
                _bartenders[0].SetActive(true);
            }
        }
        else
        {
            bartenderData.bartenderNanmes = new string[_bartenders.Count];
            _bartenders[0].Setup(gameElementsService.GetBartenderDataByName("Lucy"));
            SaveBartenders();
            _bartenders[0].SetActive(true);
        }
        
        ArrangeBartenders();
    }
    
    private void ArrangeBartenders()
    {
        List<Bartender> activeBartenders = new List<Bartender>();
        foreach (var bartender in _bartenders)
        {
            if (bartender.gameObject.activeSelf)
                activeBartenders.Add(bartender);
        }

        int count = activeBartenders.Count;
        if (count == 0)
            return;

        float startX = -((count - 1) * spacing) / 2f;
        
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = activeBartenders[i].transform.position;
            pos.x = startX + i * spacing;
            activeBartenders[i].transform.position = pos;
        }
        _activeBartendersCount = activeBartenders.Count;
    }

    public void AddBartender(int index,BartenderDataSO bartenderData)
    {
        if (index == -1)
        {
            foreach (var bartender in _bartenders)
            {
                if (!bartender.gameObject.activeSelf)
                {
                    bartender.Setup(bartenderData);
                    bartender.SetActive(true);
                    break;
                }
            }
        }
        else
        {
            _bartenders[index-1].Setup(bartenderData);
            _bartenders[index-1].SetActive(true);
        }
        
        SaveBartenders();
        ArrangeBartenders();
    }
    public void SaveBartenders()
    {
        SaveService saveService = ServiceLocator.Get<SaveService>();
        BartenderSaveData data = new BartenderSaveData { bartenderNanmes = new string[_bartenders.Count] };
        for (int i = 0; i < _bartenders.Count; i++)
        {
            if (_bartenders[i].gameObject.activeSelf)
            {
                data.bartenderNanmes[i] = _bartenders[i].BartenderName;
            }
        }
        saveService.Save(data, "BartenderSaveData");
    }
}
