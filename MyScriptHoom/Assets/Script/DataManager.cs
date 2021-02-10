using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] Data data = null;
     
    /// <summary>
    /// ItemStateから当てはまるValuesデータを探して返す
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public Values ReturnValue(ItemState item)
    {
        Values itemValue = new Values();
        foreach(Values v in data.data)
        {
            if(v.no == item.itemNo)
            {
                itemValue = v;
            }
        }
        return itemValue;
    }
}
