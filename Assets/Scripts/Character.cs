using UnityEngine;

[RequireComponent(typeof(Damage))]
[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour
{
    protected Health health;
    protected Damage damage;

    public virtual void Awake()
    {
        damage = GetComponent<Damage>();
        health = GetComponent<Health>();
    }

    public void ResetStats()
    {
        damage.ResetDamage();
        health.RestoreFullHealth();
    }

    public bool IsDead() { return health.GetCurrentHealth() == 0; }

    public void TakeDamage(int damage) { health.TakeDamage(damage); }

    public int GetDamage() { return damage.GetDamage(); }

    public void IncreaseDamage(int increase) { damage.IncreaseMaxDamage(increase); }

    public void ReduceDamage(int decrease) { damage.DecreaseMaxDamage(decrease); }

}
