using System;
using System.Collections;
using System.Collections.Generic;

public interface ISaver
{
    IEnumerable<string> GetAll { get; }

    void Save(SceneData data);

    SceneData Load(string filePath);

    void DeleteSave(string filePath);
}
