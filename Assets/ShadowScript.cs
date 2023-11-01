using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    public float moveSpeed = 5f; // オブジェクトの移動速度

    void Start()
    {
    }

    void Update()
    {
        // 前進
        // transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        // 右回転
        // transform.Rotate(Vector3.up, 60f * Time.deltaTime);
        // 左回転
        // transform.Rotate(Vector3.up, -60f * Time.deltaTime);
    }

    public void MoveForward()
    {
        Debug.Log("MoveForward");
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void Log()
    {
        Debug.Log("Hello World");
    }
}
