using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SceneData
{
    public List<Item> Items;
    public SaveInfo Info;
}

[Serializable]
public class SaveInfo
{
    public string Id;
    public byte[] Icon;
}

[Serializable]
public class Item
{
    public string Id;
    public Vector3 Position;
    public Quaternion Rotation;
}
