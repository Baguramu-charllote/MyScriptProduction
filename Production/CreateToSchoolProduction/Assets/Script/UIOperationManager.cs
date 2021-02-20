using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

/// <summary>
/// このScriptはOption等の操作があるUIを動かすためのScript。
/// UIsの中には表示させるすべてのUIをいれ使う。
/// UIsの最初はmenuに使われているUIを入れておくこと。
/// </summary>
public class UIOperationManager:MonoBehaviour
{
    GameObject[] UIs;       // 配列1個目は全体のObject
    GameObject[] Buttons;   // Menuのボタンを集める

    int cnt = 1;            // 選択されているButton
    int selectcnt = 0;      // 現在のButtonの数

    bool isOpenUI = false;
    /// <summary>
    /// 選択されているUIの種別
    /// </summary>
    enum UIStatus
    {
        Menu,
        Status,
        Skill,
        Inventory,
        Quite,
    }

    /// <summary>
    /// 生成時UI情報を受け取り記録する
    /// </summary>
    /// <param name="objs"> 保存するobject</param>
    public UIOperationManager(GameObject canvas,GameObject[] objs)
    {
        UIs = objs;
        Transform parent = UIs[0].transform;
        bool[] a = UIs.Select(n => { n.transform.parent = parent; Debug.Log(n.transform.parent); return true; }).ToArray();
        Buttons = SpecifyGetChild("Button", UIs[0].transform.Find("select").gameObject);
        selectcnt = Buttons.Length;
        OpenUI();
    }
    
    /// <summary>
    /// 一括されているObjectから必要なオブジェクトを名前をKeywordと見比べて取得している
    /// 今回はボタンを取ってきている
    /// </summary>
    /// <param name="word"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    GameObject[] SpecifyGetChild(string word,GameObject obj)
    {
        List<GameObject> objList = new List<GameObject>();
        foreach (Transform t in obj.GetComponentInChildren<Transform>())
        {
            if(t.name.Length < word.Length)
            {
                continue;
            }
            else
            {
                int wordLength = word.Length; 
                for(int i = 0; i < word.Length; i++)
                {
                    if(t.name[i] == word[i])
                    {
                        wordLength--;
                    }
                    else
                    {
                        break;
                    }
                }
                if(wordLength == 0)
                {
                    objList.Add(t.gameObject);
                }
            }
        }
        return objList.ToArray();
    }

    /// <summary>
    /// 選択肢を切り替える
    /// </summary>
    /// <param name="up"></param>
    public void SelectUI(bool up)
    {
        if (isOpenUI)
        {
            Buttons[cnt - 1].GetComponent<Image>().color = Color.white;
            if (!up)
            {
                if (selectcnt >= cnt + 1)
                { cnt++; }
                else { cnt = 1; }
            }
            else
            {
                if (0 < cnt - 1)
                { cnt--; }
                else { cnt = selectcnt; }
            }
            Buttons[cnt - 1].GetComponent<Image>().color = Color.grey;
            DeploymentOptionUI();
        }
    }
    
    /// <summary>
    /// 選択しているUIを利用できる状態にする
    /// </summary>
    public void DecisionUI()
    {
        if (UIs[cnt].transform.Find("select").childCount > 0)
        {
            Buttons = SpecifyGetChild("Button", UIs[cnt].transform.Find("select").gameObject);
        }


    }

    /// <summary>
    /// UIを開く閉じるだけ
    /// </summary>
    public void OpenUI()
    {
        UIs[(int)UIStatus.Menu].SetActive(!UIs[(int)UIStatus.Menu].activeSelf);
        UIs[(int)UIStatus.Status].SetActive(UIs[(int)UIStatus.Menu].activeSelf);
        Buttons[cnt - 1].GetComponent<Image>().color = Color.grey;
        
        if (UIs[(int)UIStatus.Menu].activeSelf)
        {
            Buttons = SpecifyGetChild("Button", UIs[(int)UIStatus.Menu].transform.Find("select").gameObject);
            selectcnt = Buttons.Length;
        }
        else
        {
            bool[] a = Buttons.Select(n =>
            { n.GetComponent<Image>().color = Color.white; return true; }
            ).ToArray();
        }

        foreach (GameObject a in UIs.Select(n => n != UIs[(int)UIStatus.Menu] || n != UIs[(int)UIStatus.Status] ? null : n))
        {
            if (a != null)
            {
                if (a.activeSelf)
                {
                    a.SetActive(false);
                }
            }
        }

        isOpenUI = UIs[(int)UIStatus.Menu].activeSelf;
        Debug.Log(isOpenUI);
    }

    /// <summary>
    /// 今の選択されているステータス(cnt)をみて開く
    /// </summary>
    public void DeploymentOptionUI()
    {
        if (isOpenUI)
        {
            foreach (GameObject a in UIs.Select(n => n != UIs.First() ? n : null))
            {
                if (a != null)
                {
                    if (a.activeSelf)
                    {
                        a.SetActive(false);
                    }
                }
            }
            GameObject ui = UIs[cnt];
            ui.SetActive(true);
        }
    }
}
