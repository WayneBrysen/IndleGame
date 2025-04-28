using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("敌人信息")]
    public string spiritName = "野生灵兽";
    public int    maxHealth  = 100;
    [HideInInspector] public int currentHealth;
    public int    attackDamage = 10;

    [Header("捕捉阈值")]
    [Range(0f,1f)]
    public float captureThreshold = 0.3f;

    [Header("动画组件")]
    public Animator animator;  // 拖入 Boss 或灵兽的 Animator

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth = Mathf.Max(currentHealth - dmg, 0);
        Debug.Log($"[敌人] 受到了 {dmg} 点伤害，当前生命 {currentHealth}/{maxHealth}");
    }

    public bool TryCapture(float threshold)
    {
        return currentHealth <= maxHealth * threshold;
    }

    /// <summary>
    /// 发动攻击：触发动画并延迟造成伤害
    /// </summary>
    public void Attack(Player player)
    {
        if (animator != null)
            animator.SetTrigger("boss_attack");

        // 延迟 0.3s 再造成伤害，与动画同步
        StartCoroutine(DelayedHit(player, attackDamage));
    }

    IEnumerator DelayedHit(Player player, int dmg)
    {
        yield return new WaitForSeconds(0.3f);
        player.TakeDamage(dmg);
        Debug.Log($"[敌人] 攻击玩家，造成 {dmg} 点伤害，玩家生命 {player.currentHealth}/{player.maxHealth}");
        UIManager.Instance.RefreshAll();
    }
}
