using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IFdata
{
    void Save();
}

[CreateAssetMenu(menuName = "Data/test")]
public class TestData : ScriptableObject, IFdata
{
    public void Save()
    {
        throw new System.NotImplementedException();
    }
}
