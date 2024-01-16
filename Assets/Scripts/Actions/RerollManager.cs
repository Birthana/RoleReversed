public class RerollManager : Counter
{
    private Hand hand;
    private CardManager cardManager;

    protected override void Awake()
    {
        base.Awake();
        hand = FindObjectOfType<Hand>();
        cardManager = FindObjectOfType<CardManager>();
    }

    public void UseSelectedCardToPayForReroll()
    {
        IncreaseCount(1);
        if (PlayerPaidForReroll())
        {
            Reroll();
        }
    }

    public bool PlayerPaidForReroll() { return GetCount() == 2; }

    public void Reroll()
    {
        SetMaxCount(0);
        //hand.Add(cardManager.CreateRandomCard());
    }

}
