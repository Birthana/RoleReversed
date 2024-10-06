using System;

public class Health : Counter
{
    public event Action<int> OnTakeDamage;

    public int GetCurrentHealth() { return GetCount(); }

    public void RestoreFullHealth() { ResetMaxCount(); }

    public void RestoreHealth(int health)
    {
        IncreaseCount(health);
    }

    public void TakeDamage(int damage)
    {
        DecreaseCount(damage);
        OnTakeDamage?.Invoke(damage);
    }

    public void IncreaseMaxHealth(int increase) { IncreaseMaxCount(increase); }

    public void IncreaseMaxHealthWithoutReset(int increase) { IncreaseMaxCountWithoutReset(increase); }

    public void TemporaryIncreaseHealth(int increase) { IncreaseCount(increase); }
}
