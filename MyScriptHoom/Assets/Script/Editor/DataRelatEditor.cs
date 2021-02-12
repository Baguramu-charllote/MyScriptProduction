using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Data))]
public class DataRelatEditor : Editor
{
    static Data data;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        data = (Data)target;
        if (data != null)
        {
            if (GUILayout.Button("Save"))
            {
                data.Save();
                EditorUtility.SetDirty(data);
            }
            if (GUILayout.Button("Update"))
            {
                EditorUtility.SetDirty(data);
            }
        }
    }
}
