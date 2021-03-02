using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int ID = 0;
    protected EnemyStatus state;

    public void Started(EnemyStatus status,int id)
    {
        state = status;
        ID = id;
    }

    public EnemyStatus readState { get { return state; } }

    public virtual void Start()
    {
    }
    public virtual void Update()
    {
    }
    public virtual void FixedUpdate()
    {
        if (state.Hp <= 0) Die();
    }

    /// <summary>
    /// 死んだときの処理
    /// </summary>
    public virtual void Die()
    {
    }
}
