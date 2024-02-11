using UnityEngine;

public class DisplayCard : MonoBehaviour
{
    public void Set(CardInfo cardInfo)
    {
        GetComponent<SpriteRenderer>().sprite = cardInfo.cardSprite;
    }
}
