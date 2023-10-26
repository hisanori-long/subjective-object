using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    public float moveSpeed = 5f; // オブジェクトの移動速度

    private string moveStatus = "stop"; // オブジェクトの移動状態を示す変数
    private float startTime; // ゲーム開始時の時間

    void Start()
    {
        startTime = Time.time; // ゲーム開始時の時間を記録
    }

    void Update()
    {
        // ゲーム開始からの経過時間を取得
        float elapsedTime = Time.time - startTime;

        // オブジェクトを左右に移動させる
        if (moveStatus == "moveForward")
        {
            // オブジェクトを前進させる
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else if (moveStatus == "turnRight")
        {
            // オブジェクトを右に移動
            transform.Rotate(Vector3.up, 60f * Time.deltaTime);
        }
        else if (moveStatus == "turnLeft")
        {
            // オブジェクトを左に移動
            transform.Rotate(Vector3.up, -60f * Time.deltaTime);
        }


        if (elapsedTime >= 5f && elapsedTime < 31.5f) // 左右を見渡す
        {
            moveStatus = "moveForward";
        }
        else if (elapsedTime >= 34f && elapsedTime < 37f)
        {
            moveStatus = "turnLeft";
        }
        else if (elapsedTime >= 40f && elapsedTime < 41.5f)
        {
            moveStatus = "turnRight";
        }
        else if (elapsedTime >= 44f && elapsedTime < 53.5f) // 前進する
        {
            moveStatus = "moveForward";
        }
        else if (elapsedTime >= 55f && elapsedTime < 56.5f) // 右に方向転換
        {
            moveStatus = "turnRight";
        }
        else if (elapsedTime >= 57f && elapsedTime < 60f) // 右に方向転換
        {
            moveStatus = "turnLeft";
        }
        else if (elapsedTime >= 61f && elapsedTime < 70.5f) // 前進する
        {
            moveStatus = "moveForward";
        }
        else if (elapsedTime >= 71f && elapsedTime < 72.5f) // 右に方向転換
        {
            moveStatus = "turnLeft";
        }
        else if (elapsedTime >= 73f && elapsedTime < 82.5f) // 前進する
        {
            moveStatus = "moveForward";
        }
        else if (elapsedTime >= 84f && elapsedTime < 85.5f) // 前進する
        {
            moveStatus = "turnLeft";
        }
        else
        {
            moveStatus = "stop";
        }
    }
}
