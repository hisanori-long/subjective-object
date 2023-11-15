using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toio;

public class ExtraScene : MonoBehaviour
{
    public GameObject shadow;
    private ShadowScript shadowscript;
    private Vector3 initialPosition; // 影の初期位置
    private Quaternion initialRotation; // 影の初期角度
    private float startTime; // ゲーム開始時の時間
    private float startTimeShadow; // 影のゲーム開始時の時間
    private float phase = 0; // ゲームの進行度
    private float phaseShadow = 0; // 影の進行度
    public Light myLight; // 照明
    public GameObject sign1; //看板オブジェクト
    public GameObject sign2; //看板オブジェクト
    public GameObject sign3; //看板オブジェクト
    public ConnectType connectType;
    CubeManager cm;
    Cube cube;
    float eulersZValue; // キューブの姿勢角度(正が右回転、負が左回転)
    float storedEulersZValue = 1000; // 保存されたキューブの姿勢角度
    float elapsedTime; // 各フェーズの経過時間
    float elapsedTimeShadow; // 影の経過時間    
    private float resetCount = 0; // リセット用のカウント
    public float resetCountMax = 1050; // リセット用のカウントの最大値
    public float fixDirection = -1;
    public float rotationSpeed = 180f;
    public float moveSpeed = 0.0004f;
    private float soundCount = 0; // サウンド用のカウント
    private Quaternion initalSign1Rotation; // 看板の初期角度
    private Quaternion initalSign2Rotation; // 看板の初期角度
    private Quaternion initalSign3Rotation; // 看板の初期角度
    private Vector3 initalSign1Position; // 看板の初期位置
    private Vector3 initalSign2Position; // 看板の初期位置
    private Vector3 initalSign3Position; // 看板の初期位置
    private int changeSign = 0; // 看板の変更用のカウント
    async void Start()
    {
        // shadowオブジェクトのShadowScriptコンポーネントを取得
        shadowscript = shadow.GetComponent<ShadowScript>();

        // キューブマネージャーの初期化・接続
        cm = new CubeManager(connectType);
        cube = await cm.SingleConnect();

        myLight.intensity = 0f; // 照明を消す


        // キューブの接続が完了したら、キューブのイベントを登録
        cube.attitudeCallback.AddListener("EventScene", OnAttitude);
        await cube.ConfigAttitudeSensor(Cube.AttitudeFormat.Eulers, 100, Cube.AttitudeNotificationType.OnChanged);
        await cube.ConfigMagneticSensor(Cube.MagneticMode.MagnetState);

        // 影の位置を記録
        initialPosition = shadow.transform.position;
        initialRotation = shadow.transform.rotation;

        // 看板の方向を記録
        initalSign1Rotation = sign1.transform.rotation;
        initalSign2Rotation = sign2.transform.rotation;
        initalSign3Rotation = sign3.transform.rotation;

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
        else if (phase == 7)
        {
            phase7();
        }
        else if (phase == 8)
        {
            phase8();
        }
        else if (phase == 9)
        {
            phase9();
        }
        else if (phase == 10)
        {
            phase10();
        }
        else if (phase == 11)
        {
            phase11();
        }

        if (phaseShadow == 2)
        {
            phaseShadow1();
        }

        resetCount += 1;
        if (resetCount > resetCountMax)
        {
            storedEulersZValue += fixDirection;
            resetCount = 0;
            Debug.Log($"storedEulersZValue is saved: {storedEulersZValue}");
        }
    }

