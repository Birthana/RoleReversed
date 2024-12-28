using UnityEngine;

[CreateAssetMenu(fileName = "RatSquire", menuName = "CardInfo/Rat/RatSquire")]
public class RatSquire : MonsterCardInfo
{
    private bool CardIsInHand(Card cardSelf)
    {
        return cardSelf.transform.parent == GetHand().transform;
    }

    private bool RatSquireIsInHand(Card cardSelf)
    {
        return cardSelf != null && CardIsInHand(cardSelf);
    }

    public override void Engage(EffectInput input)
    {
        if (!RatSquireIsInHand(input.card))
        {
            return;
        }

        FindObjectOfType<Hand>().Remove(input.card);
        FindObjectOfType<Drop>().Add(input.card.GetCardInfo());
        FindObjectOfType<PlayerSoulCounter>().IncreaseSouls();
        FindObjectOfType<PlayerSoulCounter>().IncreaseSouls();
        DestroyImmediate(input.card.gameObject);
    }
}
