using UnityEngine;

public class RandomYRotation : MonoBehaviour
{
    // ������ʵ����ʱִ��
    void Start()
    {
        Debug.Log("������ʵ������׼����ת");

        int[] rotationAngles = new int[] { -360, -180, -90, 0, 90, 180, 360 };
        int randomRotation = rotationAngles[Random.Range(0, rotationAngles.Length)];

        Debug.Log("�����ת�Ƕ�: " + randomRotation);
        transform.Rotate(0, randomRotation, 0);
    }
}
