using System;
using UnityEngine;

[RequireComponent(typeof(BasicUI))]
public class Health : MonoBehaviour
{
    public event Action<int> OnHealthChange;
    public int maxHealth;
    private int currentHealth;

    private void Awake()
    {
        OnHealthChange += GetComponent<BasicUI>().Display;
        RestoreFullHealth();
    }

    public int GetCurrentHealth() { return currentHealth; }

    public void RestoreFullHealth()
    {
        SetHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        SetHealth(Mathf.Max(0, currentHealth - damage));
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        SetHealth(newMaxHealth);
    }

    public void IncreaseMaxHealth(int increase)
    {
        SetMaxHealth(maxHealth + increase);
    }

    private void SetHealth(int health)
    {
        currentHealth = health;
        OnHealthChange?.Invoke(currentHealth);
    }
}
