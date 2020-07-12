using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSystem : ISaveSystem
{
    private const string LEVEL_KEY = "m_level";
    private const string NAME_KEY = "m_name";
    private const string TUTORIAL_KEY = "m_tutorial";

    public void Save(SaveData data)
    {
        PlayerPrefs.SetInt(LEVEL_KEY, data.Level);
        PlayerPrefs.SetString(NAME_KEY, data.Name);
        PlayerPrefs.SetInt(TUTORIAL_KEY, data.IsTutorialCompleted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public SaveData Load()
    {
        var result = new SaveData();
        if (PlayerPrefs.HasKey(LEVEL_KEY))
        {
            result.Level = PlayerPrefs.GetInt(LEVEL_KEY);
        }
        if (PlayerPrefs.HasKey(NAME_KEY))
        {
            result.Name = PlayerPrefs.GetString(NAME_KEY);
        }
        if (PlayerPrefs.HasKey(TUTORIAL_KEY))
        {
            result.IsTutorialCompleted = 
                PlayerPrefs.GetInt(TUTORIAL_KEY) == 1;
        }
        return result;
    }
}
