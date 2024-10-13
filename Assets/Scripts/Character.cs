using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Health health;
    protected Damage damage;

    public virtual void Awake() { }

    public Damage GetDamageComponent()
    {
        if (damage == null)
        {
            damage = GetComponent<Damage>();
        }

        return damage;
    }

    public Health GetHealthComponent()
    {
        if (health == null)
        {
            health = GetComponent<Health>();
        }

        return health;
    }

    public void ResetStats()
    {
        GetDamageComponent().ResetDamage();
        GetHealthComponent().RestoreFullHealth();
    }

    public bool IsDead() { return GetHealthComponent().GetCurrentHealth() <= 0; }

    public void TakeDamage(int damage) { GetHealthComponent().TakeDamage(damage); }

    public void TakeDamage(MonsterCardInfo monsterCardInfo) { GetHealthComponent().TakeDamage(monsterCardInfo.damage); }

    public void TakeDamage(MonsterCard monsterCard) { TakeDamage((MonsterCardInfo)monsterCard.GetCardInfo()); }

    public void RestoreHealth(int damage) { GetHealthComponent().RestoreHealth(damage); }

    public int GetDamage() { return GetDamageComponent().GetDamage(); }

    public int GetHealth() { return GetHealthComponent().GetCount(); }

    public void IncreaseDamage(int increase) { GetDamageComponent().IncreaseMaxDamage(increase); }

    public void ReduceDamage(int decrease) { GetDamageComponent().DecreaseMaxDamage(decrease); }

    public void TemporaryIncreaseStats(int damage, int health)
    {
        GetDamageComponent().TemporaryIncreaseDamage(damage);
        GetHealthComponent().TemporaryIncreaseHealth(health);
    }

    public void TemporaryDecreaseStats(int damage, int health)
    {
        GetDamageComponent().TemporaryDecreaseDamage(damage);
        GetHealthComponent().TemporaryDecreaseHealth(health);
    }

    public virtual IEnumerator MakeAttack(Character character) { yield return null; }
}
