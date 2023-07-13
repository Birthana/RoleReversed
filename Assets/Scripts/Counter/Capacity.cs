public class Capacity : Counter
{
    public void ResetMaxCapacity() { ResetMaxCount(); }

    public bool HasCapacity() { return GetCount() > 0; }

    public void DecreaseCapacity(int capacity) { DecreaseCount(capacity); }
}
