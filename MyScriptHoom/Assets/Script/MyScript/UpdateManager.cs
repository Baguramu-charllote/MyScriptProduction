using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdate
{
    void FixedUpdated();
    void Updated();
    void OnEnable();
    void OnDisable();
}

public class UpdateManager : Singleton<UpdateManager>
{
    public System.Action Updated;
    public System.Action FixedUpdated;

    public void Add(IUpdate update)
    {
        Updated += update.Updated;
        FixedUpdated += update.FixedUpdated;
    }

    public void Delet(IUpdate update)
    {
        Updated -= update.Updated;
        FixedUpdated -= update.FixedUpdated;
    }

    void Update()
    {
        if (Updated != null)
        {
            Updated();
        }
        if(FixedUpdated != null)
        {
            FixedUpdated();
        }
    }
}
