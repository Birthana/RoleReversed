using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "CardInfo/Monster")]
public class MonsterCardInfo : CardInfo
{
    private static readonly string MONSTER_CARD_PREFAB = "Prefabs/MonsterCardPrefab";
    private static readonly string MONSTER_CARD_UI_PREFAB = "Prefabs/MonsterCardUI";
    public int damage;
    public int health;

    public virtual int GetDamage()
    {
        return damage;
    }

    public virtual int GetHealth()
    {
        return health;
    }

    public virtual void Entrance(Character self)
    {
    }

    public virtual void Engage(Character characterSelf, Card cardSelf)
    {
    }

    public virtual void Exit(Character self)
    {
    }

    public override Card GetCardPrefab()
    {
        var cardPrefab = Resources.Load<MonsterCard>(MONSTER_CARD_PREFAB);
        return cardPrefab;
    }

    public override CardUI GetCardUI()
    {
        var cardUI = Resources.Load<MonsterCardUI>(MONSTER_CARD_UI_PREFAB);
        return cardUI;
    }
}
