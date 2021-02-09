using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveManager 
{
    public void Writetext(string txt)
    {
        string path = @"..\\Text\lll.txt";
        string[] data = new string[]
        {
            "123","135","146","987","567","123","456",
        };
        string text = txt + ':';
        foreach(string u in data)
        {
            text += u + ':';
        }
        
        File.WriteAllText(path, text);
        string content = File.ReadAllText(path);
        string[] splitdata = content.Split(':');
        Debug.Log(string.Join("\n",splitdata));
    }
}
