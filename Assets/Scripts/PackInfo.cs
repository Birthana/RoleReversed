using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PackInfo", menuName = "PackInfo")]
public class PackInfo : CardInfo
{
    private static readonly string PACK_HAND_PREFAB = "Prefabs/PackHandPrefab";
    private static readonly string PACK_HAND_UI_PREFAB = "Prefabs/PackHandUI";
    private static readonly string DROP_PACK_HAND_UI_PREFAB = "Prefabs/DropPackHand";
    public List<CardInfo> cardInfos = new List<CardInfo>();
    private Color color = Color.white;
    private Sprite packSprite;

    public override Card GetCardPrefab()
    {
        var cardPrefab = Resources.Load<PackHand>(PACK_HAND_PREFAB);
        return cardPrefab;
    }

    public override CardUI GetCardUI()
    {
        var cardUI = Resources.Load<PackHandUI>(PACK_HAND_UI_PREFAB);
        return cardUI;
    }

    public override CardUI GetDropCardUI()
    {
        var cardUI = Resources.Load<PackHandUI>(DROP_PACK_HAND_UI_PREFAB);
        return cardUI;
    }

    public void SetChoices(List<CardInfo> newCardInfos)
    {
        cardInfos = newCardInfos;
    }

    public List<CardInfo> GetChoices() { return cardInfos; }

    public void RemoveChoice(CardInfo cardInfo)
    {
        cardInfos.Remove(cardInfo);
    }

    public Color GetColor()
    {
        if (color == Color.white)
        {
            color = FindObjectOfType<RandomColor>().GetRandomColor();
        }

        return color;
    }

    public void SetPackSprite(Sprite sprite)
    {
        packSprite = sprite;
    }

    public Sprite GetPackSprite()
    {
        return packSprite;
    }
}
