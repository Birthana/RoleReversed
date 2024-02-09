public class DropCount : Counter
{
    public void AddToDrop() { IncreaseCount(1); }
    public void RemoveFromDrop() { DecreaseCount(1); }
}
