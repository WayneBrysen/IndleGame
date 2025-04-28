using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("玩家状态")]
    public int  maxHealth        = 100;
    [HideInInspector] public int  currentHealth;
    [HideInInspector] public int  energy           = 3;

    [Header("捕捉设置")]
    [Range(0f,1f)]
    public float captureThreshold = 0.3f;

    [Header("已捕获灵兽")]
    public List<string> capturedSpirits = new List<string>();

    [Header("拥有的灵兽")]
    public List<SpiritBeast> beasts = new List<SpiritBeast>();

    [Header("卡组与手牌")]
    [HideInInspector] public List<Card> deck = new List<Card>();
    [HideInInspector] public List<Card> hand = new List<Card>();

    [Header("动画组件")]
    public Animator animator;  // 拖入玩家精灵的 Animator

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"[玩家] 战斗开始——生命 {currentHealth}/{maxHealth}，能量 {energy}");

        // 根据灵兽汇总技能卡生成卡组
        deck.Clear();
        foreach (var beast in beasts)
        {
            Debug.Log($"[玩家] 添加灵兽 {beast.beastName} 的 {beast.skills.Count} 张技能卡");
            foreach (var skill in beast.skills)
                deck.Add(skill);
        }
        Debug.Log($"[玩家] 卡组总数：{deck.Count} 张");

        DrawCards(3);
    }

    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (deck.Count == 0)
            {
                Debug.Log("[玩家] 牌库已空，无法抽牌");
                return;
            }
            int idx = Random.Range(0, deck.Count);
            var c = deck[idx];
            deck.RemoveAt(idx);
            hand.Add(c);
            Debug.Log($"[玩家] 抽牌：{c.cardName}（{c.cardType}），牌组剩余 {deck.Count}");
        }
    }

    /// <summary>
    /// UIManager 点击卡牌按钮时调用
    /// </summary>
    public void PlayCard(int handIndex, Enemy enemy)
    {
        if (handIndex < 0 || handIndex >= hand.Count) return;
        var c = hand[handIndex];
        hand.RemoveAt(handIndex);

        // 播放玩家攻击动画（仅攻击卡）
        if (c.cardType == CardType.Attack && animator != null)
            animator.SetTrigger("attack");

        // 执行卡牌效果
        c.Play(this, enemy);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth = Mathf.Max(currentHealth - dmg, 0);
        Debug.Log($"[玩家] 受到了 {dmg} 点伤害，当前生命 {currentHealth}/{maxHealth}");
    }

    public void OnCaptureSuccess(Enemy enemy)
    {
        capturedSpirits.Add(enemy.spiritName);
        BattleManager.Instance.OnBattleEnd(true);
    }
}
