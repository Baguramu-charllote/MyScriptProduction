using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SkillData")]
public class SkillData : Data<SkillStatus>
{
    public PskillStatus[] pData;
    public override void Save()
    {
        base.Save();
    }
    public override SkillStatus ValueOut(int no)
    {
        for (int i = 0; i < aData.Length; i++)
        {
            if (i == no)
            {
                return aData[i];
            }
        }
        return base.ValueOut(no);
    }
    public PskillStatus PValueOut(int no)
    {
        for(int i = 0; i < pData.Length; i++)
        {
            if(i == no)
            {
                return pData[i];
            }
        }
        return pData[0];
    }
}
