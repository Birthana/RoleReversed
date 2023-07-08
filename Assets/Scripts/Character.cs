using UnityEngine;
using TMPro;

[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour
{
    public int damage;
    public TextMeshPro damageUI;
    private Health health;

    private void Awake()
    {
        damageUI.text = $"{damage}";
        health = GetComponent<Health>();
    }

    public int GetDamage() { return damage; }

    public void TakeDamage(int damage) { health.TakeDamage(damage); }
}
