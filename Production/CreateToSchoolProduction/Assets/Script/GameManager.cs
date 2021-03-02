using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Playerのステータス
    [System.NonSerialized] public Status s;
    [System.NonSerialized] public Transform player;

    // セーブ用のテキスト読み書き用Manager
    SaveManager saveM = new SaveManager();

    // skillを獲得したときこの配列に入れる
    bool[] Inventory = new bool[32];

    // 押した判定をとるためのbool配列
    bool[] keys;

    ParticleSystem getskill = null;

    [System.NonSerialized] public DataManager datamanager;
  
    [System.NonSerialized] public ReUIMnager uimanagr;

    [System.NonSerialized] public EnemyManager enemyManager;

    RectTransform CanvasRT;
    #region Unity

    void Awake()
    {
        s = new Status(saveM.Readtext());                    //要改良
        datamanager = GetComponent<DataManager>();
        enemyManager = GetComponent<EnemyManager>();
        GameObject canvas;
        canvas = GameObject.Find("Canvas");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        CanvasRT = canvas.GetComponent<RectTransform>();        
        uimanagr = GetComponent<ReUIMnager>();
        GameObject p = Instantiate(datamanager.Pskillget,player.position,Quaternion.identity);
        getskill = p.GetComponent<ParticleSystem>();
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

    #region private
    /// <summary>
    /// キー入力を受け取る
    /// </summary>
    void GetInput()
    {
        /*if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                uimanagr.InputKeyReceiver(KeyCode.Return);
                getSkill(1);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                uimanagr.InputKeyReceiver(KeyCode.Space);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                uimanagr.InputKeyReceiver(KeyCode.UpArrow);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                uimanagr.InputKeyReceiver(KeyCode.DownArrow);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                uimanagr.InputKeyReceiver(KeyCode.RightArrow);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                uimanagr.InputKeyReceiver(KeyCode.LeftArrow);
            }
        }*/
    }

    /// <summary>
    /// セーブする
    /// </summary>
    void save()
    {
        string savedata = "";
        savedata = s.SaveString + '\n' ;
        foreach(bool b in Inventory)
        {
            savedata += b?1:0 + ',';
        }
        saveM.Writetext(savedata);
    }

    //ロードする
    void Load()
    {
        string[] datas = saveM.Readtext().Split(';');
        s = new Status(datas[0]);
        string[] skills = datas[1].Split(',');
        for(int i = 0;i < skills.Length;i++)
        {
            if(Inventory.Length > i)
            {
                Inventory[i] = int.Parse(skills[i]) == 1 ? true : false;
            }
        }
    }

    #endregion

    #region public

    /// <summary>
    /// 簡単にkeyコンフィグをつくろうとした名残(無理)
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public bool onkeydown(KeyCode code)
    {
        bool onkey = false;

        return onkey;
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
        enemyManager.CreateEnemy(state);
    }

    public void getSkill(int SkillId)
    {
        Inventory[SkillId] = true;
        getskill.Play();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        save();
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        save();
        Application.Quit();
#endif
    }
    #endregion


}
