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
    public int Stamina;
    public int MaxStamina;
    public int Cost;
    public int Enlightenment;
    public int[] Skill;
    public int[] Item;
    public StatusValue(int i = 0)
    {
        name = "Blood slurping";
        Hp = i;
        MaxHp = i;
        MaxStamina = i;
        Stamina = i;
        Cost = i;
        Enlightenment = i;
        Skill = Enumerable.Repeat(i, 3).ToArray();
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
            string data = ""/*status.name + ':'*/;
            //data += status.Hp.ToString() + ':';
            //data += status.MaxHp.ToString() + ':';
            //data += status.MaxStamina.ToString() + ':';
            //data += status.Cost.ToString() + ':';
            //data += status.Enlightenment.ToString() + ':';
            //foreach(int i in status.Skill)
            //{
            //    data += i.ToString() + ',';
            //}
            //data = data.Remove(data.Length-1,1);
            //data += ':'; 
            //foreach(int i in status.Item)
            //{
            //    data += i.ToString() + ',';
            //}
            //data = data.Remove(data.Length - 1,1);

            // 上を短くしたい↓
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
            status.MaxStamina = int.Parse(v[3]);
            status.Cost = int.Parse(v[4]);
            status.Enlightenment = int.Parse(v[5]); 
            string[] skill = v[6].Split(',');
            string[] item = v[7].Split(',');
            int a = 0;
            foreach (string s in skill)
            {
                status.Skill[a] = int.Parse(s);
                a++;
            }
            a = 0;
            foreach (string i in item)
            {
                status.Item[a] = int.Parse(i);
                a++;
            }
        }
    }
}
