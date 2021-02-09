using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class InputManagerGenerator : MonoBehaviour
{
    //public Action<Vector3> action;
    Player CodePlayer;
    public string[] ButtonNames;
    [NamedArrayAttribute(new string[] {"取得","攻撃１", "攻撃２", "ジャンプ", "ダッシュ","インベントリ","終了"})]
    public KeyCode[] keyCodes;
    public Dictionary<string, KeyCode> Buttons;

    void Start()
    {
        //action = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBaseController>().GSvec;
        CodePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        InputKeyBord();
    }

    void InputKeyBord()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        bool y = Input.GetKeyDown(KeyCode.Space);

        if (Vector2.SqrMagnitude(new Vector2(x, z)) > 0.3f)
        {
            //action(new Vector3(x, 0, z));
            CodePlayer.GSvec(new Vector3(x, 0, z));
        }

        if (Input.GetKeyDown(keyCodes[0]))
        {
            CodePlayer.GetItem();
        }

        if (Input.GetKeyDown(keyCodes[1]))
        {
            SaveManager sm = new SaveManager();
            sm.Writetext("Data");
        }

        if (Input.GetKeyDown(keyCodes[5]))
        {
            GameManager.instance.ISInventory();
        }

        if (Input.GetKeyDown(keyCodes[6]))
        {
            GameManager.instance.Quit();
        }
    }
}
