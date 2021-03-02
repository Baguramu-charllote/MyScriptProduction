using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ReUIMnager : Singleton<ReUIMnager>
{
    [Header("使用するUIオブジェクト")]
    [SerializeField] GameObject UI = null;          // ゲーム上にあるUI

    [Header("遷移パラメータカラー")]
    [NamedArray(new string[] { "Normal","Select","NonSelect","OverCost"})]
    [SerializeField] Color[] colors = new Color[4]; // ボタンのカラーの変更カラー

    public bool isOpenUI { get; private set; }

    public GameObject[] UIs { get; private set; }   // UIを管理する
    public UIStatus now;           // 現在の開いているUI
    UIStatus previous;      // 前まで開いていたUI

    GameObject[] Buttons;   // 現在選択可能なボタン配列
    int cnt = 1;            // 現在選択されているボタン(数)
    int MaxEnableCnt = 0;   // 
    int MinEnableCnt = 0;   // 
    int enableCnt = 0;      // 選択可能ボタン数

    // Menuの状態
    public enum UIStatus
    {
        Menu,
        Status,
        Skill,
        Quite,
        Skillselect,
    }

    // ボタンのカラー選択
    enum UIColor
    {
        Normal,
        Select,
        NonSelect,
        OverCost
    }

    // 使用優先度順に登録する
    string[] objName = new string[]
    {
        "select","list","Button","Cartridge"
    };

    void Awake()
    {
        Init();
    }  
    
    // 面倒なところを一括でするメソッド
    #region Convenient
    /// <summary>
    /// objから子の配列を取り出す
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    GameObject[] GetChildObject(GameObject obj)
    {
        List<GameObject> list = new List<GameObject>();
        for(int i = 0; i < obj.transform.childCount; i++)
        {
            list.Add(obj.transform.GetChild(i).gameObject);
        }
        return list.ToArray();
    }

    /// <summary>
    /// 名前を指定して子の配列を取り出す
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    GameObject[] GetChildObject(GameObject obj,string name)
    {
        List<GameObject> list = new List<GameObject>();
        foreach (Transform t in obj.GetComponentInChildren<Transform>())
        {
            if (t.name.Length < name.Length)
            {
                continue;
            }
            else
            {
                int wordLength = name.Length;
                for (int i = 0; i < name.Length; i++)
                {
                    if (t.name[i] == name[i])
                    {
                        wordLength--;
                    }
                    else
                    {
                        break;
                    }
                }
                if (wordLength == 0)
                {
                    list.Add(t.gameObject);
                }
            }
        }
        return list.ToArray();        
    }

    /// <summary>
    /// objsの親を設定する
    /// </summary>
    /// <param name="objs"></param>
    /// <param name="parent"></param>
    void ToParent(GameObject[] objs,GameObject parent)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].transform.parent = parent.transform;
        }
    }
    #endregion

    //　ほかにかまわすことがないメソッド
    #region Private
    void Init()
    {
        // UIsの代入
        if (UI == null)
        {
            UI = GameObject.FindGameObjectWithTag("UIParent");
            UIs = GetChildObject(UI);
        }
        else
        {
            UIs = GetChildObject(UI);
        }
        ToParent(UIs.Where(n => n != UIs[(int)UIStatus.Menu]).ToArray(), UIs[(int)UIStatus.Menu]);
        UIs[(int)UIStatus.Menu].SetActive(false);

        Buttons = GetChildObject(UIs[(int)UIStatus.Menu].transform.Find(objName[0]).gameObject, objName[2]);
        enableCnt = Buttons.Length;

        GameObject[] cartridge = GetChildObject(UIs[(int)UIStatus.Skill].transform.Find(objName[1]).gameObject, objName[3]);
        for (int i = 0; i < DataManager.instance.SkillCount; i++)
        {
            cartridge[i].GetComponent<Image>().sprite = DataManager.instance.SkillValueOut(i).sprite;
        }
        for (int i = DataManager.instance.SkillCount; i < DataManager.instance.SkillCount + DataManager.instance.PSkillCount; i++)
        {
            cartridge[i].GetComponent<Image>().sprite = DataManager.instance.PskillValueOut(i).sprite;
        }

        isOpenUI = false;
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
                        Debug.Log(i.Name);
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
    #endregion


    #region PublicMethod

    /// <summary>
    /// 選択肢を切り替える
    /// </summary>
    /// <param name="up"></param>
    public void SelectUI(bool up)
    {
        if (isOpenUI)
        {
            Buttons[cnt - 1].GetComponent<Image>().color = colors[(int)UIColor.Normal];
            if (!up)
            {
                if (enableCnt >= cnt + 1)
                { 
                    cnt++;
                }
                else
                {
                    cnt = 1; 
                }
            }
            else
            {
                if (0 < cnt - 1)
                {
                    cnt--;
                }
                else
                {
                    cnt = enableCnt;
                }
            }
            Buttons[cnt - 1].GetComponent<Image>().color = colors[(int)UIColor.Select];
            if (now == UIStatus.Menu)
            {
                DeploymentOptionUI();
            }
            Debug.Log("cnt:" + cnt.ToString() + "\nEnableCnt:" + enableCnt.ToString());
        }
    }

    /// <summary>
    /// UIを開くまたは閉じるだけ
    /// </summary>
    public void OpenUI()
    {
        if (UIs[(int)UIStatus.Menu].activeSelf) // メニューが開いているとき(閉じるとき)
        {
            foreach (GameObject g in UIs)
            {
                g.SetActive(false);
            }
            Buttons = GetChildObject(UIs[(int)UIStatus.Menu].transform.Find(objName[0]).gameObject , objName[2]); // ボタンの初期化
            foreach (GameObject g in Buttons)
            {
                g.GetComponent<Image>().color = Color.white;
            }
            cnt = 1;
            enableCnt = Buttons.Length;
        }
        else // メニューが閉じているとき(開くとき)
        {
            UIs[(int)UIStatus.Menu].SetActive(true);
            UIs[(int)UIStatus.Status].SetActive(true);
            Buttons[0].GetComponent<Image>().color = colors[(int)UIColor.Select];
            now = UIStatus.Menu;
        }
        inputState();
        isOpenUI = UIs[(int)UIStatus.Menu].activeSelf;
    }

    /// <summary>
    /// UIをMenuまで戻す
    /// </summary>
    public void ReturnMenu()
    {
        if (now != UIStatus.Menu)
        {
            Buttons[cnt-1].GetComponent<Image>().color = colors[(int)UIColor.Normal];
            Buttons = GetChildObject(UIs[(int)UIStatus.Menu].transform.Find(objName[0]).gameObject, objName[2]);
            cnt = (int)now;
            Buttons[(int)now -1].GetComponent<Image>().color = colors[(int)UIColor.Select];
            enableCnt = Buttons.Length;
            previous = now;
            now = UIStatus.Menu;
        }
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

    /// <summary>
    /// 開いているUIに介入できるようにする
    /// </summary>
    /// <param name="nS">next status</param>
    public void DecisionUI(UIStatus nS = UIStatus.Menu)
    {
        if (now == UIStatus.Menu)
        {
            if (nS == UIStatus.Menu && cnt == (int)UIStatus.Menu) return;
            else if (nS == UIStatus.Menu)
            {
                switch ((UIStatus)cnt)
                {
                    case UIStatus.Status:

                        Buttons[cnt - 1].GetComponent<Image>().color = colors[(int)UIColor.Normal];
                        if (UIs[(int)UIStatus.Status].transform.Find(objName[0]).childCount > 0)
                        {
                            Buttons = GetChildObject(UIs[(int)UIStatus.Status].transform.Find(objName[0]).gameObject, objName[2]);
                            now = (UIStatus)cnt;
                        }

                        break;
                    case UIStatus.Skill:

                        Buttons[cnt - 1].GetComponent<Image>().color = colors[(int)UIColor.Normal];
                        if (UIs[(int)UIStatus.Skill].transform.Find(objName[0]).childCount > 0)
                        {
                            Buttons = GetChildObject(UIs[(int)UIStatus.Skill].transform.Find(objName[0]).gameObject, objName[3]);
                            now = (UIStatus)cnt;
                        }

                        break;
                    case UIStatus.Quite:

                        Buttons[cnt - 1].GetComponent<Image>().color = colors[(int)UIColor.Normal];
                        if (UIs[(int)UIStatus.Quite].transform.Find(objName[0]).childCount > 0)
                        {
                            Buttons = GetChildObject(UIs[(int)UIStatus.Quite].transform.Find(objName[0]).gameObject, objName[2]);
                            now = (UIStatus)cnt;
                        }

                        break;
                    default:
                        ReturnMenu();
                        break;
                }
                previous = UIStatus.Menu;
            }
            else
            {
                switch (nS)
                {
                    case UIStatus.Status:

                        Buttons[(int)nS].GetComponent<Image>().color = colors[(int)UIColor.Normal];
                        if (UIs[(int)UIStatus.Status].transform.Find(objName[0]).childCount > 0)
                        {
                            Buttons = GetChildObject(UIs[(int)UIStatus.Status].transform.Find(objName[0]).gameObject, objName[2]);
                            now = nS;
                        }

                        break;
                    case UIStatus.Skill:

                        Buttons[(int)nS].GetComponent<Image>().color = colors[(int)UIColor.Normal];
                        if (UIs[(int)UIStatus.Skill].transform.Find(objName[0]).childCount > 0)
                        {
                            Buttons = GetChildObject(UIs[(int)UIStatus.Skill].transform.Find(objName[0]).gameObject, objName[3]);
                            now = nS;
                        }

                        break;
                    case UIStatus.Quite:

                        Buttons[(int)nS].GetComponent<Image>().color = colors[(int)UIColor.Normal];
                        if (UIs[(int)UIStatus.Quite].transform.Find(objName[0]).childCount > 0)
                        {
                            Buttons = GetChildObject(UIs[(int)UIStatus.Quite].transform.Find(objName[0]).gameObject, objName[2]);
                            now = nS;
                        }

                        break;
                    default:
                        ReturnMenu();
                        break;
                }
            }
            previous = UIStatus.Menu;
            enableCnt = Buttons.Length;
            cnt = 1;
            Buttons[cnt - 1].GetComponent<Image>().color = colors[(int)UIColor.Select];
            Debug.Log(cnt.ToString() + "    "+enableCnt.ToString());
        }
        else if (now == UIStatus.Skill)
        {
            Buttons = GetChildObject(UIs[(int)UIStatus.Skill].transform.Find(objName[1]).gameObject, objName[3]);
            previous = now;
            now = UIStatus.Skillselect;
            enableCnt = Buttons.Length;
            cnt = 1;
            Buttons[cnt - 1].GetComponent<Image>().color = colors[(int)UIColor.Select];
        }
        else if(now == UIStatus.Quite)
        {
            if(cnt == 1)
            {
                GameManager.instance.Quit();
            }
            else if(cnt == 2)
            {
                ReturnMenu();
            }
        }
        else if(now == UIStatus.Skillselect)
        {
            Buttons[cnt - 1].GetComponent<Image>().color = colors[(int)UIColor.Normal];
            Buttons = GetChildObject(UIs[(int)UIStatus.Skill].transform.Find(objName[0]).gameObject, objName[3]);
            enableCnt = Buttons.Length;
            for(int i = 0;i < Buttons.Length; i++)
            {
                if(Buttons[i].GetComponent<Image>().color == colors[(int)UIColor.Select]){ cnt = i + 1; }
            }
            now = UIStatus.Skill;
        }
    }

    public void InputKeyReceiver(KeyCode code)
    {
        switch (now)
        {
            case UIStatus.Menu:
                if (code == KeyCode.Space)          { OpenUI(); }
                else if (code == KeyCode.LeftArrow) { }
                else if (code == KeyCode.RightArrow){ DecisionUI(); }
                else if (code == KeyCode.Return)    { DecisionUI(); }
                else if (code == KeyCode.UpArrow)   { SelectUI(true); }
                else if (code == KeyCode.DownArrow) { SelectUI(false); }
                else { }
                break;
            case UIStatus.Status:
                if (code == KeyCode.Space)          { OpenUI(); }
                else if (code == KeyCode.LeftArrow) { ReturnMenu(); }
                else if (code == KeyCode.RightArrow){ }
                else if (code == KeyCode.Return)    { }
                else if (code == KeyCode.UpArrow)   { }
                else if (code == KeyCode.DownArrow) { }
                else { }
                break;
            case UIStatus.Skill:
                if (code == KeyCode.Space)          { OpenUI(); }
                else if (code == KeyCode.LeftArrow) { SelectUI(true);}
                else if (code == KeyCode.RightArrow){ SelectUI(false);}
                else if (code == KeyCode.Return)    { DecisionUI(); }
                else if (code == KeyCode.UpArrow)   {  }
                else if (code == KeyCode.DownArrow) { DecisionUI(); }
                else { }
                break;
            case UIStatus.Quite:
                if (code == KeyCode.Space)          { OpenUI(); }
                else if (code == KeyCode.LeftArrow) { ReturnMenu();}
                else if (code == KeyCode.RightArrow){  }
                else if (code == KeyCode.Return)    { DecisionUI(); }
                else if (code == KeyCode.UpArrow)   { SelectUI(true); }
                else if (code == KeyCode.DownArrow) { SelectUI(false); }
                else { }
                break;
            case UIStatus.Skillselect:
                if (code == KeyCode.Space)          { OpenUI(); }
                else if (code == KeyCode.LeftArrow) { ReturnMenu(); }
                else if (code == KeyCode.RightArrow){ DecisionUI(); }
                else if (code == KeyCode.Return)    { DecisionUI(); }
                else if (code == KeyCode.UpArrow)   { SelectUI(true); }
                else if (code == KeyCode.DownArrow) { SelectUI(false); }
                else { }
                break;
            default:
                break;
        }
    }
    #endregion
}
