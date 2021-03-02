using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scene上にあるEnemyを生成、管理するmanager
/// </summary>
public class EnemyManager : Singleton<EnemyManager>
{
    GameObject[] SceneEnemy;    

    void Awake()
    {
    　　//テスト用の追加
        //Transform e = GameObject.FindGameObjectWithTag("Enemy").transform;
        //List<GameObject> list = new List<GameObject>();
        //for (int i = 0; i < e.childCount; i++)
        //{
        //    list.Add(e.GetChild(i).gameObject);
        //}
        //SceneEnemy = list.ToArray();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    /// <summary>
    /// sceneに設定されたEnemyを生成する
    /// </summary>
    /// <param name="scene"></param>
    public void CreateEnemy(SceneState scene)
    {
        GameObject parent = new GameObject();
        parent.name = "Enemy";
        GameObject ene;
        List<GameObject> list = new List<GameObject>();
        foreach (SceneDetail s in scene.EntryEnemy)
        {
            ene = new GameObject();
            EnemyStatus enemy = GameManager.instance.datamanager.EnemyValueOut(s.id);
            ene.transform.parent = parent.transform;
            ene.name = enemy.name;
            ene.AddComponent<MeshFilter>();
            ene.AddComponent<MeshRenderer>();

            ene.GetComponent<MeshFilter>().mesh = enemy.mesh;
            ene.GetComponent<MeshRenderer>().material = enemy.material;

            ene.transform.position = s.SpornPos;
            list.Add(ene);
        }
        SceneEnemy = list.ToArray();
    }

    /// <summary>
    /// 近場の敵の座標を索敵する
    /// </summary>
    /// <returns></returns>
    public Vector2 NearEnemy()
    {
        Vector2 pos = Vector2.zero;
        float dis = 1000000;
        Transform p = GameManager.instance.player;
        foreach (GameObject g in SceneEnemy)
        {
            if (dis > Vector2.Distance(p.position, g.transform.position))
            {
                dis = Vector2.Distance(p.position, g.transform.position);
                pos = g.transform.position;
            }
        }
        return pos;
    }

    /// <summary>
    /// 敵に生成時割り振られているidをもとにダメージを食らったはずの子を特定し
    /// ダメージ分を食わらせる
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="Damage"></param>
    public void DamageEnemy(int ID ,int Damage)
    {
        BaseEnemy enemy;
        foreach(GameObject g in SceneEnemy)
        {
            enemy = g.GetComponent<BaseEnemy>();
            if (enemy.ID == ID)
            {
                enemy.readState.Hp -= Damage;
            }
        }
    }
}
