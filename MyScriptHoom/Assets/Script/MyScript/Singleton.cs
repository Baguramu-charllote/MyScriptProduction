using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    public static T inst;

    public static T instance
    {
        get
        {
            if (inst == null)
            {
                Type t = typeof(T);
                inst = (T)FindObjectOfType(t);

                if (inst == null)
                {
                    Debug.Log("NonAttachObject");
                }
            }
            return inst;
        }
    }

    void Awake()
    {
        CheckInst();
    }

    bool CheckInst()
    {
        if (inst == null)
        {
            inst = this as T;
            return true;
        }
        else if (inst == this)
        {
            return true;
        }
        Destroy(this);
        return false;
    }
}
