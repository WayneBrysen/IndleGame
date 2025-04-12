using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("玩家状态")]
    public int maxHealth = 100;
    public int currentHealth;
    public int energy = 3;

    [Header("灵兽系统")]
    // 玩家拥有的灵兽列表（3只灵兽）
    public List<SpiritBeast> spiritBeasts = new List<SpiritBeast>();

    [Header("卡牌系统")]
    // 卡组：将所有灵兽技能卡汇集到一起，共 9 张卡
    public List<Card> deck = new List<Card>();
    // 手牌：玩家当前可用的卡牌
    public List<Card> hand = new List<Card>();

    void Start()
    {
        currentHealth = maxHealth;

        // 从所有灵兽中加载技能卡，合成卡组
        foreach (SpiritBeast beast in spiritBeasts)
        {
            foreach (Card card in beast.GetSkills())
            {
                deck.Add(card);
            }
        }
        Debug.Log("合成卡组，共 " + deck.Count + " 张卡牌。");

        // 开局抽 3 张卡牌
        DrawCards(3);
    }

    // 从卡组中随机抽取指定数量卡牌加入手牌
    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (deck.Count > 0)
            {
                int index = Random.Range(0, deck.Count);
                Card card = deck[index];
                hand.Add(card);
                deck.RemoveAt(index);  // 从牌库移除该卡牌
                Debug.Log($"抽到卡牌：{card.cardName}，剩余牌库数量：{deck.Count}");
            }
            else
            {
                Debug.Log("牌库为空！");
            }
        }
    }


    // 使用手牌中的卡牌（根据索引）对敌人使用
    public void PlayCard(int handIndex, Enemy enemy)
    {
        if (handIndex >= 0 && handIndex < hand.Count)
        {
            Card card = hand[handIndex];
            card.Play(this, enemy);
            hand.RemoveAt(handIndex);
        }
        else
        {
            Debug.Log("无效的手牌索引！");
        }
    }

    // 玩家受到伤害
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("玩家阵亡！");
        }
        else
        {
            Debug.Log($"玩家受到 {amount} 点伤害，剩余生命：{currentHealth}");
        }
    }
}
