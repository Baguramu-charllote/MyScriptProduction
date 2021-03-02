using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjInfo
{
    public Mesh mesh;
    public Material material;
}

public class DataManager : Singleton<DataManager>
{
    [SerializeField] SceneData SceneD = null;
    [SerializeField] EnemyData EnemyD = null;
    [SerializeField] SkillData skillD = null;
    [SerializeField] ItemData  ItemD  = null;
    public GameObject Pskillget = null;
    public GameObject uiprefab1;
    public ObjInfo wall;

    private void Awake()
    {
    }
    public SceneState SceneValueOut(int no)
    {
        return SceneD.ValueOut(no);
    }
    public SkillStatus SkillValueOut(int no)
    {
        return skillD.ValueOut(no);
    }
    public int SkillCount { get { return skillD.SkillCount; } }
    public int PSkillCount { get { return skillD.PassiveSkillCount; } }
    public PskillStatus PskillValueOut(int no)
    {
        return skillD.PValueOut(no);
    }
    public EnemyStatus EnemyValueOut(int no)
    {
        return EnemyD.ValueOut(no);
    }
    public ItemState ItemValueOut(int no)
    {
        return ItemD.ValueOut(no);
    }
}
