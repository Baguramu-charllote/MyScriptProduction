using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 編集者：高木　基稔
/// ――――簡潔な仕様――――――――
/// Playerの移動に関することのみのClass
/// 入力は外部取得(専用クラスを制作予定)Vec3受取
/// 移動の実装にはRigidBodyのVelocityを原則使用
/// 継承しての利用を考慮して制作すること(fps、tps気にしない利用ができるようにする)
/// このクラス内には
/// (移動)(-未実装-ジャンプ)(接地判定)のみ操作できるようにする
/// ―――――――――――――――
/// </summary>

public class PlayerBaseController: MonoBehaviour
{
    [SerializeField] float speedValue = 0;            // 速度計算に使う値。
    [SerializeField] float maxSpeed = 0;            // 限界スピードを設定する。時速単位で表記する。(目標)
    [SerializeField] float jumpValue = 0;            // ジャンプの動作に使う上方向の力の強さ。
    [SerializeField] float disGround = 0.5f;        // 地面に飛ばすレイの処理 

    [SerializeField] Vector3 moveVec = Vector3.zero;    //移動の入力を受け取る
    [SerializeField] bool isTouchGround = false;        //
    [SerializeField] bool isRayGround = false;          //
    [SerializeField] bool isMove = false;               //
    [SerializeField] bool isJump = false;               //

    Ray ray;
    RaycastHit hit;
    Vector3 NormalVector;

    //マジックナンバーダメ絶対－－－－－－－－－－－－－－－－－－－－－
    float StopVec = 0.3f;
    float FallDeray = 1.28f;

    //－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－
    [System.NonSerialized] public Rigidbody rig = null;  // RigidBodyは継承先でも使う可能性があるのでpublic,かつ見えると邪魔なのでNonSerialized
    [System.NonSerialized] public Transform Camera;      // 
    #region Self-Made

    public void GSvec(Vector3 value)
    {
        moveVec = value;
        isMove = true;
    }

    public void UseGravity()
    {
        if (!isTouchGround)
        {
            rig.velocity = new Vector3(rig.velocity.x, -5f, rig.velocity.z);
        }
        else
        {

        }
    }

    /// <summary>
    /// 移動に関するメソッド
    /// </summary>
    public virtual void Move()
    {
        Vector3 vel = CalcMoveVector();
        rig.velocity = vel;
    }

    Vector3 CalcMoveVector()
    {
        Vector3 vec = rig.velocity;
        if (!isMove)
        {
            //現在の移動速度が少しでも動いていたら
            if (Vector2.SqrMagnitude(new Vector2(vec.x, vec.z)) > 0.2f)
            {
                vec = new Vector3(vec.x * 0.1f, vec.y, vec.z * 0.1f);
            }
            else
            {
                vec = new Vector3(0, vec.y, 0);
            }
        }
        else
        {
            if (Vector2.SqrMagnitude(new Vector2(vec.x, vec.z)) < maxSpeed * maxSpeed)
            {
                //前に向き直してから、前方に力をかけて移動する
                //ブレワイ風な動き

                //法線ベクトルを前面に方向転換するときの角度(quaternion)
                var quo1 = Quaternion.FromToRotation(transform.forward, NormalVector);
                //x軸を回転軸に指定数値分回転(角度,軸ベクトル)
                var quo2 = Quaternion.AngleAxis(90, new Vector3(1, 0, 0).normalized);
                var quo3 = quo2 * quo1;
                var pos = transform.position + quo3 * (Vector3.forward * speedValue);

                Vector3 forward = new Vector3(Camera.forward.x, 0, Camera.forward.z).normalized;
                forward = (forward * moveVec.z + Camera.right * moveVec.x).normalized;

                //入力方向にプレイヤーを回転する(ラジアン→度数に変換する定数　*　ラジアン,Y軸回転)
                transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(forward.x, forward.z), new Vector3(0, 1, 0));

                //Vector3 forward = transform.forward;
                vec = new Vector3(forward.x * speedValue, vec.y, forward.z * speedValue);

                Debug.DrawRay(transform.position, vec, Color.red);
            }
            isMove = false;
        }
        return vec;
    }

    /// <summary>
    /// Jumpに関するメソッド
    /// </summary>
    public virtual void Jump()
    {

    }

    /// <summary>
    /// 設置判定に関するメソッド(Ray&Colliderによって判断している)
    /// </summary>
    public void OnGroundPlayer()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        ray = new Ray(pos, -transform.up);
        if (Physics.SphereCast(ray, 1.0f, out hit, disGround))
        {
            NormalVector = hit.normal;
            isRayGround = true;
        }
        else
        {
            isRayGround = false;
        }
        Debug.DrawRay(hit.point, hit.normal * 50, Color.blue);
        //Debug.DrawRay(transform.position, (new Vector3(hit.normal.x, NormalVector.y < 0.98f ? 1 : 0,hit.normal.z) + rig.velocity) * 50, Color.green);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 9)
        {
            isTouchGround = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer == 9)
        {
            isTouchGround = false;
        }
    }
    #endregion
}
