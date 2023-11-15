public class BlackSlime : Monster
{
    public override void Exit()
    {
        var card = FindObjectOfType<CardManager>().GetRandomCard();
        FindObjectOfType<Hand>().Add(card);
    }
}
