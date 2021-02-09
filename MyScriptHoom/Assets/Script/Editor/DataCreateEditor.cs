using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataCreateEditor : EditorWindow
{
    static Data data = null;
    static Values value;
    string path;

    [MenuItem("Window/My Window/CreateItem")]
    static void Init()
    {
        DataCreateEditor window = (DataCreateEditor)EditorWindow.GetWindow(typeof(DataCreateEditor));
        data = AssetDatabase.LoadAssetAtPath<Data>("Assets/Data.asset");
        value = new Values();
        window.Show();
    }

    private void OnGUI()
    {
        if(data == null)
        {
            GUILayout.Label("NonAssetsData");
        }
        value.name = GUILayout.TextField(value.name);
        value.no = EditorGUILayout.IntField("ナンバー",value.no);
        if (GUILayout.Button("Create"))
        {
            data.Save(value);
            EditorUtility.SetDirty(data);
            value = new Values();
        }
    }
}
