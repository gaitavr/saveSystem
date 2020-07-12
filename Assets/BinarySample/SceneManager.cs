using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private SavePanel _savePanel;
    [SerializeField]
    private ScreenshotMaker _screenshotMaker;

    private ISaver _saver;

    private List<InteractableObject> _objects;

    private void Awake()
    {
        _saver = new BinarySaver();
        _objects = FindObjectsOfType<InteractableObject>().ToList();
        _savePanel.SaveRequested += OnSaveRequested;
        _savePanel.LoadRequested += OnLoadRequested;
        _savePanel.SetSaver(_saver);
    }

    private void OnSaveRequested()
    {
        var sceneData = new SceneData()
        {
            Items = new List<Item>()
        };
        foreach (var obj in _objects)
        {
            var interactionData = obj.Get();
            sceneData.Items.Add(new Item()
            {
                Id = obj.Id,
                Position = interactionData.Position,
                Rotation = interactionData.Rotation
            });
        }
        sceneData.Info = new SaveInfo()
        {
            Icon = _screenshotMaker.MakePhoto(),
        };
        _saver.Save(sceneData);
        _savePanel.Add(sceneData.Info);
    }

    private void OnLoadRequested(string save)
    {
        var sceneData = _saver.Load(save);
        foreach (var item in sceneData.Items)
        {
            foreach (var obj in _objects)
            {
                if (obj.Id == item.Id)
                {
                    obj.Set(new InteractableData()
                    {
                        Position = item.Position,
                        Rotation = item.Rotation
                    });
                }
            }
        }
    }
}
