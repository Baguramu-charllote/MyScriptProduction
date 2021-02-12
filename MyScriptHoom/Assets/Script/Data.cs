using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 編集者：高木　基稔
/// 内容
/// scriptableobject化したいもの簡潔に使えるようにする。
/// testとしてscriptableobjectを継承していても、継承先になれるかテストする。
/// ScriptableObjectはジェネリックが使えない
/// </summary>

[CreateAssetMenu(menuName = "Data/ItemData")]
public class Data : ScriptableObject
{
    [SerializeField] GameObject objects = null;
    public Values[] Itemdata;

    public virtual void Save()
    {
        Itemdata = new Values[objects.transform.childCount];
        GameObject a = null;
        DataValue d = null;
        for (int i = 0;i<objects.transform.childCount;i++)
        {
            a = objects.transform.GetChild(i).gameObject;
            d = a.GetComponent<DataValue>();
            Values t = d.val;
            Itemdata[i] = t;
        }
    }

    public virtual void Save(Values d)
    {
        List<Values> list = new List<Values>(Itemdata);
        list.Add(d);
        Itemdata = list.ToArray();
    }
}
