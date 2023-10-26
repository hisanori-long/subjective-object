using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toio;

public class ExtraScene : MonoBehaviour
{
    public int moveSpeed = 1500; // キューブの移動速度（スピード）

    private string moveStatus = "stop"; // キューブの移動状態を示す変数
    private float startTime; // ゲーム開始時の時間

    public ConnectType connectType;
    CubeManager cm;

    async void Start()
    {
        // ConnectType.Auto - ビルド対象に応じて内部実装が自動的に変わる
        // ConnectType.Simulator - ビルド対象に関わらずシミュレータのキューブで動作する
        // ConnectType.Real - ビルド対象に関わらずリアル(現実)のキューブで動作する
        cm = new CubeManager(connectType);
        await cm.MultiConnect(2);
        startTime = Time.time; // ゲーム開始時の時間を記録
    }

    void Update()
    {
        if (cm.synced)
        {
            foreach (var cube in cm.syncCubes)
            {
                // ゲーム開始からの経過時間を取得
                float elapsedTime = Time.time - startTime;

                // オブジェクトを左右に移動させる
                if (moveStatus == "moveForward")
                {
                    // オブジェクトを前進させる
                    cube.Move(8, 8, (short)moveSpeed); // moveSpeedをintからshortにキャスト
                }
                else if (moveStatus == "turnRight")
                {
                    // オブジェクトを右に移動
                    cube.Move(10, -10, (short)moveSpeed); // moveSpeedをintからshortにキャスト
                }
                else if (moveStatus == "turnLeft")
                {
                    // オブジェクトを左に移動
                    cube.Move(-10, 10, (short)moveSpeed); // moveSpeedをintからshortにキャスト
                }

                // 移動制御のタイミングと方向を設定
                if (elapsedTime >= 0f && elapsedTime < 31.5f) // 左右を見渡す
                {
                    moveStatus = "moveForward";
                }
                else if (elapsedTime >= 34f && elapsedTime < 37f)
                {
                    moveStatus = "moveForward";
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
    }
}
