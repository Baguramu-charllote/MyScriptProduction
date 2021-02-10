using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [System.NonSerialized] public Vector3 time;
    [System.NonSerialized] public GameObject[] items;
    public ItemState[] Inventory;
    DataManager dataManager = null;

    public int ItemCount = 0;
    public int EnemyCount = 0;
    public bool isMoving = false;
    GameObject preUI;
    GameObject inventoryObj;
    GameObject[] Magic_Inv;

    int itemUiPlayDis = 5;
    int itemPosPlayDis = 2;
    void Start()
    {
        inventoryObj = GameObject.FindGameObjectWithTag("Inventory");
        OpenInventory();

        dataManager = GameObject.FindGameObjectWithTag("DataBase").GetComponent<DataManager>();

        items = GameObject.FindGameObjectsWithTag("Item");
        isMoving = true;

        //GameObject a = null;
        //for(int i = 0; i < ItemCount; i++)
        //{
        //    Vector3 pos = new Vector3(Random.Range(0, 100), 50, Random.Range(0, 100));
        //    a = Instantiate(gameObject, pos, Quaternion.identity);
        //    a.AddComponent<ItemState>();
        //}
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
    }
    #region Inventory
    /// <summary>
    /// インベントリを開閉する
    /// </summary>
    public void ISInventory()
    {
        if (inventoryObj.activeSelf)
        {
            inventoryObj.SetActive(!inventoryObj.activeSelf);
            isMoving = true;
        }
        else
        {
            inventoryObj.SetActive(!inventoryObj.activeSelf);
            //OpenInventory();
            isMoving = false;
        }
    }

    /// <summary>
    /// インベントリのUIの中身を構成する
    /// </summary>
    public void OpenInventory()
    {
        if (items == null)
        {
            preUI = (GameObject)Resources.Load("preUI");
            Inventory = new ItemState[10];
            Magic_Inv = new GameObject[Inventory.Length];
            float base_Width = inventoryObj.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x - 100.0f;
            float itemUI_Width = preUI.GetComponent<RectTransform>().sizeDelta.x;

            int itemUiXCount = (int)System.Math.Ceiling(base_Width / itemUI_Width);
            int CreatedCount = 0;
            float UIposX = 0;
            float UIposY = itemUI_Width;
            int i = 0;
            do
            {
                UIposX = (-base_Width / 2) + ((base_Width / itemUiXCount) + (itemUiPlayDis * 2)) * i;

                GameObject ui = Instantiate(preUI);
                ui.transform.SetParent(inventoryObj.transform, false);
                ui.GetComponent<RectTransform>().localPosition = new Vector3(UIposX, UIposY, 0);
                Magic_Inv[CreatedCount] = ui;
                CreatedCount++; i++;
                if (i == itemUiXCount)
                {
                    UIposY += -(itemUI_Width + itemUiPlayDis * 2);
                    i = 0;
                }
            }
            while (CreatedCount < Inventory.Length);
            inventoryObj.SetActive(false);
        }
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] != null)
            {
                Image img = Magic_Inv[i].transform.GetChild(0).GetComponent<Image>();
                Text text = Magic_Inv[i].transform.GetChild(1).GetComponent<Text>();
                //img.sprite = Inventory[i].itemNo;
                //text.text = Inventory[i].val.name;
            }
        }
    }
    /// <summary>
    /// Inventoryにアイテムをしまう
    /// </summary>
    /// <param name="item"></param>
    public void CloseUpInventory(GameObject item)
    {
        if (item == null) return;
        if (item.GetComponent<Item>())
        {

        }
    }
    #endregion

    #region Item
    /// <summary>
    /// 取得したアイテムをマップから消す
    /// </summary>
    /// <param name="it"></param>
    public void DestroyItem(GameObject it)
    {
        List<GameObject> its = new List<GameObject>();
        GameObject des = null;

        foreach (GameObject g in items)
        {
            if (g.transform.position == it.transform.position)
            {
                des = g;
            }
            else
            {
                its.Add(g);
            }
        }

        items = its.ToArray();

        if (des != null)
        {
            des.transform.position = new Vector3(100, 100, 100);
            //Destroy(des);
        }
    }

    /// <summary> 
    /// 送られてきたポジションの近くにアイテムがあるか検索する
    /// </summary>
    /// <param name="pos">検索する座標</param>
    /// <returns></returns>
    public GameObject getValue(Vector3 pos)
    {
        foreach (GameObject v in items)
        {
            if (v.transform.position.x - itemPosPlayDis < pos.x && v.transform.position.x + itemPosPlayDis > pos.x)
            {
                if (v.transform.position.z - itemPosPlayDis < pos.z && v.transform.position.z + itemPosPlayDis > pos.z)
                {
                    return v;
                }
            }
        }
        return null;
    }
    #endregion

    /// <summary>
    /// ゲーム終了用
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
