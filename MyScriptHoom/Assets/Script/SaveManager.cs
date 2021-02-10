using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// SaveManagerとなっているが正確にはSaveに使っているtxtデータにアクセスするだけ
/// </summary>
public class SaveManager
{
    /// <summary>
    /// String配列でplayerSaveDataを引っ張ってくる
    /// </summary>
    /// <returns></returns>
    public string[] Readtext()
    {
        string path = @"..\\Text\lll.txt";
        string text = File.ReadAllText(path);

        string[] arry = text.Split(':');
        return arry;
    }

    /// <summary>
    /// SaveData用のstringをtextで書き出す
    /// </summary>
    /// <param name="d"></param>
    public void Writetext(ObjState d)
    {
        string path = @"..\\Text\lll.txt";
        string text = d.SaveString;

        File.WriteAllText(path, text);       
    }

    /// <summary>
    /// Fileの存在確認
    /// </summary>
    /// <returns></returns>
    public bool IsFileLocked()
    {
        string path = @"..\\Text\lll.txt";

        FileStream stream = null;

        try
        {
            stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }
        catch
        {
            return true;
        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
            }
        }

        return false;
    }
}
