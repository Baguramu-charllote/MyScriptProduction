﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("UIObject")]
    [SerializeField] GameObject UI = null;

    // Playerのステータス
    Status s;

    DataManager datamanager;

    // セーブ用のテキスト読み書き用Manager
    SaveManager saveM = new SaveManager();

    // skillを獲得したときこの配列に入れる
    int[] Inventory = new int[64];

    [System.NonSerialized] public UIOperationManager uimanagr;

    RectTransform CanvasRT;

    void Awake()
    {
        s = new Status(saveM.Readtext());
        datamanager = GetComponent<DataManager>();
        GameObject canvas;
        canvas = GameObject.Find("Canvas");
        CanvasRT = canvas.GetComponent<RectTransform>();
        List<GameObject> UIs = new List<GameObject>();
        foreach (Transform t in (UI.transform.GetComponentInChildren<Transform>()))
        {
            UIs.Add(t.gameObject);
        }
        uimanagr = new UIOperationManager(canvas, UIs.ToArray());
    }

    void Start()
    {
    }

    void Update()
    {
        GetInput();
    }

    public void test()
    {
        string a = s.SaveString;
        Debug.Log(a);
    }

    void GetInput()
    {
        if (Input.anyKeyDown)
        {
            //UIにおいてのkeyでの入力受け取り
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                uimanagr.SelectUI(true);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                uimanagr.SelectUI(false);
            }
            
            if (Input.GetKeyDown(KeyCode.Return))
            {

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                uimanagr.OpenUI();
            }
        }
    }

    /// <summary>
    /// Scene開始時マップをロードの裏で生成するメソッド
    /// </summary>
    public void CreateMap()
    {

    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        Application.Quit();
#endif
    }
}