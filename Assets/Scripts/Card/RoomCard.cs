public class RoomCard : Card
{
    private RoomCardUI cardUI;
    private Room roomPrefab;
    private GameManager gameManager;
    private RoomTransform roomTransform;
    private RoomCardInfo roomCardInfo;
    private IMouseWrapper mouseWrapper;
    private SpaceManager spaces;

    public void SetMouseWrapper(IMouseWrapper wrapper)
    {
        mouseWrapper = wrapper;
    }

    private void Awake()
    {
        SetMouseWrapper(new MouseWrapper());
    }

    private RoomCardUI GetCardUI()
    {
        if (cardUI == null)
        {
            cardUI = (RoomCardUI)Instantiate(roomCardInfo.GetCardUI(), transform);
            cardUI.SetCardInfo(roomCardInfo);
        }

        return cardUI;
    }

    private GameManager GetGameManager()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        return gameManager;
    }

    private SpaceManager GetSpaceManager()
    {
        if (spaces == null)
        {
            spaces = FindObjectOfType<SpaceManager>();
        }

        return spaces;
    }

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        roomCardInfo = (RoomCardInfo)newCardInfo;
        roomPrefab = roomCardInfo.GetFieldRoomPrefab();
        roomTransform = new RoomTransform();
        GetCardUI().SetCardInfo(roomCardInfo);
    }

    public override CardInfo GetCardInfo()
    {
        return roomCardInfo;
    }

    public override int GetCost()
    {
        return roomCardInfo.cost;
    }

    public override string GetName()
    {
        return roomCardInfo.cardName;
    }

    public void SetRoomTransform()
    {
        roomTransform = new RoomTransform(mouseWrapper.GetHitTransform());
    }

    public override bool HasTarget()
    {
        if (Mouse.IsOnSpace())
        {
            SetRoomTransform();
            if (!RoomIsAdjacentToRoom())
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public bool RoomIsAdjacentToRoom()
    {
        if (GetGameManager().DoesNotHaveStartRoom())
        {
            return true;
        }

        return roomTransform.SelectSpaceHasAdjacentRoom();
    }

    public override void Cast()
    {
        GetHand().Remove(this);
        SpawnRoom();
        base.Cast();
    }

    private void SpawnRoom()
    {
        var room = Instantiate(roomPrefab);
        room.Setup(roomCardInfo, roomTransform.GetTransform().position);
        GetSpaceManager().RemoveSpace(roomTransform.GetTransform().localPosition);
        GetGameManager().AddToRooms(room);
    }
}
