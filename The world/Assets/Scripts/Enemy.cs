using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("敌人状态")]
    public int maxHealth = 50;
    public int currentHealth;
    public int attackDamage = 10;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // 敌人受到伤害
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("敌人阵亡！");
        }
        else
        {
            Debug.Log($"敌人受到 {damage} 点伤害，剩余生命：{currentHealth}");
        }
    }

    // 敌人攻击玩家
    public void Attack(Player player)
    {
        player.TakeDamage(attackDamage);
        Debug.Log($"敌人攻击，造成 {attackDamage} 点伤害。");
    }
}
