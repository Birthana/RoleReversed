using UnityEngine;

public class Pack : MonoBehaviour
{
    public int numberOfCards = 5;

    private void Update()
    {
        if (PlayerClicksOnPack())
        {
            OpenPack();
            Destroy(gameObject);
        }
    }

    private bool PlayerClicksOnPack() { return Mouse.PlayerPressesLeftClick() && Mouse.IsOnPack(); }

    private void OpenPack()
    {
        var hand = FindObjectOfType<Hand>();
        var rngCards = FindObjectOfType<CardManager>();
        hand.Add(rngCards.GetMonsterCard());
        hand.Add(rngCards.GetRoomCard());
        for (int i = 0; i < numberOfCards - 2; i++)
        {
            hand.Add(rngCards.GetRandomCard());
        }
    }
}
