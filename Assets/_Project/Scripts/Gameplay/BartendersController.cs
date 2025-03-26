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

    public void LoadBartenders()
    {
        GameElementsService gameElementsService = ServiceLocator.Get<GameElementsService>();
        SaveService saveService = ServiceLocator.Get<SaveService>();
        SaveData saveData = saveService.Load<SaveData>();
        _activeBartendersCount = 0;
        if (saveData != null)
        {
            if (string.IsNullOrEmpty(saveData.bartenderNanmes[0]))
            {
                saveData.bartenderNanmes = new string[_bartenders.Count];
                _bartenders[0].Setup(gameElementsService.GetBartenderDataByName("Lucy"));
                SaveBartenders();
                _bartenders[0].SetActive(true);
                saveData = saveService.Load<SaveData>();
            }
            for (int i = 0; i < saveData.bartenderNanmes.Length; i++)
            {
                string bartenderName = saveData.bartenderNanmes[i];
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
        SaveData data = new SaveData { bartenderNanmes = new string[_bartenders.Count] };
        for (int i = 0; i < _bartenders.Count; i++)
        {
            if (_bartenders[i].gameObject.activeSelf)
            {
                data.bartenderNanmes[i] = _bartenders[i].BartenderName;
            }
        }
        saveService.Save(data, "SaveData");
    }
}
