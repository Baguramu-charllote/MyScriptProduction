using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SceneState
{
    public int no;
    public SceneDetail[] EntryEnemy;
    public SceneDetail[] WallEntry;
}

[System.Serializable]
public class SceneDetail
{
    public int id;
    public Vector3 SpornPos;
    public Vector3 Scale = Vector3.one;
}
