using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SceneData")]
public class SceneData : Data<SceneState>
{
    public GameObject[] wall;
    public override void Save()
    {
        base.Save();
    }
    public override SceneState ValueOut(int no)
    {
        for(int i = 0; i < aData.Length; i++)
        {
            if(aData[i].no == no)
            {
                return aData[i];
            }
        }
        return base.ValueOut(no);
    }
    public GameObject WallValueOut(int no)
    {
        if (wall.Length > no)
        {
            return wall[no];
        }
        return (GameObject)Resources.Load("wall");
    }
}
