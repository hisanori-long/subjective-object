using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toio;

public class ExtraScene : MonoBehaviour
{
    public GameObject shadowObject;
    public ConnectType connectType;

    CubeManager cm;
    float intervalTime = 4f;
    float elapsedTime = 0;
    int phase = 0;
    ShadowScript shadowScript; // ShadowScriptへの参照

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
         if (cm.synced)
        {

            foreach(var cube in cm.syncCubes)
            {

                elapsedTime += Time.deltaTime;

                if (intervalTime < elapsedTime) // 2秒ごとに実行
                {
                    if (phase == 0)
                    {
                        cube.Move(10, 10, 2000);
                    }
                    else if (phase == 1)
                    {
                        // cube.Move(0, 0, 2000);
                    }
                    else if (phase == 2)
                    {
                        cube.Move(-10, -10, 2000);
                    }
                    else if (phase == 3)
                    {
                        // cube.Move(0, 0, 2000);
                        phase = -1;
                    }

                    elapsedTime = 0.0f;
                    phase += 1;
                }
            }


            
        }
    }
}
