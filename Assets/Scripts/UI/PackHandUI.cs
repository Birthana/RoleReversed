using UnityEngine;

public class PackHandUI : CardUI
{
    public SpriteRenderer packSprite;

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        var packInfo = (PackInfo)newCardInfo;
        SetColor(packInfo.GetColor());

        if (packInfo.GetPackSprite() != null && packSprite != null)
        {
            packSprite.sprite = packInfo.GetPackSprite();
        }
    }
}
