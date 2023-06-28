using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour{

    [SerializeField] private Vector3 velocity;              // 移動方向
    [SerializeField] private float moveSpeed = 5.0f;        // 移動速度

    int phase = 0;
    float intervalTime = 0.5f;
    float elapsedTime = 0;
    

    void Update () {

        elapsedTime += Time.deltaTime;

        if (intervalTime < elapsedTime) // 2秒ごとに実行
        {

            if (phase <= 2)
            {
                velocity.x -= 1;
            }
            else if (phase <= 5)
            {
                velocity.x += 1;
            }
            else if (phase == 8)
            {
                phase = -1;
            }

            elapsedTime = 0.0f;
            phase += 1;
        }

        // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        // いずれかの方向に移動している場合
        if(velocity.magnitude > 0)
        {
            // プレイヤーの位置(transform.position)の更新
            // 移動方向ベクトル(velocity)を足し込みます
            transform.position += velocity;
        }
    }
}