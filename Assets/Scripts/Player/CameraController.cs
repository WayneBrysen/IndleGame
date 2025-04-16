using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // ���Ŀ��
    public float distance = 10f; // ����������ɫ�ľ��루Z����
    public float height = 5f; // ������ĸ߶ȣ�Y����
    public float smoothing = 5f; // ����ƽ����
    public Vector3 tiltEulerAngles = new Vector3(45f, 0f, 0f); // ��������ӽǶȣ���б��

    void Start()
    {
        // �����������ʼ����
        transform.rotation = Quaternion.Euler(tiltEulerAngles);
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // ����Ŀ��λ�ã��ڽ�ɫ���Ϸ���
            Vector3 targetPosition = player.position + new Vector3(0f, height, -distance);

            // ƽ���ƶ������λ��
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}