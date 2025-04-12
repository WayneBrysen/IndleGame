using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public enum Turn { PlayerTurn, EnemyTurn }
    public Turn currentTurn = Turn.PlayerTurn;

    public Player player;   // 在 Inspector 中将 Player 对象拖入
    public Enemy enemy;     // 在 Inspector 中将 Enemy 对象拖入

    void Update()
    {
        // 玩家回合：按数字键 1 使用手牌中第 1 张卡牌
        if (currentTurn == Turn.PlayerTurn)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (player.hand.Count > 0)
                {
                    player.PlayCard(0, enemy);
                    if (enemy.currentHealth <= 0)
                    {
                        Debug.Log("战斗结束：敌人被击败！");
                        return;
                    }
                    StartCoroutine(EnemyTurn());
                }
                else
                {
                    Debug.Log("手牌为空，请抽牌！");
                }
            }
        }
    }

    IEnumerator EnemyTurn()
    {
        currentTurn = Turn.EnemyTurn;
        yield return new WaitForSeconds(1f); // 敌人回合延时 1 秒
        enemy.Attack(player);
        if (player.currentHealth <= 0)
        {
            Debug.Log("战斗结束：玩家阵亡！");
            yield break;
        }
        // 每回合结束时，重置玩家能量，并抽一张卡
        player.energy = 3;
        player.DrawCards(1);
        currentTurn = Turn.PlayerTurn;
    }
}
