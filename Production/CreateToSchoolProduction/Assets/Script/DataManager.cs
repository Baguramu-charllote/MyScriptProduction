using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] SceneData SceneD = null;
    [SerializeField] EnemyData EnemyD = null;
    [SerializeField] SkillData skillD = null;
    [SerializeField] ItemData  ItemD  = null;

    public SceneState SceneValueOut(int no)
    {
        return SceneD.ValueOut(no);
    }
    public SkillStatus SkillValueOut(int no)
    {
        return skillD.ValueOut(no);
    }
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
