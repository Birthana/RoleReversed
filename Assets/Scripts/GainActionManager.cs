public class GainActionManager : Counter
{
    private ActionManager actionManager;

    protected override void Awake()
    {
        base.Awake();
        actionManager = FindObjectOfType<ActionManager>();
    }

    public bool HaveEnoughActions(Card card) { return actionManager.CanCast(card); }

    public void UseSelectCardToPayForGainAction() { IncreaseCount(1); }

    public bool PlayerPaidForGainAction() { return GetCount() == 3; }

    public void GainAction()
    {
        SetMaxCount(0);
        actionManager.AddActions(1);
    }

}
