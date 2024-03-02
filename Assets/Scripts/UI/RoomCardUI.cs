using UnityEngine;


public class RoomCardUI : CardUI
{
    [SerializeField] private BasicUI capacity;

    public BasicUI GetCapacity() { return capacity; }

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        var roomCardInfo = (RoomCardInfo)newCardInfo;
        SetCapacity(roomCardInfo.capacity);
        base.SetCardInfo(newCardInfo);
    }

    protected void SetCapacity(int cardCapacity)
    {
        if (capacity == null)
        {
            return;
        }

        capacity.Display(cardCapacity);
    }
}
