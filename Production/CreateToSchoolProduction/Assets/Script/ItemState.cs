using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    material,
    heal,
    Attack,
    weapon,
}
[System.Serializable]
public class ItemState
{
    public string name;
    public int no;
    public int power;
    public ItemType type;
    public Sprite sprite;
}
