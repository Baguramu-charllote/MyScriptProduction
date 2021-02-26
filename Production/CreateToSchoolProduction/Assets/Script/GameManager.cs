using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("UIObject")]
    [SerializeField] GameObject UI = null;

    // Playerのステータス
    [System.NonSerialized] public Status s;

    DataManager datamanager;

    // セーブ用のテキスト読み書き用Manager
    SaveManager saveM = new SaveManager();

    // skillを獲得したときこの配列に入れる
    int[] Inventory = new int[64];

    // 押した判定をとるためのbool配列
    bool[] keys;
  
    [System.NonSerialized] public UIOperationManager uimanagr;

    RectTransform CanvasRT;
    #region Unity

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
        CreateMap();
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        
    }

    #endregion

    #region propaty
    public bool IsOpenUI { get { return uimanagr.isOpenUI; } }

    #endregion


    #region public
    public void test()
    {
        string a = s.SaveString;
    }
    public bool onkeydown(KeyCode code)
    {
        bool onkey = false;

        return onkey;
    }
    #endregion

    #region private
    void GetInput()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                uimanagr.OpenUI();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                uimanagr.SelectUI(true);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                uimanagr.SelectUI(false);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                uimanagr.DecisionUI();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

            }
        }
    }

    /// <summary>
    /// Scene開始時マップをロードの裏で生成するメソッド
    /// </summary>
    public void CreateMap()
    {
        int rnd = Random.Range(0, 9);
        SceneState state = datamanager.SceneValueOut(rnd);
        ObjInfo wall = datamanager.wall;

        // 壁の生成
        for (int i = 0; i < state.WallEntry.Length; i++)
        {
            GameObject obj = new GameObject();
            obj.name = "Wall" + i.ToString();

            obj.AddComponent<MeshFilter>();
            obj.AddComponent<MeshRenderer>();

            obj.GetComponent<MeshFilter>().mesh = wall.mesh;
            obj.GetComponent<MeshRenderer>().material = wall.material;

            obj.transform.position = state.WallEntry[i].SpornPos;
            obj.transform.localScale = state.WallEntry[i].Scale;
        }
        // 敵の生成
        for (int i = 0; i < state.EntryEnemy.Length; i++)
        {
            GameObject obj = new GameObject();
            obj.name = "Enemy" + i.ToString();
            EnemyStatus enemy = datamanager.EnemyValueOut(state.EntryEnemy[i].id);
            obj.AddComponent<MeshFilter>();
            obj.AddComponent<MeshRenderer>();

            obj.GetComponent<MeshFilter>().mesh = enemy.mesh;
            obj.GetComponent<MeshRenderer>().material = enemy.material;

            obj.transform.position = state.EntryEnemy[i].SpornPos;
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        Application.Quit();
#endif
    }
    #endregion
}
