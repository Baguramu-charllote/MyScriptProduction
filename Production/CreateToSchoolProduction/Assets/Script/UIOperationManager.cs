using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Reflection;

/// <summary>
/// このScriptはOption等の操作があるUIを動かすためのScript。
/// UIsの中には表示させるすべてのUIをいれ使う。
/// UIsの最初はmenuに使われているUIを入れておくこと。
/// </summary>
public class UIOperationManager
{
    GameObject[] UIs;       // 配列1個目は全体のObject
    GameObject[] Buttons;   // Menuのボタンを集める

    int cnt = 1;            // 選択されているButton
    int selectcnt = 0;      // 現在のButtonの数

    UIStatus now = UIStatus.Menu;
    GameObject After = null;

    public bool isOpenUI = false;  // UIが開いているか
    /// <summary>
    /// 選択されているUIの種別
    /// </summary>
    enum UIStatus
    {
        Menu,
        Status,
        Skill,
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
        bool[] a = UIs.Select(n => { n.transform.parent = parent; return true; }).ToArray();    
        // メニューのselectを追加する
        Buttons = SpecifyGetChild("Button", UIs[0].transform.Find("select").gameObject);
        // ステータスの記入
        {
            inputState();
        }
        // スキルの表示をできるようにする
        {
            GameObject[] cartridge = SpecifyGetChild("Cartridge", UIs[2].transform.Find("List").gameObject);
            for (int i = 0; i < DataManager.instance.SkillCount; i++)
            {
                cartridge[i].GetComponent<Image>().sprite = DataManager.instance.SkillValueOut(i).sprite;
            }
            for (int i = DataManager.instance.SkillCount; i < DataManager.instance.SkillCount + DataManager.instance.PSkillCount; i++)
            {
                cartridge[i].GetComponent<Image>().sprite = DataManager.instance.PskillValueOut(i).sprite;
            }
            Debug.Log(DataManager.instance.SkillCount.ToString() + ':' +DataManager.instance.PSkillCount.ToString());
        }
        selectcnt = Buttons.Length;
        OpenUI();
    }
    
    /// <summary>
    /// stateをUIに反映させる
    /// </summary>
    void inputState()
    {
        StatusValue state = GameManager.instance.s.status;
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < UIs[1].transform.Find("State").childCount; i++)
        {
            list.Add(UIs[1].transform.Find("State").GetChild(i).gameObject);
        }
        Dictionary<string, int> pairs = new Dictionary<string, int>();
        int hp = state.MaxHp;
        FieldInfo[] info = state.GetType().GetFields();
        foreach (FieldInfo i in info)
        {
            Type t = i.GetValue(state).GetType();
            if (t.IsValueType || t == typeof(String)) // 値型か
            {
                foreach (GameObject obj in list)
                {
                    if (i.Name == obj.name) // 入力場所とfield名が同じか
                    {
                        if (i.Name == "Hp")
                        {
                            obj.transform.GetChild(0).GetComponent<Text>().text = ": " + hp.ToString() + '/' + i.GetValue(state).ToString();
                        }
                        else
                        {
                            obj.transform.GetChild(0).GetComponent<Text>().text = ": " + i.GetValue(state).ToString();
                        }
                    }
                }
            }
        }
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
                {
                    cnt--;
                }
                else
                {
                    if (now == UIStatus.Menu)
                    {
                        cnt = selectcnt;
                    }
                }
            }
            Buttons[cnt - 1].GetComponent<Image>().color = Color.gray;
            if (now == UIStatus.Menu)
            {
                DeploymentOptionUI();
            }
        }
    }
    
    /// <summary>
    /// 選択しているUIを利用できる状態にする
    /// </summary>
    public void DecisionUI()
    {
        if (now == UIStatus.Menu)
        {
            if ((UIStatus)cnt == UIStatus.Skill)
            {
                if (UIs[cnt].transform.Find("select"))
                {
                    if (UIs[cnt].transform.Find("select").childCount > 0)
                    {
                        Buttons = SpecifyGetChild("Button", UIs[cnt].transform.Find("select").gameObject);
                    }
                }
            }
            else if ((UIStatus)cnt == UIStatus.Quite)
            {
                Buttons = SpecifyGetChild("Button", UIs[cnt].transform.Find("select").gameObject);
                cnt = 1;
                selectcnt = Buttons.Length;
                Buttons[cnt - 1].GetComponent<Image>().color = Color.gray;
            }
            now = (UIStatus)cnt;
            Debug.Log("now:" + now + "\nbutton数:" + selectcnt);
        }
    }

    /// <summary>
    /// UIを開く閉じるだけ
    /// </summary>
    public void OpenUI()
    {
        if (UIs[(int)UIStatus.Menu].activeSelf) // メニューが開いているとき(閉じるとき)
        {
            foreach(GameObject g in UIs)
            {
                g.SetActive(false);
            }
            Buttons = SpecifyGetChild("Button", UIs[(int)UIStatus.Menu].transform.Find("select").gameObject); // ボタンの初期化
            foreach(GameObject g in Buttons)
            {
                g.GetComponent<Image>().color = Color.white;
            }
            cnt = 1;
            selectcnt = Buttons.Length;
        }
        else // メニューが閉じているとき(開くとき)
        {
            UIs[(int)UIStatus.Menu].SetActive(true);
            UIs[(int)UIStatus.Status].SetActive(true);
            Buttons[0].GetComponent<Image>().color = Color.grey;
        }
        isOpenUI = UIs[(int)UIStatus.Menu].activeSelf;        
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
