public class DeckCount : Counter
{
    public void AddToDeck() { IncreaseCount(1); }
    public void DrawFromDeck() { DecreaseCount(1); }
}
