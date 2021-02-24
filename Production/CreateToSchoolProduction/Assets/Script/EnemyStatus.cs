using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemyのステータス。
//Dataから引っ張ってくるときはnoを参考に引っ張る事！
[System.Serializable]
public class EnemyStatus
{
    public string name;     // 名前
    public int no;          // 個体ナンバー
    public int MaxHp;       // 名前通り
    public int Hp;          // 名前通り
    public int Atk;         // 名前通り
    public int SkillNo;     // 保持するスキル(能力？)
    public int Experience;  // 獲得経験値数
    public Mesh mesh;       // 見た目
    public Material material;//マテリアル
}