using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public Animator animator;
    public Camera mainCamera;

    private Vector2 movement;

    void Update()
    {
        // ��ȡ����
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // ���ö�������
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // ���÷���
        if (movement != Vector2.zero)
        {
            animator.SetFloat("LastMoveX", movement.x);
            animator.SetFloat("LastMoveY", movement.y);
        }
    }

    void FixedUpdate()
    {
        // �ƶ���ɫ
        transform.position += new Vector3(movement.x, movement.y, 0f) * moveSpeed * Time.fixedDeltaTime;

        // ���������
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }
}
