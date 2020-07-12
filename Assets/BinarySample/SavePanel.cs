using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
{
    [SerializeField]
    private Button _saveBtn;
    [SerializeField]
    private Button _loadBtn;
    [SerializeField]
    private Button _deleteAllBtn;
    [SerializeField]
    private SaveItem _itemPrefab;
    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private Transform _container;

    public event Action SaveRequested;
    public event Action<string> LoadRequested;

    private ISaver _saver;
    private List<SaveItem> _items = new List<SaveItem>();

    private void Awake()
    {
        _saveBtn.onClick.AddListener(OnSaveBtnClicked);
        _loadBtn.onClick.AddListener(OnLoadBtnClicked);
        _deleteAllBtn.onClick.AddListener(OnDeleteAllBtnClicked);
        _panel.gameObject.SetActive(false);
    }

    public void SetSaver(ISaver saver)
    {
        _saver = saver;
        foreach (var save in _saver.GetAll)
        {
            var sceneData = _saver.Load(save);
            Add(sceneData.Info);
        }
    }

    private void OnSaveBtnClicked()
    {
        SaveRequested?.Invoke();
    }

    public void Add(SaveInfo save)
    {
        var item = Instantiate(_itemPrefab, _container);
        item.gameObject.SetActive(true);
        Texture2D texture = new Texture2D(Screenshot.Width, Screenshot.Height);
        texture.LoadImage(save.Icon);
        texture.Apply();
        item.Init(save.Id, texture);
        _items.Add(item);
        item.Selected += OnItemSelected;
        item.Deleted += OnItemDeleted;
    }

    private void OnLoadBtnClicked()
    {
        _panel.gameObject.SetActive(true);
    }

    private void OnItemSelected(SaveItem item)
    {
        LoadRequested?.Invoke(item.Id);
        _panel.gameObject.SetActive(false);
    }

    private void OnDeleteAllBtnClicked()
    {
        foreach (var item in _items)
        {
            DeleteItem(item, false);
        }
        _items.Clear();
    }

    private void OnItemDeleted(SaveItem item)
    {
        DeleteItem(item, true);
    }

    private void DeleteItem(SaveItem item, bool removeFromList)
    {
        item.Deleted -= OnItemDeleted;
        item.Selected -= OnItemSelected;
        if (removeFromList)
        {
            _items.Remove(item);
        }
        item.FreeResources();
        Destroy(item.gameObject);
        _saver.DeleteSave(item.Id);
    }
}
