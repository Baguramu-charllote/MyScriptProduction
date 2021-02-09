using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjState : MonoBehaviour
{
    int Hp;
    int MaxHp;
    int Atk;
    int Def;
    int Agi;

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
    public int Damage
    {
        set { Hp -= value; }
    }
}
