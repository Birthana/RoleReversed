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

    public void OpenPack()
    {
        var hand = FindObjectOfType<Hand>();
        var rngCards = FindObjectOfType<CardManager>();
        hand.AddNewCard(rngCards.GetMonsterCard());
        hand.AddNewCard(rngCards.GetRoomCard());
        for (int i = 0; i < numberOfCards - 2; i++)
        {
            hand.AddNewCard(rngCards.GetRandomCard());
        }
    }

    public void OpenRarityPack()
    {
        var hand = FindObjectOfType<Hand>();
        var rngCards = FindObjectOfType<CardManager>();
        hand.AddNewCard(rngCards.GetRareCard());
        hand.AddNewCard(rngCards.GetRoomCard());
        for (int i = 0; i < numberOfCards - 2; i++)
        {
            hand.AddNewCard(rngCards.GetCommonCard());
        }
    }
}
