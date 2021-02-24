using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SkillData))]
public class SkillDataEditor : Editor
{
    static SkillData data;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        data = (SkillData)target;
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
