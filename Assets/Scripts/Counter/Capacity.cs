public class Capacity : Counter
{
    public void ResetMaxCapacity() { ResetMaxCount(); }

    public bool HasCapacity() { return GetCount() > 0; }

    public void IncreaseCapacity(int capacity) { IncreaseCount(capacity); }

    public void IncreaseMaxCapacity(int capacity) { IncreaseMaxCountWithoutReset(capacity); }

    public void DecreaseCapacity(int capacity) { DecreaseCount(capacity); }

    public void DecreaseMaxCapacity(int capacity) { DecreaseMaxCountWithoutReset(capacity); }
}
