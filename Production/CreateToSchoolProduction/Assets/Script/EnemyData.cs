using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")]
public class EnemyData : Data<EnemyStatus>
{
    public override EnemyStatus ValueOut(int no)
    {
        for (int i = 0; i < aData.Length; i++)
        {
            if(aData[i].no == no)
            {
                return aData[i];
            } 
        }
        return base.ValueOut(no);
    }
}
