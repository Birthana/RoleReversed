using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "CardInfo/Monster")]
public class MonsterCardInfo : CardInfo
{
    private static readonly string MONSTER_CARD_PREFAB = "Prefabs/MonsterCardPrefab";
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

    public virtual void Engage(Character self)
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
}
