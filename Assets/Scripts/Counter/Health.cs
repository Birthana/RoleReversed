using System;

public class Health : Counter
{
    public event Action<int> OnTakeDamage;
    public event Action<int, int> OnHealthChange;

    public int GetCurrentHealth() { return GetCount(); }
    public int GetMaxHealth() { return maxCount; }

    public void RestoreFullHealth()
    {
        ResetMaxCount();
        OnHealthChange?.Invoke(GetCurrentHealth(), GetMaxHealth());
    }

    public void RestoreHealth(int health)
    {
        IncreaseCount(health);
        OnHealthChange?.Invoke(GetCurrentHealth(), GetMaxHealth());
    }

    public void TakeDamage(int damage)
    {
        DecreaseCount(damage);
        OnTakeDamage?.Invoke(damage);
        OnHealthChange?.Invoke(GetCurrentHealth(), GetMaxHealth());
    }

    public void IncreaseMaxHealth(int increase) { IncreaseMaxCount(increase); }

    public void DecreaseMaxHealth(int decrease) { DecreaseMaxCount(decrease); }

    public void IncreaseMaxHealthWithoutReset(int increase) { IncreaseMaxCountWithoutReset(increase); }

    public void TemporaryIncreaseHealth(int increase) { IncreaseCount(increase); }

    public void TemporaryDecreaseHealth(int decrease) { DecreaseCount(decrease); }
}
