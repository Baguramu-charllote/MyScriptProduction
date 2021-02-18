using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerBaseController
{
    public ObjState state;

    GameObject NearObj;
    int flame = 0;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        //近くにアイテムがある順
        flame++;
        if (flame == 10)
        {
            itemCheck();
            flame = 0;
        }
        Move();
        Jump();
        UseGravity();
        OnGroundPlayer();
    }

    void itemCheck()
    {
        NearObj = GameManager.instance.getValue(transform.position);
    }

    /// <summary>
    /// そばにあるアイテムをインベントリに回収する。
    /// ここでの動作はGameManagerにそばのアイテムを教えて渡すこと
    /// </summary>
    public void GetItem()
    {
        if(NearObj == null)
        {
            return;
        }
        GameManager.instance.CloseUpInventory(NearObj);
    }
}
