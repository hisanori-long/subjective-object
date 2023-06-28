using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toio;

public class ExtraScene : MonoBehaviour
{
    public GameObject shadowObject;
    public ConnectType connectType;

    CubeManager cm;
    float intervalTime = 7f;
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

        // shadowScript = shadowObject.GetComponent<ShadowScript>();
    }


    void Update()
    {
        // if (intervalTime < elapsedTime) // 2秒ごとに実行
        // {
            // bool shadowStopped = shadowScript.isStopped;
            // Debug.Log("isStopped");
            // Debug.Log(shadowStopped);

            // if(shadowStopped) {
            //     Transform shadowPosition = shadowObject.transform;
            //     Debug.Log("position-x:");
            //     Debug.Log(shadowPosition.position.x);
            // }
        // }

        // bool shadowStopped = shadowScript.isStopped;

        // if(shadowStopped) {
        //     Transform shadowPosition = shadowObject.transform;
        //     Debug.Log("position-x:");
        //     Debug.Log(shadowPosition.position.x);
        // }

         if (cm.synced)
        {

            foreach(var cube in cm.syncCubes)
            {

                elapsedTime += Time.deltaTime;

                if (intervalTime < elapsedTime) // 2秒ごとに実行
                {
                    if (phase == 0)
                    {
                        cube.Move(30, 30, 1760);
                    }
                    else if (phase == 1)
                    {
                        cube.Move(-30, -30, 1760);
                        phase = -1;
                    }

                    elapsedTime = 0.0f;
                    phase += 1;
                }
            }


            
        }
    }
}
