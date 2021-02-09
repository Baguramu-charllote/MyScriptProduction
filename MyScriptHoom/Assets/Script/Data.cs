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
    public Values[] data;

    public virtual void Save()
    {
        data = new Values[objects.transform.childCount];
        GameObject a = null;
        DataValue d = null;
        for (int i = 0;i<objects.transform.childCount;i++)
        {
            a = objects.transform.GetChild(i).gameObject;
            d = a.GetComponent<DataValue>();
            Values t = d.val;
            //DestroyImmediate(d,true);
            //t.obj = a;
            data[i] = t;
        }
    }

    public virtual void Save(Values d)
    {
        List<Values> list = new List<Values>(data);
        list.Add(d);
        data = list.ToArray();
    }
}
