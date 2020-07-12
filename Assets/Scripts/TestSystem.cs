using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TestSystem : MonoBehaviour
{
    #region DISPLAY

    [SerializeField]
    private Text _infoLabel;

    [SerializeField]
    private InputField _nameField;

    [SerializeField]
    private Button _decreaseLevelBtn;

    [SerializeField]
    private Button _increaseLevelBtn;

    [SerializeField]
    private Toggle _tutorialToggle;

    [SerializeField]
    private GameObject _questContainer;

    [SerializeField]
    private QuestSlider _questPrefab;

    private List<QuestSlider> _sliders = new List<QuestSlider>();

    private void CreateQuests()
    {
        foreach (var q in _myData.Quests)
        {
            var quest = Instantiate(_questPrefab,
                _questContainer.transform);
            _sliders.Add(quest);
            quest.Init(q);
            quest.OnQuestUpdated.AddListener((id, val) =>
            {
                for (int i = 0; i < _myData.Quests.Count; i++)
                {
                    if (_myData.Quests[i].Id == id)
                    {
                        _myData.Quests[i].Progress = val;
                        Display(_myData);
                    }
                }
            });
        }
    }

    private void Subscribe()
    {
        CreateQuests();
        _nameField.onEndEdit.AddListener(str =>
        {
            _myData.Name = str;
            Display(_myData);
            _nameField.text = string.Empty;
        });
        _decreaseLevelBtn.onClick.AddListener(() =>
        {
            _myData.Level--;
            Display(_myData);
        });
        _increaseLevelBtn.onClick.AddListener(() =>
        {
            _myData.Level++;
            Display(_myData);
        });
        _tutorialToggle.onValueChanged.AddListener(flag =>
        {
            _myData.IsTutorialCompleted = flag;
            Display(_myData);
        });

        _saveBtn.onClick.AddListener(OnSaveBtnClicked);
        _loadBtn.onClick.AddListener(OnLoadBtnClicked);
    }

    private void Display(SaveData data)
    {
        var builder = new StringBuilder();
        builder.Append("Hello, ");
        builder.Append(data.Name);
        builder.Append("!");
        builder.Append("\n");
        builder.Append("You reached level: ");
        builder.Append(data.Level);
        builder.Append("\n");
        builder.Append(data.IsTutorialCompleted ? 
            "Tutorial is completed" : "You have to complete tutorial");

        foreach (var quest in data.Quests)
        {
            builder.Append("\n");
            builder.Append(quest.Id);
            builder.Append(" progress is: ");
            builder.Append(quest.Progress * 100);
            builder.Append("%");
        }

        _infoLabel.text = builder.ToString();
    }

    private void CorrectUi(SaveData data)
    {
        _tutorialToggle.isOn = data.IsTutorialCompleted;
        _nameField.text = string.Empty;
        foreach (var slr in _sliders)
        {
            foreach (var quest in data.Quests)
            {
                if (slr.Id == quest.Id)
                {
                    slr.UpdateValue(quest.Progress);
                }
            }
        }
    }

    #endregion

    [SerializeField]
    private Button _saveBtn;
    [SerializeField]
    private Button _loadBtn;

    private ISaveSystem _saveSystem;

    private SaveData _myData;

    private void Start()
    {
        _saveSystem = new BinarySaveSystem();
        CreateStartData();
        Subscribe();
        Display(_myData);
        CorrectUi(_myData);
    }

    private void CreateStartData()
    {
        _myData = new SaveData()
        {
            Level = Random.Range(1, 10),
            Name = "Maksym",
            IsTutorialCompleted = false,
            Quests = new List<Quest>()
            {
                new Quest()
                {
                    Id = "Quest1",
                    Progress = 0.3f,
                    QuestsToOpen = new string[]
                    {

                    }
                },
                new Quest()
                {
                    Id = "Quest2",
                    Progress = 0.13f,
                    QuestsToOpen = new string[]
                    {

                    }
                },
                new Quest()
                {
                    Id = "Quest3",
                    Progress = 0.01f,
                    QuestsToOpen = new string[]
                    {
                        "Quest2"
                    }
                },
                new Quest()
                {
                    Id = "Quest4",
                    Progress = 0.0f,
                    QuestsToOpen = new string[]
                    {
                        "Quest3"
                    }
                },
                new Quest()
                {
                    Id = "Quest5",
                    Progress = 0.0f,
                    QuestsToOpen = new string[]
                    {

                    }
                },
                new Quest()
                {
                    Id = "Quest6",
                    Progress = 0.0f,
                    QuestsToOpen = new string[]
                    {
                        "Quest4",
                        "Quest5"
                    }
                }
            }
        };
    }

    private void OnSaveBtnClicked()
    {
        _saveSystem.Save(_myData);
    }

    private void OnLoadBtnClicked()
    {
        _myData = _saveSystem.Load();
        Display(_myData);
        CorrectUi(_myData);
    }
}
