using UnityEngine;

public class RandomYRotation : MonoBehaviour
{
    // 在物体实例化时执行
    void Start()
    {
        Debug.Log("物体已实例化并准备旋转");

        int[] rotationAngles = new int[] { -360, -180, -90, 0, 90, 180, 360 };
        int randomRotation = rotationAngles[Random.Range(0, rotationAngles.Length)];

        Debug.Log("随机旋转角度: " + randomRotation);
        transform.Rotate(0, randomRotation, 0);
    }
}
