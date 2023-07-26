using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    public float moveSpeed = 4.3f; // 移動速度
    public float moveDistance = 4.3f; // 移動距離
    public float stopTime = 4f; // 停止時間（変更前は12fでした）

    private bool isMoving = false;
    private float currentMoveTime = 0f;
    private float moveTime = 2f; // オブジェクトが動く時間（追加）
    private int moveDirection = 1; // 1: 正の方向、-1: 負の方向
    private Rigidbody rb; // Rigidbodyコンポーネントを格納する変数

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbodyコンポーネントを取得
        if (rb == null)
        {
            Debug.LogError("Rigidbodyコンポーネントが見つかりません。このスクリプトをアタッチしたオブジェクトにRigidbodyを追加してください。");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = true; // スペースを押すと動き始める
            currentMoveTime = 0f; // 現在の移動時間をリセット
            moveDirection = 1; // 初回は正の方向に移動
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        if (currentMoveTime < moveTime) // オブジェクトが動く時間まで移動
        {
            // 移動中の場合
            float movement = moveSpeed * Time.fixedDeltaTime * moveDirection;
            rb.MovePosition(transform.position + new Vector3(movement, 0f, 0f));
            currentMoveTime += Time.fixedDeltaTime;
        }
        else
        {
            // 停止中の場合
            isMoving = false;
            rb.velocity = Vector3.zero;
            Invoke("ChangeDirection", stopTime); // 一定時間後に方向を変える
        }
    }

    private void ChangeDirection()
    {
        // 方向を反転させて再度移動を開始する
        moveDirection *= -1;
        isMoving = true;
        currentMoveTime = 0f;
    }
}
