using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public Animator animator;

    private Vector3 movement;
    private string currentDirection = "Front";

    void Update()
    {
        movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement.z = 1;
            currentDirection = "Front";
            animator.Play("HeroWalkFront");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement.z = -1;
            currentDirection = "Back";
            animator.Play("HeroWalkBack");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
            currentDirection = "Side";

            // 保留原始缩放，仅翻转 X 轴
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;

            animator.Play("HeroWalkSide");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
            currentDirection = "Side";

            // 保留原始缩放，仅翻转 X 轴
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;

            animator.Play("HeroWalkSide");
        }
        else
        {
            switch (currentDirection)
            {
                case "Front": animator.Play("HeroIdleFront"); break;
                case "Back": animator.Play("HeroIdleBack"); break;
                case "Side": animator.Play("HeroIdleSide"); break;
            }
        }

        // 移动角色
        GetComponent<Rigidbody>().MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
    }
}