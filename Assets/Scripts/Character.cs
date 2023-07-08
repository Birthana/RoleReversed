using UnityEngine;
using TMPro;

[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour
{
    public int damage;
    public TextMeshPro damageUI;
    private Health health;

    public virtual void Awake()
    {
        damageUI.text = $"{damage}";
        health = GetComponent<Health>();
    }

    public bool IsDead() { return health.GetCurrentHealth() == 0; }

    public int GetDamage() { return damage; }

    public void TakeDamage(int damage) { health.TakeDamage(damage); }
}
