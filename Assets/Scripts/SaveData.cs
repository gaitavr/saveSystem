using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int Level;
    public string Name;
    public bool IsTutorialCompleted;

    public List<Quest> Quests;
}

[Serializable]
public class Quest
{
    public string Id;
    public string[] QuestsToOpen;
    public float Progress;
}
