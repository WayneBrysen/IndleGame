using UnityEngine;

// 每只灵兽拥有三个技能卡牌
public class SpiritBeast : MonoBehaviour
{
    [Header("灵兽技能卡")]
    public Card skill1;
    public Card skill2;
    public Card skill3;

    // 返回该灵兽所有技能卡牌
    public Card[] GetSkills()
    {
        return new Card[] { skill1, skill2, skill3 };
    }
}
