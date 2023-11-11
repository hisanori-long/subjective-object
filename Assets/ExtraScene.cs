using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toio;

public class ExtraScene : MonoBehaviour
{
    public GameObject shadow;

    private ShadowScript shadowscript;
    private float startTime; // ゲーム開始時の時間
    private float phase = 0; // ゲームの進行度
    public ConnectType connectType;
    CubeManager cm;
    Cube cube;
    float eulersZValue; // キューブの姿勢角度(正が右回転、負が左回転)
    float storedEulersZValue = 1000; // 保存されたキューブの姿勢角度
    float elapsedTime; // 各フェーズの経過時間
    public float rotationSpeed = 180f;
    public float moveSpeed = 0.0004f;
    async void Start()
    {
        // shadowオブジェクトのShadowScriptコンポーネントを取得
        shadowscript = shadow.GetComponent<ShadowScript>();

        // キューブマネージャーの初期化・接続
        cm = new CubeManager(connectType);
        cube = await cm.SingleConnect();


        // キューブの接続が完了したら、キューブのイベントを登録
        cube.attitudeCallback.AddListener("EventScene", OnAttitude);
        await cube.ConfigAttitudeSensor(Cube.AttitudeFormat.Eulers, 100, Cube.AttitudeNotificationType.OnChanged);
        await cube.ConfigMagneticSensor(Cube.MagneticMode.MagnetState);

        resetTime(); // ゲーム開始時の時間を記録

    }
    void OnAttitude(Cube c)
    {
        //Debug.Log($"attitude = {c.eulers.x}, {c.eulers.y}, {c.eulers.z}");
        eulersZValue = c.eulers.z;
    }

    void Update()
    {
        elapsedTime = Time.time - startTime; // 経過時間を計算

        if (phase == 0)
        {
            phase0();
        }
        else if (phase == 1)
        {
            phase1();
        }
        else if (phase == 2)
        {
            phase2();
        }
        else if (phase == 3)
        {
            phase3();
        }
        else if (phase == 4)
        {
            phase4();
        }
        else if (phase == 5)
        {
            phase5();
        }
        else if (phase == 6)
        {
            phase6();
        }

    }

