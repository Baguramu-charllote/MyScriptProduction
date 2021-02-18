using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム中のアイテムに直接貼るclass
public class ItemState : MonoBehaviour
{
    public int itemNo = 0;
    public int Count = 0;
    public Vector3 pos = Vector3.zero;
    public Values value;


    public ItemState()
    {
        value = new Values();
    }

    /// <summary>
    /// 自分に対応するDataをvalueに入れる
    /// </summary>
    public void InValues()
    {
        value = DataManager.instance.ReturnValue(this);
    }
}
