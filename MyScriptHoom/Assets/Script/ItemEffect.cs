using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect
{
    public void UseEffect(ItemState item,GameObject accept)
    {
        switch (item.val.itm_type)
        {
            case ItemType.Hl:
                Hl(accept,item.val.no);
                break;
            case ItemType.Dmg:
                break;
            case ItemType.Bf:
                break;
            case ItemType.Dbf:
                break;
            default:
                break;
        }
    }
    public void Hl(GameObject g,int e)
    {
        g.GetComponent<ObjState>().AddHp = e;
    }
    public void Dmg (GameObject g,int e)
    {
        g.GetComponent<ObjState>().Damage = e;
    }
}
