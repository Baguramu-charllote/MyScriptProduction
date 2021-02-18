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

public class Data<T> : ScriptableObject
{
    public T[] aData;
    /// <summary>
    /// 新規データ保存用
    /// </summary>
    public virtual void Save()
    {

    }
    
    public virtual T ValueOut(int no)
    {
        return aData[0];      
    }
}
