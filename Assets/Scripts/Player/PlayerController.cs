using UnityEngine;

// 建议添加 RequireComponent 特性，确保 GameObject 上总有 CharacterController
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    // public float jumpForce = 8.0f; // 跳跃需要单独实现逻辑
    public float gravity = -19.62f; // 重力加速度 (可以调整, 通常是 -9.81 * 2)
    public Animator animator;

    // 移除了 Rigidbody 相关的变量 (rb)
    // 移除了地面检测相关的变量 (groundCheck, groundRadius, groundLayer, isGrounded)
    // CharacterController 自带 isGrounded

    private CharacterController controller;
    private Vector3 inputDir;
    private Vector3 playerVelocity; // 用于存储和累加垂直速度（重力）
    private string currentDirection = "Front";
    private bool isMoving = false; // 辅助判断是否正在移动

    void Start()
    {
        // 获取 CharacterController 组件
        controller = GetComponent<CharacterController>();
        // 确保 Animator 被正确赋值 (可以在 Inspector 中拖拽，或者 GetComponent)
        if (animator == null)
            animator = GetComponentInChildren<Animator>(); // 如果 Animator 在子对象上
    }

    void Update()
    {
        // --- 地面检测 ---
        // 使用 CharacterController 内置的 isGrounded
        bool isGrounded = controller.isGrounded;

        // 如果在地面上且垂直速度为负 (表示正在下落或静止)，重置垂直速度
        // 给一个小的负值可以帮助更好地贴合地面
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // --- 输入处理 ---
        // 重置输入方向和移动状态
        inputDir = Vector3.zero;
        isMoving = false;

        // 使用 GetAxisRaw 获取更灵敏的输入 (-1, 0, 1)
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // A/D 或左右箭头
        float verticalInput = Input.GetAxisRaw("Vertical");     // W/S 或上下箭头

        if (Mathf.Abs(verticalInput) > 0.1f) // 优先处理前后移动
        {
            inputDir.z = Mathf.Sign(verticalInput);
            isMoving = true;
            currentDirection = (verticalInput > 0) ? "Front" : "Back";
        }
        else if (Mathf.Abs(horizontalInput) > 0.1f) // 处理左右移动
        {
            inputDir.x = Mathf.Sign(horizontalInput);
            isMoving = true;
            currentDirection = "Side";
            Flip(horizontalInput); // 调用翻转函数
        }

        // --- 计算移动向量 ---
        // 使用 normalized 确保斜向移动速度和直线移动速度一致
        Vector3 move = new Vector3(inputDir.x, 0, inputDir.z).normalized;

        // --- 应用水平移动 ---
        controller.Move(move * moveSpeed * Time.deltaTime);

        // --- 应用重力 ---
        // 持续将重力加速度累加到垂直速度上
        playerVelocity.y += gravity * Time.deltaTime;
        // 应用包含重力的垂直移动
        controller.Move(playerVelocity * Time.deltaTime);

        // --- 动画处理 ---
        UpdateAnimationState(move.magnitude > 0.1f); // 根据是否移动更新动画
    }

    // 角色左右翻转
    void Flip(float horizontalInput)
    {
        Vector3 scale = transform.localScale;
        // 根据输入方向设置 X 轴的缩放符号 (正或负)
        scale.x = Mathf.Sign(horizontalInput) * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    // 更新动画状态 (建议使用 Animator Parameters)
    void UpdateAnimationState(bool moving)
    {
        if (!animator) return; // 如果没有 Animator 组件，则不执行

        // --- 强烈建议使用 Animator Parameters 而不是 Play ---
        // 假设你在 Animator Controller 中设置了以下参数：
        // - "Speed" (Float): 控制移动速度，用于 Idle 和 Walk/Run 之间的过渡
        // - "Direction" (Integer) or specific Bools: 控制朝向 (例如 0:Front, 1:Back, 2:Side)
        // - "IsMoving" (Bool) : 可以用来简化 Idle/Walk 判断

        // 示例 (使用 Play，但注释说明了参数方法)
        if (moving)
        {
            // animator.SetBool("IsMoving", true); // 参数方法
            // animator.SetFloat("Speed", moveSpeed); // 可以传递实际速度，而不仅仅是移动状态

            switch (currentDirection)
            {
                case "Front":
                    // animator.SetInteger("Direction", 0); // 参数方法
                    animator.Play("HeroWalkFront");
                    break;
                case "Back":
                    // animator.SetInteger("Direction", 1); // 参数方法
                    animator.Play("HeroWalkBack");
                    break;
                case "Side":
                    // animator.SetInteger("Direction", 2); // 参数方法
                    animator.Play("HeroWalkSide");
                    break;
            }
        }
        else
        {
            // animator.SetBool("IsMoving", false); // 参数方法
            // animator.SetFloat("Speed", 0f); // 参数方法

            switch (currentDirection)
            {
                case "Front":
                    // animator.SetInteger("Direction", 0); // 参数方法
                    animator.Play("HeroIdleFront");
                    break;
                case "Back":
                    // animator.SetInteger("Direction", 1); // 参数方法
                    animator.Play("HeroIdleBack");
                    break;
                case "Side":
                    // animator.SetInteger("Direction", 2); // 参数方法
                    animator.Play("HeroIdleSide");
                    break;
            }
        }
    }
}