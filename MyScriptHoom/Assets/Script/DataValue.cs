using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data(ScriptableObject)の中に保存する最低限のデータ
/// </summary>
[System.Serializable]
public struct Values
{    
    public string name;         // アイテムの名前
    public int no;              // アイテムのデータNo
    public int Eff;             // アイテムのスキルNo
    public ItemType itm_type;   // アイテムの種類
    public Sprite sprite;       // アイテムアイコンのイラスト
    public Mesh mesh;           // アイテムの3dデータ
    public Material material;   // アイテムのマテリアルデータ
}

// アイテムの特性を大雑把に表すもの。UI使用時の色分けなどに使う
public enum ItemType
{
    Material,   // 素材アイテム
    Hl,         // 回復アイテム
    Dmg,        // ステータスを持ったオブジェクトにダメージを与えるアイテム
    Bf,         // 　　　　　”　　　　　　　　　 にバフを与えるアイテム
    Dbf,        // 　　　　　”　　　　　　　　　 にデバフを与えるアイテム
}

// アイテムデータ()を作成するとき使う。
public class DataValue :MonoBehaviour
{
    public Values val;

    public Values SetValue()
    {
        Values a = new Values();
        return a;
    }
}
