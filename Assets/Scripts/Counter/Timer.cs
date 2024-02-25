public class Timer : Counter
{
    public void ResetMaxTimer() { ResetMaxCount(); }

    public bool HasCapacity() { return GetCount() > 0; }

    public void IncreaseTimer(int timer) { IncreaseCount(timer); }

    public void DecreaseTimer(int timer) { DecreaseCount(timer); }
}
