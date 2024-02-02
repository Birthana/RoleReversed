public class Capacity : Counter
{
    public void ResetMaxCapacity() { ResetMaxCount(); }

    public bool HasCapacity() { return GetCount() > 0; }

    public void IncreaseCapacity(int capacity) { IncreaseCount(capacity); }

    public void DecreaseCapacity(int capacity) { DecreaseCount(capacity); }

    public bool CurrentCapacityIsMaxCapcity() { return GetCount() == maxCount; }
}
