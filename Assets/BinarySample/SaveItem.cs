using System;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaveItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Text _name;
    [SerializeField]
    private Button _deleteBtn;
    [SerializeField]
    private RawImage _icon;

    public event Action<SaveItem> Selected;
    public event Action<SaveItem> Deleted;

    public string Id { get; private set; }

    private void Awake()
    {
        _deleteBtn.onClick.AddListener(OnDeleteBtnPressed);
    }

    public void Init(string id, Texture2D icon)
    {
        Id = id;
        _name.text = Path.GetFileNameWithoutExtension(id);
        _icon.texture = icon;
    }

    public void FreeResources()
    {
        if (_icon.texture != null)
        {
            Destroy(_icon.texture);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Selected?.Invoke(this);
    }

    private void OnDeleteBtnPressed()
    {
        Deleted?.Invoke(this);
    }
}
