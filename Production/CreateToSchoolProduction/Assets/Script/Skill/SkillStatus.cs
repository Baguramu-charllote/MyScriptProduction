using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillStatus
{
    public string name = "";
    public int cost = 0;
    public EffectType Etype;
    public Sprite sprite;
    public enum EffectType
    {
        Attack,     // 物理
        Throw,      // 遠隔
    }
}
[System.Serializable]
public class PskillStatus
{
    public string name = "";
    public int cost = 0;
    public EffectType effect;
    public int Power;
    public Sprite sprite;
    public enum EffectType {
        Atk,
        Agi,
        Def,
        Cos,
    }
}
