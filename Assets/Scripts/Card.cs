using UnityEngine;

// 简单卡牌：具有名称、伤害、能量消耗，并提供 Play 方法
public class Card : MonoBehaviour
{
    [Header("卡牌属性")]
    public string cardName = "攻击卡";
    public int damage = 20;
    public int cost = 1;

    // 打出卡牌的方法，传入玩家和目标敌人
    public void Play(Player player, Enemy enemy)
    {
        if (player.energy >= cost)
        {
            player.energy -= cost;
            enemy.TakeDamage(damage);
            Debug.Log($"{cardName} 被打出，造成 {damage} 点伤害。");
        }
        else
        {
            Debug.Log("能量不足，无法使用卡牌！");
        }
    }
}
