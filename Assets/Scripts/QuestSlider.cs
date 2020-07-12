using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class QuestProgressEvent : UnityEvent<string, float>
{
}

public class QuestSlider : Slider
{
    public QuestProgressEvent OnQuestUpdated;

    public string Id { get; private set; }

    private Text _label;

    public void Init(Quest quest)
    {
        _label = GetComponentInChildren<Text>();
        Id = quest.Id;
        _label.text = quest.Id;
        value = quest.Progress;
        onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(float val)
    {
        OnQuestUpdated?.Invoke(Id, val);
    }
}
