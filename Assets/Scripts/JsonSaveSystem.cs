using System;
using System.IO;
using UnityEngine;

public class JsonSaveSystem : ISaveSystem
{ 
    private readonly string _filePath;

    public JsonSaveSystem()
    {
        _filePath = Application.persistentDataPath + "/Save.json";
    }

    public void Save(SaveData data)
    {
        var json = JsonUtility.ToJson(data);
        using (var writer = new StreamWriter(_filePath))
        {
            writer.WriteLine(json);
        }
    }

    public SaveData Load()
    {
        string json = "";
        using (var reader = new StreamReader(_filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                json += line;
            }
        }

        if (string.IsNullOrEmpty(json))
        {
            return new SaveData();
        }

        return JsonUtility.FromJson<SaveData>(json);
    }
}