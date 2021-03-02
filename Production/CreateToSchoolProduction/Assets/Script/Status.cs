using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;

/// <summary>
/// PLayerのステータスの本体
/// </summary>
public struct StatusValue
{
    public string name;
    public int Hp;
    public int MaxHp;
    public int Atk;
    public int Agi;
    public int Cost;
    public int Enlightenment;
    public int[] Skill;
    public int[] PSkill;
    public int[] Item;
    public StatusValue(int i = 0)
    {
        name = "sawai";
        Hp = i;
        MaxHp = i;
        Atk = i;
        Agi = i;
        Cost = i;
        Enlightenment = i;
        Skill = Enumerable.Repeat(i, 3).ToArray();
        PSkill = Enumerable.Repeat(i, 3).ToArray();
        Item = Enumerable.Repeat(i,5).ToArray();
    }
}

/// <summary>
/// playerのステータスを構うClass
/// セーブロードから
/// </summary>
public class Status
{
    public StatusValue status;
    public Status(string s)
    {
        status = new StatusValue(0);
        LoadString = s;       
    }

    /// <summary>
    /// SaveManagerに送るstringをstatusから作る
    /// </summary>
    public string SaveString
    {
        get
        {
            string data = "";

            FieldInfo[] info = status.GetType().GetFields();
            foreach (FieldInfo i in info)
            {
                Type t = i.GetValue(status).GetType();
                if (t.IsValueType || t == typeof(String))
                {
                    data += i.GetValue(status).ToString() + ':';
                }
                else
                {
                    if (t.IsArray)
                    {
                        foreach(object a in (Array)i.GetValue(status))
                        {
                            data += a.ToString() + ',';
                        }
                        data = data.Remove(data.Length-1,1);
                        data += ':';
                    }
                }
            }
            return data;
        }
    }

    /// <summary>
    /// txtファイルから読み込んだstringをStatusに入れる
    /// </summary>
    public string LoadString
    {
        set
        {
            string[] v = value.Split(':');
            status.name = v[0];
            status.Hp = int.Parse(v[1]);
            status.MaxHp = int.Parse(v[2]);
            status.Atk = int.Parse(v[3]);
            status.Agi = int.Parse(v[4]);
            status.Cost = int.Parse(v[5]);
            status.Enlightenment = int.Parse(v[6]);
            string[] skill = v[7].Split(',');
            string[] pskill = v[8].Split(',');
            string[] item = v[9].Split(',');
            int a = 0;
            foreach (string s in skill)
            {
                status.Skill[a] = int.Parse(s);
                a++;
            }
            a = 0;
            foreach (string s in pskill)
            {
                status.PSkill[a] = int.Parse(s);
                a++;
            }
            a = 0;
            foreach (string i in item)
            {
                status.Item[a] = int.Parse(i);
                a++;
            }
            a = 0;
        }
    }
}
