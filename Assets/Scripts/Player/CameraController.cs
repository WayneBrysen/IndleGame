using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // 玩家目标
    public float distance = 10f; // 摄像机距离角色的距离（Z方向）
    public float height = 5f; // 摄像机的高度（Y方向）
    public float smoothing = 5f; // 跟随平滑度
    public Vector3 tiltEulerAngles = new Vector3(45f, 0f, 0f); // 摄像机俯视角度（倾斜）

    void Start()
    {
        // 设置摄像机初始朝向
        transform.rotation = Quaternion.Euler(tiltEulerAngles);
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // 计算目标位置（在角色后上方）
            Vector3 targetPosition = player.position + new Vector3(0f, height, -distance);

            // 平滑移动摄像机位置
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}