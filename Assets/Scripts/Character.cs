using UnityEngine;
using TMPro;

[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour
{
    public int damage;
    public TextMeshPro damageUI;
    protected Health health;

    public virtual void Awake()
    {
        SetMaxDamage();
        health = GetComponent<Health>();
    }

    public void ResetStats()
    {
        SetMaxDamage();
        health.RestoreFullHealth();
    }

    public void SetMaxDamage() { damageUI.text = $"{damage}"; }

    public bool IsDead() { return health.GetCurrentHealth() == 0; }

    public int GetDamage() { return damage; }

    public void TakeDamage(int damage) { health.TakeDamage(damage); }
}
