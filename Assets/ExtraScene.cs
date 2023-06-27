using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toio;

public class ExtraScene : MonoBehaviour
{
    public ConnectType connectType;

    CubeManager cm;
    float intervalTime = 0.5f;
    float elapsedTime = 0;
    int phase = 0;

    async void Start()
    {
        // ConnectType.Auto - ビルド対象に応じて内部実装が自動的に変わる
        // ConnectType.Simulator - ビルド対象に関わらずシミュレータのキューブで動作する
        // ConnectType.Real - ビルド対象に関わらずリアル(現実)のキューブで動作する
        cm = new CubeManager(connectType);
        await cm.MultiConnect(2);
    }


    void Update()
    {

        foreach(var cube in cm.syncCubes)
        {

            elapsedTime += Time.deltaTime;

            if (intervalTime < elapsedTime) // 2秒ごとに実行
            {
                if (phase <= 2)
                {
                    cube.Move(30, 30, 2500);
                }
                else if (phase == 3)
                {
                    cube.Move(0, 0, 1500);
                }
                else if (phase <= 6)
                {
                    cube.Move(-30, -30, 2500);
                }
                else if (phase == 7)
                {
                    cube.Move(0, 0, 1500);
                }
                else if (phase == 8)
                {
                    phase = -1;
                }

                elapsedTime = 0.0f;
                phase += 1;
            }
        }
    }
}