    void phase0()
    {


        if (elapsedTime >= 5f)
        {
            // 次のphaseへ
            resetTime();
            phase = 1;
            storedEulersZValue = eulersZValue;
            // storedEulersZValue = 42;
            Debug.Log($"storedEulersZValue is saved: {storedEulersZValue}");
        }
    }
    void phase1()
    {

        if (elapsedTime >= 2f && elapsedTime < 2.5f)
        {
            toioRight();
            shadowRight();
        }
        else if (elapsedTime >= 3f && elapsedTime < 4f)
        {
            toioLeft();
            shadowLeft();
        }
        else if (elapsedTime >= 4.5f && elapsedTime < 5f)
        {
            toioRight();
            shadowRight();
        }
        else if (elapsedTime >= 5f)
        {
            toioFixDirection(0);
            shadowFixDirection(0);
        }
    }
    void phase2()
    {
        if (elapsedTime >= 2f && elapsedTime < 9f)
        {
            toioForward(0);
            shadowForward();
        }
        else if (elapsedTime >= 11.5f && elapsedTime < 12f)
        {
            toioRight();
            shadowRight();
        }
        else if (elapsedTime >= 14f && elapsedTime < 14.5f)
        {
            toioLeft();
            shadowLeft();
        }
        else if (elapsedTime >= 15f)
        {
            // 次のphaseへ
            toioFixDirection(0);
            shadowFixDirection(0);
        }
    }
    void phase3()
    {
        // 
        if (elapsedTime >= 1f && elapsedTime < 7f)
        {
            toioForward(0);
            shadowForward();
        }
        else if (elapsedTime >= 8.5f && elapsedTime < 8.7f)
        {
            toioRight();
            shadowRight();
        }
        else if (elapsedTime >= 11f && elapsedTime < 11.7f)
        {
            toioLeft();
            shadowLeft();
        }
        else if (elapsedTime >= 12.2f && elapsedTime < 13.2f)
        {
            toioRight();
            shadowRight();
        }
        else if (elapsedTime >= 13.7f && elapsedTime < 14.2f)
        {
            toioLeft();
            shadowLeft();
        }
        else if (elapsedTime >= 15f && elapsedTime < 15.5f)
        {
            toioStop();
            shadowLeft();
        }
        else if (elapsedTime >= 16f && elapsedTime < 17f)
        {
            toioStop();
            shadowRight();
        }
        else if (elapsedTime >= 17.5f && elapsedTime < 18f)
        {
            toioStop();
            shadowLeft();
        }
        else if (elapsedTime >= 19f)
        {
            // 次のphaseへ
            toioFixDirection(0);
            shadowFixDirection(0);
        }

    }
    void phase4()
    {
        if (elapsedTime >= 1f && elapsedTime < 1.55f)
        {
            toioStop();
            shadowRight();
        }
        else if (elapsedTime >= 1.6 && elapsedTime < 2f)
        {
            shadowFixDirection(-60);
        }
        else if (elapsedTime >= 2f && elapsedTime < 3.7f)
        {
            toioStop();
            shadowForward();
        }
        else if (elapsedTime >= 3.7f && elapsedTime < 4.25f)
        {
            toioLeft();
            shadowForward();
        }
        else if (elapsedTime >= 4.1f)
        {
            // 次のphaseへ
            toioFixDirection(-60);
        }
    }
    void phase5()
    {
        if (elapsedTime >= 1f && elapsedTime < 1.5f)
        {
            toioLeft();
            shadowRight();
        }
        else if (elapsedTime >= 1.5f && elapsedTime < 1.9f)
        {
            toioStop();
            shadowRight();
        }
        else if (elapsedTime >= 1.9f && elapsedTime < 2f)
        {
            shadowFixDirection(-150);
        }
        else if (elapsedTime >= 2f && elapsedTime < 3f)
        {
            toioRight();
        }
        else if (elapsedTime >= 3.5f && elapsedTime < 4f)
        {
            toioLeft();
        }
        else if (elapsedTime >= 4f && elapsedTime < 5.2f)
        {
            toioForward(-60);
        }
        else if (elapsedTime >= 6f && elapsedTime < 6.5f)
        {
            toioRight();
        }
        else if (elapsedTime >= 7f && elapsedTime < 8f)
        {
            toioLeft();
        }
        else if (elapsedTime >= 8.5f && elapsedTime < 10.2f)
        {
            toioRight();
        }
        else if (elapsedTime >= 14f)
        {
            toioFixDirection(80);
        }
    }
    void phase6()
    {
        if (elapsedTime >= 1f && elapsedTime < 4f)
        {
            toioForward(80);
        }
    }

    void toioForward(float direction)
    {
        float difEulersZValue = eulersZValue - storedEulersZValue - direction;  // 現在のキューブの姿勢角度
        // difEulersZValueが正の場合

        if (difEulersZValue > 0 && difEulersZValue <= 180)
        {
            // 左回転
            cube.Move(12, 13, 100);
        }
        else if (difEulersZValue < -180)
        {
            // 左回転
            cube.Move(12, 13, 100);
        }
        else if (difEulersZValue < 0 && difEulersZValue >= -180)
        {
            // 右回転
            cube.Move(13, 12, 100);
        }
        else if (difEulersZValue > 180)
        {
            // 右回転
            cube.Move(13, 12, 100);
        }
        else if (difEulersZValue == 0)
        {
            // 前進
            cube.Move(12, 12, 100);
        }
    }

    void toioRight()
    {
        cube.Move(8, -8, 100);
    }

    void toioLeft()
    {
        cube.Move(-8, 8, 100);
    }
    void toioStop()
    {
        cube.Move(0, 0, 100);
    }

    void shadowForward()
    {
        // shadowオブジェクトのTransformコンポーネントを取得し、前進させる
        shadow.transform.Translate(0f, 0f, -moveSpeed * Time.deltaTime);
    }

    void shadowRight()
    {
        // shadowオブジェクトのTransformコンポーネントを取得し、右回転させる
        shadow.transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
    }

    void shadowLeft()
    {
        // shadowオブジェクトのTransformコンポーネントを取得し、左回転させる
        shadow.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }


