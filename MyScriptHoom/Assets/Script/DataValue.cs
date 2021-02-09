using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data(ScriptableObject)の中に保存する最低限のデータ
/// </summary>
[System.Serializable]
public struct Values
{    
    public string name;
    public int no;
    public int Eff;
    public ItemType itm_type;
    public Sprite sprite;
    public Mesh mesh;
    public Material material;
}

public enum ItemType
{
    Hl,
    Dmg,
    Bf,
    Dbf,
}

public class DataValue :MonoBehaviour
{
    public Values val;

    public Values SetValue()
    {
        Values a = new Values();
        return a;
    }
}
