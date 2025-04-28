using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("顶部状态（TMP）")]
    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI playerEnergyText;
    public TextMeshProUGUI capturedText;
    public TextMeshProUGUI enemyHPText;  // 新增：敌人HP文本引用

    [Header("手牌区 & 预制体")]
    public Transform handPanel;
    public GameObject cardButtonPrefab;

    void Awake()
    {
        Instance = this;
    }

    public void RefreshAll()
    {
        RefreshStatus();
        RefreshHand();
    }

    void RefreshStatus()
    {
        // 取出玩家 & 敌人实例
        var player = BattleManager.Instance.player;
        var enemy  = BattleManager.Instance.enemy;

        // 刷新玩家状态
        playerHPText.text     = $"HP: {player.currentHealth}/{player.maxHealth}";
        playerEnergyText.text = $"Energy: {player.energy}";
        capturedText.text     = "Captured: " +
            (player.capturedSpirits.Count > 0
                ? string.Join(", ", player.capturedSpirits)
                : "—");

        // 刷新敌人HP
        enemyHPText.text      = $"Enemy: {enemy.spiritName}  HP: {enemy.currentHealth}/{enemy.maxHealth}";
    }

    void RefreshHand()
    {
        // 清空旧按钮
        foreach (Transform t in handPanel)
            Destroy(t.gameObject);

        var hand = BattleManager.Instance.player.hand;
        for (int i = 0; i < hand.Count; i++)
        {
            var card = hand[i];
            var go   = Instantiate(cardButtonPrefab, handPanel);
            go.name  = $"CardButton_{i}";

            // 设置文本
            var label = go.GetComponentInChildren<TextMeshProUGUI>();
            label.text = $"{card.cardName}\n<size=28>{card.cardType}</size>\n<size=24>Cost:{card.cost}</size>"
                         + (card.cardType == CardType.Attack
                            ? $"\n<size=24>Dmg:{card.damage}</size>"
                            : "");

            // 点击事件
            int idx = i;
            var btn = go.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                BattleManager.Instance.OnCardButtonClicked(idx);
            });
        }
    }
}