    void toioFixDirection(float direction)
    {
        float difEulersZValue = eulersZValue - storedEulersZValue - direction;  // 現在のキューブの姿勢角
        float absdifEulersZValue = Mathf.Floor(Mathf.Abs(difEulersZValue) / 180);
        Debug.Log($"difEulersZValue: {difEulersZValue}, absdifEulersZValue: {absdifEulersZValue}");

        if (difEulersZValue % 360 == 0)
        {
            // 次のphaseへ
            phase = phase + 1;
            resetTime();
        }
        else if (difEulersZValue > 0)
        {
            // 奇数の場合
            if (absdifEulersZValue % 2 == 1)
            {
                // 右回転
                cube.Move(0, -8, 23);
            }
            // 偶数の場合
            else
            {
                // 左回転
                cube.Move(-8, 0, 23);
            }
        }
        else if (difEulersZValue < 0)
        {
            // 奇数の場合
            if (absdifEulersZValue % 2 == 1)
            {
                // 左回転
                cube.Move(-8, 0, 23);
            }
            // 偶数の場合
            else
            {
                // 右回転
                cube.Move(0, -8, 23);
            }
        }
    }

    void shadowFixDirection(float direction)
    {
        // 影の方向をdirectionに合わせる
        shadow.transform.rotation = Quaternion.Euler(0, direction - 90, 0);
    }



    void resetTime()
    {
        startTime = Time.time;
        cube.PlayPresetSound(0);
    }

    // void Update()
    // {
    //     float currentTime = Time.time;
    //     if (cm.synced)
    //     {
    //         foreach (var cube in cm.syncCubes)
    //         {
    //             float elapsedTime = currentTime - startTime + 86f;

    //             // Phase1 左右を見渡す
    //             if (elapsedTime >= 20f && elapsedTime < 20.5f)
    //             {
    //                 turnRight(cube);
    //                 moveShadowRight();
    //             }
    //             else if (elapsedTime >= 21.5f && elapsedTime < 22f)
    //             {
    //                 turnLeft(cube);
    //                 moveShadowLeft();
    //             }
    //             else if (elapsedTime >= 23f && elapsedTime < 23.5f)
    //             {
    //                 turnLeft(cube);
    //                 moveShadowLeft();
    //             }
    //             else if (elapsedTime >= 24.5f && elapsedTime < 25f)
    //             {
    //                 turnRight(cube);
    //                 moveShadowRight();
    //             }
    //             else if (elapsedTime >= 26f && elapsedTime < 35.2f)
    //             {
    //                 moveForward(cube);
    //                 moveShadowForward(0.0306f);
    //             }

    //             //phase2 右を向き看板を確認
    //             else if (elapsedTime >= 36f && elapsedTime < 36.5f)
    //             {
    //                 turnRight(cube);
    //                 moveShadowRight();
    //             }

    //             //phase3 前を向き次の看板まで移動
    //             else if (elapsedTime >= 40.5f && elapsedTime < 41f)
    //             {
    //                 turnLeft(cube);
    //                 moveShadowLeft();
    //             }

    //             else if (elapsedTime >= 42f && elapsedTime < 46.5f)
    //             {
    //                 moveForward(cube);
    //                 moveShadowForward(0.0306f);
    //             }

    //             //phase4 左を向き看板を確認
    //             else if (elapsedTime >= 47.5f && elapsedTime < 48f)
    //             {
    //                 turnLeft(cube);
    //                 moveShadowLeft();
    //             }

