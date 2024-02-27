using UnityEngine;

public class DraftCard : MonoBehaviour
{
    private CardInfo cardInfo;

    public void SetCardInfo(CardInfo cardInfo)
    {
        this.cardInfo = cardInfo;
    }

    public CardInfo GetCardInfo()
    {
        return cardInfo;
    }
}
