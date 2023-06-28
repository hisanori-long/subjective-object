using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    public float speed = 0.1f; // 移動速度
    private float direction = 1f; // 移動方向（1: 正方向, -1: 逆方向）
    private Vector3 startPosition; // 初期位置
    private Vector3 targetPosition; // 目標位置
    public bool isStopped = false; // 停止フラグ
    private float stopTimer = 10f; // 停止タイマー

    void Start()
    {
        startPosition = transform.position; // 初期位置を保持
        targetPosition = new Vector3(-1f, startPosition.y, startPosition.z); // 目標位置を設定
    }

    void Update()
    {
        if (!isStopped)
        {
            float newPosition = transform.position.x + direction * speed * Time.deltaTime; // 新しい位置を計算

            if ((direction > 0f && newPosition >= targetPosition.x) || (direction < 0f && newPosition <= targetPosition.x))
            {
                transform.position = targetPosition; // 目標位置に到達
                isStopped = true; // 停止フラグを立てる
                stopTimer = 0f; // 停止タイマーをリセット
            }
            else
            {
                transform.position = new Vector3(newPosition, startPosition.y, startPosition.z); // キューブを移動させる
            }
        }
        else
        {
            stopTimer += Time.deltaTime; // 停止時間を計測

            if (stopTimer >= 7f)
            {
                isStopped = false; // 停止フラグを解除
                direction *= -1f; // 移動方向を逆転させる
                startPosition = transform.position; // 新しい移動の起点として現在位置を設定
                targetPosition = new Vector3(-targetPosition.x, startPosition.y, startPosition.z); // 新しい目標位置を反転させて設定
            }
        }
    }
}
