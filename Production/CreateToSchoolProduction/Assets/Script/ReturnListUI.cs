using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ボタンを内包するオブジェクトか判断するため＆
/// ボタンのオブジェクトをまとめて渡せるようにする目的のスクリプト
/// </summary>
public class ReturnListUI : MonoBehaviour
{
    GameObject[] buttons;

    void Awake()
    {
        List<GameObject> list = new List<GameObject>();

        foreach(Transform t in GetComponentInChildren<Transform>())
        {
            if (t.gameObject.GetComponent<Button>())
            {
                list.Add(t.gameObject);
            }
        }
        buttons = list.ToArray();        
    }

    public GameObject[] chidArray
    {
        get { return buttons; }
    }
}
