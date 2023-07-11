public class Health : Counter
{
    public int GetCurrentHealth() { return GetCount(); }

    public void RestoreFullHealth() { ResetMaxCount(); }

    public void TakeDamage(int damage) { DecreaseCount(damage); }

    public void IncreaseMaxHealth(int increase) { IncreaseMaxCount(increase); }
}
