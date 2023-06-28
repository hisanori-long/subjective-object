using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    public float speed = 1f; // 移動速度
    private float direction = 1f; // 移動方向（1: 正方向, -1: 逆方向）
    private Vector3 startPosition; // 初期位置
    private bool isStopped = false; // 停止フラグ
    private float stopTimer = 0f; // 停止タイマー

    void Start()
    {
        startPosition = transform.position; // 初期位置を保持
    }

    void Update()
    {
        if (!isStopped)
        {
            float newPosition = transform.position.x + direction * speed * Time.deltaTime; // 新しい位置を計算

            if (newPosition >= 1f || newPosition <= -1f)
            {
                direction *= -1f; // 移動方向を逆転させる
                isStopped = true; // 停止フラグを立てる
                stopTimer = 0f; // 停止タイマーをリセット
            }

            transform.position = new Vector3(newPosition, startPosition.y, startPosition.z); // キューブを移動させる
        }
        else
        {
            stopTimer += Time.deltaTime; // 停止時間を計測

            if (stopTimer >= 5f)
            {
                isStopped = false; // 停止フラグを解除
            }
        }
    }
}