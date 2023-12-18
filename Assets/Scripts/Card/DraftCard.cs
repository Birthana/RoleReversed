using UnityEngine;
using TMPro;

public class DraftCard : MonoBehaviour
{
    private CardInfo cardInfo;

    public void SetCardInfo(CardInfo newCardInfo)
    {
        cardInfo = newCardInfo;
        GetComponent<SpriteRenderer>().sprite = newCardInfo.cardSprite;
        GetComponentInChildren<TextMeshPro>().text = newCardInfo.effectDescription;
    }

    public CardInfo GetCardInfo() { return cardInfo; }
}
