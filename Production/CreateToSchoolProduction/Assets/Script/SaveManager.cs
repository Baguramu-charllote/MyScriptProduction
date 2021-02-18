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
    string path = @"..\\Text\createtoschoolproduction.txt";
    /// <summary>
    /// StringでplayerSaveDataを引っ張ってくる
    /// </summary>
    /// <returns></returns>
    public string Readtext()
    {
        string text = File.ReadAllText(path);
        
        return text;
    }

    /// <summary>
    /// SaveData用のstringをtextで書き出す
    /// </summary>
    /// <param name="d"></param>
    public void Writetext(string s)
    {
        string text = s;

        File.WriteAllText(path, text);       
    }

    /// <summary>
    /// Fileの存在確認
    /// </summary>
    /// <returns></returns>
    public bool IsFileLocked()
    {
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
