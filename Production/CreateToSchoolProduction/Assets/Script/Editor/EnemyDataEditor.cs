using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(EnemyData))]
public class EnemyDataEditor :Editor
{
    static EnemyData data;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        data = (EnemyData)target;
        if (data != null)
        {
            if (GUILayout.Button("Save"))
            {
                EditorUtility.SetDirty(data);
            }
            if (GUILayout.Button("Update"))
            {
                EditorUtility.SetDirty(data);
            }
        }
    }
}
