using System.Collections.Generic;
using UnityEngine;

public class SpiritBeast : MonoBehaviour
{
    [Header("灵兽名称")]
    public string beastName = "灵兽";

    [Header("灵兽技能卡（3 张）")]
    // 这里定义了 skills 字段，Player 会遍历它来生成 deck
    public List<Card> skills = new List<Card>();
}
