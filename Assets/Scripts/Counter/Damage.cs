public class Damage : Counter
{
    public int GetDamage() { return GetCount(); }

    public void ResetDamage() { ResetMaxCount(); }

    public void IncreaseMaxDamage(int increase) { IncreaseMaxCount(increase); }

    public void IncreaseMaxDamageWithoutReset(int increase) { IncreaseMaxCountWithoutReset(increase); }

    public void DecreaseMaxDamage(int decrease) { DecreaseMaxCount(decrease); }

    public void TemporaryIncreaseDamage(int increase) { IncreaseCount(increase); }

    public void TemporaryDecreaseDamage(int decrease) { DecreaseCount(decrease); }
}
