using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaver : ISaver
{
    private readonly string _saveDirectory;
    private readonly BinaryFormatter _formatter;
    public BinarySaver()
    {
        _saveDirectory = Application.persistentDataPath + "/Saves/";
        Directory.CreateDirectory(_saveDirectory);
        _formatter = GetFormatter();
    }

    public IEnumerable<string> GetAll => Directory.GetFiles(_saveDirectory);

    public void Save(SceneData data)
    {
        var savePath = _saveDirectory + DateTime.UtcNow.ToString(
            "ss-mm-hh-dd-MM-yyyy") + ".dat";
        data.Info.Id = savePath;
        using (FileStream stream = File.Create(savePath))
        {
            _formatter.Serialize(stream, data);
        }
    }

    public SceneData Load(string filePath)
    {
        SceneData returnObj;
        using (FileStream file = File.Open(filePath, FileMode.Open))
        {
            object loadedData = _formatter.Deserialize(file);
            returnObj = (SceneData)loadedData;
        }

        return returnObj;
    }

    private BinaryFormatter GetFormatter()
    {
        var formatter = new BinaryFormatter();
        var surrogateSelector = new SurrogateSelector();
        surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(
            StreamingContextStates.All), new Vector3Serializer());
        surrogateSelector.AddSurrogate(typeof(Quaternion), new StreamingContext(
            StreamingContextStates.All), new QuaternionSerializer());
        formatter.SurrogateSelector = surrogateSelector;
        return formatter;
    }

    public void DeleteSave(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
