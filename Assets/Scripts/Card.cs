using UnityEngine;

public enum CardType
{
    Attack,
    Capture
}

public class Card : MonoBehaviour
{
    [Header("卡牌属性")]
    public string cardName = "卡牌名称";
    public CardType cardType = CardType.Attack;
    public int cost = 1;
    public int damage = 20; // 仅在攻击卡时生效

    public void Play(Player player, Enemy enemy)
    {
        if (player.energy < cost)
        {
            Debug.Log($"能量不足，无法使用 {cardName}");
            return;
        }
        player.energy -= cost;

        if (cardType == CardType.Attack)
        {
            enemy.TakeDamage(damage);
            Debug.Log($"[{cardName}] 造成 {damage} 点伤害，敌人剩余生命 {enemy.currentHealth}/{enemy.maxHealth}");
        }
        else // Capture
        {
            bool success = enemy.TryCapture(player.captureThreshold);
            if (success)
            {
                player.OnCaptureSuccess(enemy);
                Debug.Log($"[{cardName}] 捕捉成功，获得灵兽：{enemy.spiritName}");
            }
            else
            {
                Debug.Log($"[{cardName}] 捕捉失败，敌人剩余生命 {enemy.currentHealth}/{enemy.maxHealth}");
            }
        }
    }
}
