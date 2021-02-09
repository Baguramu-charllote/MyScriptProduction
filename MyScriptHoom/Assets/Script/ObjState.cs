using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjState
{
   [SerializeField] string name = null;
   [SerializeField] int Hp = 0;
   [SerializeField] int MaxHp = 0;
   [SerializeField] int Atk = 0;
   [SerializeField] int Def = 0;
   [SerializeField] int Agi = 0;
   [SerializeField] int NextLebel = 0;

    // Saveに使う----------------
    public string SaveString
    {
        get
        {
            string data = name + ':' + Hp.ToString() + ':' + MaxHp.ToString() + ':' + Atk.ToString() + ':'
                + Def.ToString() + ':' + Agi.ToString();
            return data;
        }
    }
    // --------------------------

    // 呼び出しに使う------------
    public string GetSetName
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    public int GetSetExperience
    {
        get { return NextLebel; }
        set { NextLebel = value; }
    }
    public bool CheckExperience
    {
        get { return NextLebel > 0 ? false : true; }
    }
    public int getHp
    {
        get { return Hp; }
    }
    public int getMHp
    {
        get { return MaxHp; }
    }
    public int getAtk
    {
        get { return Atk; }
    }
    public int getDef
    {
        get { return Def; }
    }
    public int getAgi
    {
        get { return Agi; }
    }
    // --------------------------

    //　AddDicに使う-------------
    public int AddHp
    {
        set
        {
            if (MaxHp >= Hp + value)
            {
                Hp += value;
            }
            else
            {
                Hp = MaxHp;
            }
        }
    }
    public int AddMaxHp
    {
        set{MaxHp += value;}
    }
    public int AddAtk
    {
        set{Atk += value;}
    }
    public int AddDef
    {
        set { Def += value; }
    }
    public int AddAgi
    {
        set { Agi += value; }
    }
    public int DecExperience
    {
        set { NextLebel -= value; }
    }
    public int Damage
    {
        set { Hp -= value; }
    }
    //　--------------------------
}
