using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ItemData")]
public class ItemData : Data<ItemState>
{
    public override void Save()
    {
        base.Save();
    }
    public override ItemState ValueOut(int no)
    {
        for (int i = 0; i < aData.Length; i++)
        {
            if (aData[i].no == no)
            {
                return aData[i];
            }
        }
        return base.ValueOut(no);
    }
}
