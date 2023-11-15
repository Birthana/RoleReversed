using UnityEngine;

public class Pack : MonoBehaviour
{
    public int numberOfCards = 5;

    private void Update()
    {
        if (PlayerClicksOnPack())
        {
            OpenRarityPack();
            Destroy(gameObject);
        }
    }

    private bool PlayerClicksOnPack() { return Mouse.PlayerPressesLeftClick() && Mouse.IsOnPack(); }

    public void OpenRarityPack()
    {
        var hand = FindObjectOfType<Hand>();
        var rngCards = FindObjectOfType<CardManager>();
        hand.Add(rngCards.CreateRareCard());
        hand.Add(rngCards.CreateRoomCard());
        for (int i = 0; i < numberOfCards - 2; i++)
        {
            hand.Add(rngCards.CreateCommonCard());
        }
    }
}
