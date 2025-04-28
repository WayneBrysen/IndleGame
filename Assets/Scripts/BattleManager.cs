using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [Header("引用")]
    public Player player;
    public Enemy enemy;

    public enum Turn { PlayerTurn, EnemyTurn }
    public Turn currentTurn = Turn.PlayerTurn;

    void Awake() => Instance = this;

    void Start()
    {
        UIManager.Instance.RefreshAll();
        Debug.Log("=== 进入战斗区 ===");
    }

    public void OnCardButtonClicked(int handIndex)
    {
        if (currentTurn != Turn.PlayerTurn) return;

        player.PlayCard(handIndex, enemy);
        UIManager.Instance.RefreshAll();

        if (enemy.currentHealth <= 0)
        {
            OnBattleEnd(true);
            return;
        }

        currentTurn = Turn.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(0.5f);
        enemy.Attack(player);
        UIManager.Instance.RefreshAll();

        if (player.currentHealth <= 0)
        {
            OnBattleEnd(false);
            yield break;
        }

        currentTurn = Turn.PlayerTurn;
        player.energy = 3;
        player.DrawCards(1);
        UIManager.Instance.RefreshAll();
    }

    public void OnBattleEnd(bool victory)
    {
        StopAllCoroutines();
        Debug.Log(victory ? "【战斗胜利】" : "【战斗失败】");
    }
}
