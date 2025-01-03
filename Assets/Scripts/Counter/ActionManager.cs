public class ActionManager : Counter
{
    public void ResetActions() { ResetMaxCount(); }

    public bool CanCast(Card card) { return card.GetCost() <= GetCount(); }

    public void AddActions(int actions) { IncreaseCount(actions); }

    public void ReduceActions(int actions) { DecreaseCount(actions); }
}
