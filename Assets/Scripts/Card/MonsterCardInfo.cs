using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "CardInfo/Monster")]
public class MonsterCardInfo : CardInfo
{
    private static readonly string MONSTER_CARD_PREFAB = "Prefabs/MonsterCardPrefab";
    private static readonly string MONSTER_CARD_UI_PREFAB = "Prefabs/MonsterCardUI";
    private static readonly string DROP_MONSTER_CARD_UI_PREFAB = "Prefabs/DropMonsterCard";
    private Player player;
    private Hand hand;
    public int damage;
    public int health;

    protected Player GetPlayer()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        return player;
    }

    protected Hand GetHand()
    {
        if (hand == null)
        {
            hand = FindObjectOfType<Hand>();
        }

        return hand;
    }

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

    public virtual void Engage(EffectInput input)
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

    public override CardUI GetDropCardUI()
    {
        var cardUI = Resources.Load<MonsterCardUI>(DROP_MONSTER_CARD_UI_PREFAB);
        return cardUI;
    }

    public void TriggerHandMonsterEngage(MonsterCard randomHandMonster, EffectInput input)
    {
        var randomHandMonsterCardInfo = (MonsterCardInfo)randomHandMonster.GetCardInfo();
        input.monster = null;
        input.card = randomHandMonster;
        input.position = randomHandMonster.transform.position;
        randomHandMonsterCardInfo.Engage(input);
    }
}
