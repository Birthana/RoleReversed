public class Respawn : Counter
{
    public int GetNumberOfTimesDied() { return GetCount(); }

    public void Increment() { IncreaseCount(1); }
}
