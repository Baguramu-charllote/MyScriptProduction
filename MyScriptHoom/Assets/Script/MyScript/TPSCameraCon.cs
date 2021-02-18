using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCameraCon : MonoBehaviour
{
    GameObject playerObj;               // 視点となるオブジェクト
    Vector3 lookpos = Vector3.zero;     // 実際にカメラを向ける座標
    float playdistance = 0.3f;      // 視点の遊び
    float followSmooth = 4.0f;          // 追いかけるときの速度

    float cameraToplayerDis = 2.5f;     // 視点からカメラまでの距離
    float cameraHeight = 1.0f;          // デフォルトのカメラの高さ
    float currentCameraHeight = 1.0f;   // 現在のカメラの高さ
    float leaveSmooth = 20.0f;

    float minCameraHeight = 0.3f;
    float maxCameraHeight = 3.0f;

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (playerObj == null) return;

        Updatelookposition();
        UpdateCameraPosition();

        transform.LookAt(lookpos);
    }

    /// <summary>
    /// 目標の視点と現在の視点の距離を求める
    /// </summary>
    void Updatelookposition()
    {
        Vector3 vec = playerObj.transform.position - lookpos;
        float distance = vec.magnitude;

        if(distance > playdistance)
        {
            float move_distance = (distance - playdistance) * (Time.deltaTime * followSmooth);
            lookpos += vec.normalized * move_distance;
        }
    }

    /// <summary>
    /// 次のカメラの位置を計算する
    /// </summary>
    void UpdateCameraPosition()
    {
        Vector3 vec = playerObj.transform.position - transform.position;
        vec.y = 0;
        float distance = vec.magnitude;

        float move_distance = 0;
        if(distance > cameraToplayerDis + playdistance) // 遠い時
        {
            move_distance = distance - (cameraToplayerDis + playdistance);
            move_distance *= Time.deltaTime * followSmooth;
            Debug.Log("Long");
        }
        else if(distance < cameraToplayerDis - playdistance) // 近い時
        {
            move_distance = distance - (cameraToplayerDis - playdistance);
            move_distance *= Time.deltaTime * followSmooth;
            Debug.Log("Near");
        }
        else{}

        Vector3 newCameraPos = transform.position + (vec.normalized * move_distance);
        newCameraPos.y = playerObj.transform.position.y + currentCameraHeight;

        transform.position = newCameraPos;
    }

    void Roll(float x,float y)
    {

        // 移動前の距離を保存する
        float prev_distance = Vector3.Distance(playerObj.transform.position, transform.position);
        Vector3 pos = transform.position;

        // 横に移動する
        pos += transform.right * x;

        // 縦に移動する
        currentCameraHeight = Mathf.Clamp(currentCameraHeight + y, minCameraHeight, maxCameraHeight);
        pos.y = lookpos.y + currentCameraHeight;

        // 移動後の距離を取得する
        float after_distance = Vector3.Distance(playerObj.transform.position, pos);

        // 視点を対象に向けて近づける（遊びをなくす）
        lookpos = Vector3.Lerp(lookpos, playerObj.transform.position, 0.1f);

        // カメラの更新
        transform.position = pos;
        transform.LookAt(lookpos);

        // 平行移動により若干距離が変わるので補正する
        transform.position += transform.forward * (after_distance - prev_distance);
    }
}
