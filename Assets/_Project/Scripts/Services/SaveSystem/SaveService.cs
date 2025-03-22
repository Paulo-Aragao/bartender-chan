using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveService", menuName = "MindGG/SaveService")]
public class SaveService : Service
{
    public override void Init()
    {
        Debug.Log("Save Service is initialized.");
    }
    private string SavePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName + ".json");
    }

    public void Save<T>(T data, string fileName)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath(fileName), json);
    }

    public T Load<T>(string fileName) where T : new()
    {
        string path = SavePath(fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }

        return new T();
    }

    public bool Exists(string fileName)
    {
        return File.Exists(SavePath(fileName));
    }
}