    void phase0()
    {

        if (elapsedTime >= 5f && elapsedTime < 8f)
        {
            // ライトをゆっくりとつける
            myLight.intensity = (elapsedTime - 5f) / 3f;
        }
        else if (elapsedTime >= 5f)
        {
            // 次のphaseへ
            resetTime();
            phase = 1;
            storedEulersZValue = eulersZValue;
            Debug.Log($"storedEulersZValue is saved: {storedEulersZValue}");
        }
    }
    void phase1()
    {
        if (elapsedTime >= 0f && elapsedTime < 2f)
        {
            toioForward(0);
            shadowForward();
        }
        else if (elapsedTime >= 2f && elapsedTime < 2.5f)
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
        if (elapsedTime >= 2f && elapsedTime < 7f)
        {
            toioForward(0);
            shadowForward();

            if (soundCount == 0)
            {
                // サウンドを鳴らす
                cube.PlayPresetSound(0);
                soundCount = 1;
            }
        }
        else if (elapsedTime >= 9.5f && elapsedTime < 10f)
        {
            toioRight();
            shadowRight();
        }
        else if (elapsedTime >= 12f && elapsedTime < 12.5f)
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
        else if (elapsedTime >= 15f && elapsedTime < 23f)
        {
            // ライトの明るさを0, 1, 0, 1, ...と0.1秒間隔で変化させる
            // if (Mathf.Floor(elapsedTime * 10) % 2 == 0)
            // {
            //     myLight.intensity = 0f;
            // }
            // else
            // {
            //     myLight.intensity = 1f;
            // }
            // ライトの方向を一回転するグローバルで
            myLight.transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0), Space.World);
            // 看板のz軸の角度を最初の角度から180度変化した角度に移動する（グローバルスケール）


        }
        else if (elapsedTime >= 24f && elapsedTime < 24.5f)
        {
            toioStop();
            shadowLeft();
        }
        else if (elapsedTime >= 25f && elapsedTime < 26f)
        {
            toioStop();
            shadowRight();
        }
        else if (elapsedTime >= 26.5f && elapsedTime < 27f)
        {
            toioStop();
            shadowLeft();
        }
        else if (elapsedTime >= 27f)
        {
            // 次のphaseへ
            toioFixDirection(0);
            shadowFixDirection(0);
        }

        if (elapsedTime >= 19f && changeSign == 0)
        {
            sign1.transform.rotation = Quaternion.Euler(sign1.transform.rotation.eulerAngles.x, sign1.transform.rotation.eulerAngles.y, initalSign1Rotation.z + 180);
            sign2.transform.rotation = Quaternion.Euler(sign2.transform.rotation.eulerAngles.x, sign2.transform.rotation.eulerAngles.y, initalSign2Rotation.z + 180);
            sign3.transform.rotation = Quaternion.Euler(sign3.transform.rotation.eulerAngles.x, sign3.transform.rotation.eulerAngles.y, initalSign3Rotation.z + 180);
            // 看板の位置をローカルスケールでx軸方向に移動する
            sign1.transform.Translate(new Vector3(0.08f, 0, 0));
            sign2.transform.Translate(new Vector3(0.08f, 0, 0));
            sign3.transform.Translate(new Vector3(0.08f, 0, 0));
            changeSign = 1;
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
        else if (elapsedTime >= 3.5f && elapsedTime < 4.1f)
        {
            toioLeft();
        }
        else if (elapsedTime >= 4f && elapsedTime < 5f)
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
            if (soundCount == 1)
            {
                // サウンドを鳴らす
                cube.PlayPresetSound(2);
                soundCount = 2;
            }
        }
        else if (elapsedTime >= 8.5f && elapsedTime < 10.4f)
        {
            toioRight();
        }
        else if (elapsedTime >= 12f)
        {
            toioFixDirection(80);
        }
    }
    void phase6()
    {
        if (elapsedTime >= 1f && elapsedTime < 4.5f)
        {
            toioForward(80);
        }
        else if (elapsedTime >= 5f && elapsedTime < 5.8f)
        {
            toioRight();
        }
        else if (elapsedTime >= 6f)
        {
            toioFixDirection(155);
        }
    }
    void phase7()
    {
        if (elapsedTime >= 1f && elapsedTime < 3f)
        {
            shadowForward();
        }
        if (elapsedTime >= 3f && elapsedTime < 6f)
        {
            shadowForward();
            toioForward(155);
        }
        else if (elapsedTime >= 6f && elapsedTime < 6.3f)
        {
            shadowRight();
            toioForward(155);
        }
        else if (elapsedTime >= 6.3f && elapsedTime < 6.5f)
        {
            shadowFixDirection(-180);
            toioForward(155);
        }
        else if (elapsedTime >= 6.5f && elapsedTime < 7f)
        {
            toioForward(155);
        }
        else if (elapsedTime >= 7f && elapsedTime < 8f)
        {
            shadowForward();
            toioForward(155);
        }
        else if (elapsedTime >= 8f && elapsedTime < 9f)
        {
            shadowForward();
        }
        else if (elapsedTime >= 9.5f && elapsedTime < 9.8f)
        {
            toioRight();
            shadowRight();
        }
        else if (elapsedTime >= 9.8f && elapsedTime < 10.3f)
        {
            shadowRight();
        }
        else if (elapsedTime >= 10.5f)
        {
            shadowFixDirection(90);
            toioFixDirection(180);
        }
    }
    void phase8()
    {
        if (elapsedTime >= 1f && elapsedTime < 3f)
        {
            shadowForward();
            toioForward(180);
        }
        else if (elapsedTime >= 3f && elapsedTime < 3.9f)
        {
            shadowForward();
            toioRight();
        }
        else if (elapsedTime >= 3.9f)
        {
            phaseShadow = 1;
            toioFixDirection(-90);
        }
    }
    void phase9()
    {
        if (elapsedTime >= 1f && elapsedTime < 7f)
        {
            toioForward(-90);
        }
        else if (elapsedTime >= 9f && elapsedTime < 9.8f)
        {
            toioLeft();
        }
        else if (elapsedTime >= 9.8f)
        {
            toioFixDirection(-180);
        }
    }
    void phase10()
    {
        if (elapsedTime >= 0f && elapsedTime < 10f)
        {
            toioForward(-180);
        }
        else if (elapsedTime >= 10.5f && elapsedTime < 11.7f)
        {
            toioBack();
        }
        else if (elapsedTime >= 12f && elapsedTime < 12.9f)
        {
            toioRight();
        }
        else if (elapsedTime >= 13f)
        {
            toioFixDirection(-90);
        }

    }
    void phase11()
    {
        if (elapsedTime >= 1f && elapsedTime < 6f)
        {
            toioBack();
        }
        else if (elapsedTime >= 6f && elapsedTime < 6.5f)
        {
            toioForward(-90);
        }
        else if (elapsedTime >= 7f && elapsedTime < 7.5f)
        {
            toioRight();
        }
        else if (elapsedTime >= 8f && elapsedTime < 14f)
        {
            toioLeftBack();
        }
        else if (elapsedTime >= 25f)
        {
            // 最初に戻る
            phase = 0;
            phaseShadow = 0;
            changeSign = 0;
            soundCount = 0;
            shadow.transform.position = initialPosition;
            shadow.transform.rotation = initialRotation;
            sign1.transform.rotation = initalSign1Rotation;
            sign2.transform.rotation = initalSign2Rotation;
            sign3.transform.rotation = initalSign3Rotation;
            resetTime();
        }
    }

    void phaseShadow1()
    {
        elapsedTimeShadow = Time.time - startTimeShadow;

        if (elapsedTimeShadow >= 0f && elapsedTimeShadow < 4.5f)
        {
            shadowForward();
        }
        else if (elapsedTimeShadow >= 6.5f && elapsedTimeShadow < 7.3f)
        {
            shadowLeft();
        }
        else if (elapsedTimeShadow >= 7.3f && elapsedTimeShadow < 7.5f)
        {
            shadowFixDirection(-180);
        }
        else if (elapsedTimeShadow >= 8f && elapsedTimeShadow < 12f)
        {
            shadowForward();
        }
        else if (elapsedTimeShadow >= 12f && elapsedTimeShadow < 15f)
        {
            shadowForward();
            //ゆっくりと光を消す
            myLight.intensity = 1f - (elapsedTimeShadow - 12f) / 3f;
        }
        else if (elapsedTimeShadow >= 15f)
        {
            shadowForward();
        }
    }

    void toioForward(float direction)
    {
        float difEulersZValue = eulersZValue - storedEulersZValue - direction;  // 現在のキューブの姿勢角度
        float absdifEulersZValue = Mathf.Floor(Mathf.Abs(difEulersZValue) / 180);
        Debug.Log($"difEulersZValue: {difEulersZValue}, absdifEulersZValue: {absdifEulersZValue}, difEulersZValue % 360: {difEulersZValue % 360}");

        if (difEulersZValue % 360 == 0)
        {
            cube.Move(12, 12, 100);
        }
        else if (difEulersZValue > 0)
        {
            // 奇数の場合
            if (absdifEulersZValue % 2 == 1)
            {
                // 右回転
                cube.Move(13, 12, 100);
            }
            // 偶数の場合
            else
            {
                // 左回転
                cube.Move(12, 13, 100);
            }
        }
        else if (difEulersZValue < 0)
        {
            // 奇数の場合
            if (absdifEulersZValue % 2 == 1)
            {
                // 左回転
                cube.Move(12, 13, 100);
            }
            // 偶数の場合
            else
            {
                // 右回転
                cube.Move(13, 12, 100);
            }
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

    void toioBack()
    {
        cube.Move(-12, -12, 100);
    }

    void toioLeftBack()
    {
        cube.Move(-8, -12, 100);
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
        Debug.Log($"difEulersZValue: {difEulersZValue}, absdifEulersZValue: {absdifEulersZValue}, difEulersZValue % 360: {difEulersZValue % 360}");
        if (difEulersZValue % 360 == 0)
        {
            phase = phase + 1;
            resetTime();
        }
        else if (difEulersZValue > 0)
        {
            // 奇数の場合
            if (absdifEulersZValue % 2 == 1)
            {
                // 右回転
                cube.Move(0, -9, 20);
            }
            // 偶数の場合
            else
            {
                // 左回転
                cube.Move(-9, 0, 20);
            }
        }
        else if (difEulersZValue < 0)
        {
            // 奇数の場合
            if (absdifEulersZValue % 2 == 1)
            {
                // 左回転
                cube.Move(-9, 0, 20);
            }
            // 偶数の場合
            else
            {
                // 右回転
                cube.Move(0, -9, 20);
            }
        }

        if (phaseShadow == 1)
        {
            startTimeShadow = Time.time;
            phaseShadow = 2;
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
    }
}