    //             //phase5 左右を見渡す
    //             else if (elapsedTime >= 50f && elapsedTime < 50.5f)
    //             {
    //                 turnRight(cube);
    //                 moveShadowRight();
    //             }
    //             else if (elapsedTime >= 51f && elapsedTime < 52f)
    //             {
    //                 turnLeft(cube);
    //                 moveShadowLeft();
    //             }
    //             else if (elapsedTime >= 52.5f && elapsedTime < 53f)
    //             {
    //                 turnRight(cube);
    //                 moveShadowRight();
    //             }
    //             //phase7 影のみ左右を見渡す
    //             else if (elapsedTime >= 54f && elapsedTime < 54.5f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowRight();
    //             }
    //             else if (elapsedTime >= 55f && elapsedTime < 56f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowLeft();
    //             }
    //             else if (elapsedTime >= 56.5f && elapsedTime < 57f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowRight();
    //             }
    //             //phase7 影だけが動き出す
    //             else if (elapsedTime >= 59f && elapsedTime < 60f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowRight();
    //             }
    //             else if (elapsedTime >= 60f && elapsedTime < 62.5f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowForward(0.0306f);
    //             }
    //             // phase8 toioと影が別々の方向に動き出す
    //             else if (elapsedTime >= 62.5f && elapsedTime < 63f)
    //             {
    //                 turnLeft(cube);
    //                 moveShadowForward(0.0306f);
    //             }
    //             else if (elapsedTime >= 63f && elapsedTime < 64f)
    //             {
    //                 moveForward(cube);
    //             }

    //             // phase9 影だけが右に曲がり前に動く
    //             else if (elapsedTime >= 64f && elapsedTime < 65f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowRight();
    //             }

    //             else if (elapsedTime >= 65f && elapsedTime < 66f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowForward(0.0306f);
    //             }

    //             // phase10 影に追いつこうとする
    //             else if (elapsedTime >= 67f && elapsedTime < 68f)
    //             {
    //                 turnRight(cube);
    //                 moveShadowForward(0.0306f);
    //             }
    //             else if (elapsedTime >= 68.5f && elapsedTime < 69.5f)
    //             {
    //                 turnLeft(cube);
    //                 moveShadowForward(0.0306f);
    //             }
    //             else if (elapsedTime >= 70f && elapsedTime < 71.6f)
    //             {
    //                 turnRight(cube);
    //             }
    //             else if (elapsedTime >= 72f && elapsedTime < 75f)
    //             {
    //                 moveForward(cube);
    //             }

    //             // phase 11 影に追いつこうとする
    //             else if (elapsedTime >= 80f && elapsedTime < 80.5f)
    //             {
    //                 turnRight(cube);
    //             }
    //             else if (elapsedTime >= 81f && elapsedTime < 82f)
    //             {
    //                 turnLeft(cube);
    //             }
    //             else if (elapsedTime >= 82.5f && elapsedTime < 83.75f)
    //             {
    //                 turnRight(cube);
    //             }
    //             else if (elapsedTime >= 83.5f && elapsedTime < 86f)
    //             {
    //                 moveForward(cube);
    //             }

    //             // phase 12 影が自由に動き、toioがそれについていく
    //             else if (elapsedTime >= 88f && elapsedTime < 89.3f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowForward(0.05f);
    //             }
    //             else if (elapsedTime >= 89.5f && elapsedTime < 90f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowRight();
    //             }
    //             else if (elapsedTime >= 90f && elapsedTime < 92.5f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowForward(0.05f);
    //             }
    //             else if (elapsedTime >= 92.5f && elapsedTime < 93.3f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowRight();
    //             }
    //             else if (elapsedTime >= 94f && elapsedTime < 99f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowForward(0.05f);
    //             }
    //             else if (elapsedTime >= 101f && elapsedTime < 101.7f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowLeft();
    //             }
    //             else if (elapsedTime >= 102f && elapsedTime < 103f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowForward(0.05f);
    //             }
    //             else if (elapsedTime >= 103f && elapsedTime < 103.5f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowLeft();
    //             }
    //             else if (elapsedTime >= 103.5f && elapsedTime < 107.5f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowForward(0.05f);
    //             }
    //             else if (elapsedTime >= 107.5f && elapsedTime < 108f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowLeft();
    //             }
    //             else if (elapsedTime >= 108f && elapsedTime < 110f)
    //             {
    //                 toioStop(cube);
    //                 moveShadowForward(0.05f);
    //             }

    //         }
    //     }
    // }

    // void moveForward(Cube cube)
    // {
    //     cube.Move(12, 12, 100);
    // }
    // void turnRight(Cube cube)
    // {
    //     cube.Move(8, -8, 100);
    // }

    // void turnLeft(Cube cube)
    // {
    //     cube.Move(-8, 8, 100);
    // }

    // void toioStop(Cube cube)
    // {
    //     cube.Move(0, 0, 0);
    // }
}
