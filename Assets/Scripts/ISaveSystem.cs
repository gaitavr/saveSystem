using System;

public interface ISaveSystem
{
    void Save(SaveData data);

    SaveData Load();
}
