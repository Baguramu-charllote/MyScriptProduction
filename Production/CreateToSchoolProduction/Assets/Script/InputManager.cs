using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public List<KeyCode> keys = new List<KeyCode>();
    public List<string> names = new List<string>();

    Dictionary<string, KeyCode> pairs = new Dictionary<string, KeyCode>();
    bool[] iskey;
    void Awake()
    {
        for(int i = 0; i < keys.Count; i++)
        {
            pairs.Add(names[i], keys[i]);
        }
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void OnKeyDown()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode code in pairs.Values)
            {
                
            }
        }
    }
}
