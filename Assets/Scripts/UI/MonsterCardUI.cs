using UnityEngine;

public class MonsterCardUI : CardUI
{
    [SerializeField] private BasicUI damage;
    [SerializeField] private BasicUI health;

    public BasicUI GetDamageUI() { return damage; }

    public BasicUI GetHealthUI() { return health; }

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        var monsterCardInfo = (MonsterCardInfo)newCardInfo;
        SetDamage(monsterCardInfo.damage);
        SetHealth(monsterCardInfo.health);
        base.SetCardInfo(newCardInfo);
    }

    private void SetDamage(int cardDamage)
    {
        if (damage == null)
        {
            return;
        }

        damage.Display(cardDamage);
    }

    private void SetHealth(int cardHealth)
    {
        if (health == null)
        {
            return;
        }

        health.Display(cardHealth);
    }
}
