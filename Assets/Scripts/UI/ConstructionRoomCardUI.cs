using UnityEngine;

public class ConstructionRoomCardUI : RoomCardUI
{
    [SerializeField] private BasicUI timer;

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        var constructionRoomCardInfo = (ConstructionRoomCardInfo)newCardInfo;
        SetTimer(constructionRoomCardInfo.timer);
        base.SetCardInfo(newCardInfo);
    }

    private void SetTimer(int cardTimer)
    {
        if (timer == null)
        {
            return;
        }

        timer.Display(cardTimer);
    }
}
