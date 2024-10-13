using System.Collections.Generic;
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

    public virtual void Global(Monster self)
    {
    }

    public virtual void Entrance(Monster self)
    {
    }

    public virtual void Engage(Monster characterSelf, Card cardSelf)
    {
    }

    public virtual void Exit(Monster self)
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
