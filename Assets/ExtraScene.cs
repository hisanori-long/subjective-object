using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toio;

public class ExtraScene : MonoBehaviour
{
    public GameObject shadow;

    private ShadowScript shadowscript;

    public int moveSpeed = 1500; // キューブの移動速度（スピード）

    public float moveSpeedShadow = 5f;
    private float startTime; // ゲーム開始時の時間

    public ConnectType connectType;
    CubeManager cm;

    async void Start()
    {
        shadowscript = shadow.GetComponent<ShadowScript>();

        cm = new CubeManager(connectType);
        await cm.MultiConnect(1);
        startTime = Time.time; // ゲーム開始時の時間を記録

    }
    void Update()
    {
        float currentTime = Time.time;
        if (cm.synced)
        {
            foreach (var cube in cm.syncCubes)
            {
                float elapsedTime = currentTime - startTime + 48f;

                // Phase1 左右を見渡す
                if (elapsedTime >= 20f && elapsedTime < 20.5f)
                {
                    turnRight(cube);
                    moveShadowRight();
                }
                else if (elapsedTime >= 21.5f && elapsedTime < 22f)
                {
                    turnLeft(cube);
                    moveShadowLeft();
                }
                else if (elapsedTime >= 23f && elapsedTime < 23.5f)
                {
                    turnLeft(cube);
                    moveShadowLeft();
                }
                else if (elapsedTime >= 24.5f && elapsedTime < 25f)
                {
                    turnRight(cube);
                    moveShadowRight();
                }
                else if (elapsedTime >= 26f && elapsedTime < 35.2f)
                {
                    moveForward(cube);
                    moveShadowForward(0.0306f);
                }

                //phase2 右を向き看板を確認
                else if (elapsedTime >= 36f && elapsedTime < 36.5f)
                {
                    turnRight(cube);
                    moveShadowRight();
                }

                //phase3 前を向き次の看板まで移動
                else if (elapsedTime >= 40.5f && elapsedTime < 41f)
                {
                    turnLeft(cube);
                    moveShadowLeft();
                }

                else if (elapsedTime >= 42f && elapsedTime < 46.5f)
                {
                    moveForward(cube);
                    moveShadowForward(0.0306f);
                }

                //phase4 左を向き看板を確認
                else if (elapsedTime >= 47.5f && elapsedTime < 48f)
                {
                    turnLeft(cube);
                    moveShadowLeft();
                }

                //phase5 左右を見渡す
                else if (elapsedTime >= 50f && elapsedTime < 50.5f)
                {
                    turnRight(cube);
                    moveShadowRight();
                }
                else if (elapsedTime >= 51f && elapsedTime < 52f)
                {
                    turnLeft(cube);
                    moveShadowLeft();
                }
                else if (elapsedTime >= 52.5f && elapsedTime < 53f)
                {
                    turnRight(cube);
                    moveShadowRight();
                }
                //phase7 影のみ左右を見渡す
                else if (elapsedTime >= 54f && elapsedTime < 54.5f)
                {
                    toioStop(cube);
                    moveShadowRight();
                }
                else if (elapsedTime >= 55f && elapsedTime < 56f)
                {
                    toioStop(cube);
                    moveShadowLeft();
                }
                else if (elapsedTime >= 56.5f && elapsedTime < 57f)
                {
                    toioStop(cube);
                    moveShadowRight();
                }
                //phase7 影だけが動き出す
                else if (elapsedTime >= 59f && elapsedTime < 60f)
                {
                    toioStop(cube);
                    moveShadowRight();
                }
                else if (elapsedTime >= 60f && elapsedTime < 62.5f)
                {
                    toioStop(cube);
                    moveShadowForward(0.0306f);
                }
                // phase8 toioと影が別々の方向に動き出す
                else if (elapsedTime >= 62.5f && elapsedTime < 63f)
                {
                    turnLeft(cube);
                    moveShadowForward(0.0306f);
                }
                else if (elapsedTime >= 63f && elapsedTime < 63.5f)
                {
                    moveForward(cube);
                }

                // phase9 影だけが右に曲がり前に動く
                else if (elapsedTime >= 64f && elapsedTime < 65f)
                {
                    toioStop(cube);
                    moveShadowRight();
                }

                else if (elapsedTime >= 65f && elapsedTime < 66f)
                {
                    toioStop(cube);
                    moveShadowForward(0.0306f);
                }

                // else if (elapsedTime >= 50f && elapsedTime < 50.5f)
                // {
                //     turnLeft(cube);
                //     moveShadowRight();
                // }

                // else if (elapsedTime >= 50.5f && elapsedTime < 51f)
                // {
                //     //moveForward(cube);
                //     moveShadowRight();
                // }

                // else if (elapsedTime >= 51f && elapsedTime < 51.5f)
                // {
                //     moveForward(cube);
                // }

                // else if (elapsedTime >= 51.5f && elapsedTime < 52f)
                // {

                //     moveForward(cube);
                //     moveShadowForward(0.0306f);
                // }

                // else if (elapsedTime >= 52f && elapsedTime < 53f)
                // {
                //     moveShadowForward(0.0306f);
                // }


            }
        }
    }

    void moveForward(Cube cube)
    {
        cube.Move(12, 12, 100);
    }
    void turnRight(Cube cube)
    {
        cube.Move(8, -8, 100);
    }

    void turnLeft(Cube cube)
    {
        cube.Move(-8, 8, 100);
    }

    void toioStop(Cube cube)
    {
        cube.Move(0, 0, 0);
    }

    void moveShadowForward(float speed)
    {
        // shadowオブジェクトのTransformコンポーネントを取得し、前進させる
        shadow.transform.Translate(0f, 0f, -speed, Space.Self);
    }

    void moveShadowRight()
    {
        // shadowオブジェクトのTransformコンポーネントを取得し、右回転させる
        shadow.transform.Rotate(new Vector3(0, -4.4f, 0));
    }

    void moveShadowLeft()
    {
        // shadowオブジェクトのTransformコンポーネントを取得し、左回転させる
        shadow.transform.Rotate(new Vector3(0, 4.4f, 0));
    }
}
