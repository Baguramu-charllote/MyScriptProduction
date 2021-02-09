using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerBaseController
{
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

    public void GetItem()
    {
        if(NearObj == null)
        {
            return;
        }
        ItemState[] inv = GameManager.instance.Inventory;
        ItemState itm = new ItemState();
        int inveLength = inv.Length;
        for (int i = 0; i < inveLength; i++)
        {
            if (inv[i] == null)
            {
                itm = NearObj.GetComponent<ItemState>();
                inv[i] = itm;
                GameManager.instance.DestroyItem(NearObj);
                NearObj = null;
                return;
            }
            else if (inv[i].val.no == itm.val.no)
            {
                inv[i].Count += itm.Count;
                GameManager.instance.DestroyItem(NearObj);
                NearObj = null;
                return;
            }
        }
        GameManager.instance.Inventory = inv;
    }
}
