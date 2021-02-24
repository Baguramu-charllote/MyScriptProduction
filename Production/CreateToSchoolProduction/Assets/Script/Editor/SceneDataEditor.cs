using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SceneData))]
public class SceneDataEditor : Editor
{
    static SceneData data;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        data = (SceneData)target;
        if (data != null)
        {
            if (GUILayout.Button("Save"))
            {
                EditorUtility.SetDirty(data);
            }
            if (GUILayout.Button("Update"))
            {
                EditorUtility.SetDirty(data);
                Debug.Log(data.aData[0].WallEntry[0].SpornPos.ToString());
            }
        }
    }
}
