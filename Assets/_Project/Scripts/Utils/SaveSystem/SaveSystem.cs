using System.IO;
using UnityEngine;
public static class SaveSystem
{
    private static string SavePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName + ".json");
    }

    public static void Save<T>(T data, string fileName)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath(fileName), json);
    }

    public static T Load<T>(string fileName) where T : new()
    {
        string path = SavePath(fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }

        return new T();
    }

    public static bool Exists(string fileName)
    {
        return File.Exists(SavePath(fileName));
    }
}