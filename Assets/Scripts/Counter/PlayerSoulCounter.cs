public class PlayerSoulCounter : Counter
{
    public int GetCurrentSouls() { return GetCount(); }

    public void IncreaseSouls() { IncreaseCount(1); }

    public void DecreaseSouls() { DecreaseCount(1); }

    public void DecreaseSouls(int souls) { DecreaseCount(souls); }
}
