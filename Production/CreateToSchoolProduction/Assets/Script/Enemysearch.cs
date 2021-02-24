using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 仕様
/// gameobject(gamemanagerとかと一緒にでも使って)に入れて使うよ！
/// 呼び出しはEnemysearch.instance.NearEnemy()で近い敵のvector2が渡されるのでよろしこ。
/// </summary>
public class Enemysearch : Singleton<Enemysearch>
{
    GameObject[] enemys;
    Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Transform e = GameObject.FindGameObjectWithTag("enemys").transform;
        List<GameObject> list = new List<GameObject>();
        for(int i = 0; i < e.childCount; i++)
        {
            list.Add(e.GetChild(i).gameObject);
        }
        enemys = list.ToArray();
    }

    public Vector2 NearEnemy()
    {
        Vector2 pos = Vector2.zero;
        float dis = 1000000;
        foreach(GameObject g in enemys)
        {
            if(dis > Vector2.Distance(player.position,g.transform.position))
            {
                dis = Vector2.Distance(player.position, g.transform.position);
                pos = g.transform.position;
            }
        }
        return pos;
    }
}
