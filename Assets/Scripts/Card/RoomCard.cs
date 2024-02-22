using UnityEngine;
using TMPro;

public class RoomCard : Card
{
    [SerializeField] private RoomCardUI cardUI;
    private static readonly string FIELD_ROOM_PREFAB = "Prefabs/FieldRoom";
    private Room roomPrefab;
    private GameManager gameManager;
    private RoomTransform roomTransform;
    private RoomCardInfo roomCardInfo;
    private IMouseWrapper mouseWrapper;

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
            cardUI = GetComponent<RoomCardUI>();
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

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        roomPrefab = Resources.Load<Room>(FIELD_ROOM_PREFAB);
        roomCardInfo = (RoomCardInfo)newCardInfo;
        roomTransform = new RoomTransform(mouseWrapper.GetHitTransform());
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

    public override bool HasTarget()
    {
        if (Mouse.IsOnSpace())
        {
            roomTransform = new RoomTransform(mouseWrapper.GetHitTransform());
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
        FindObjectOfType<CardManager>().IncreaseRarity();
        SpawnRoom();
        base.Cast();
    }

    private void SpawnRoom()
    {
        var room = Instantiate(roomPrefab);
        room.Setup(roomCardInfo, roomTransform.GetTransform().position);
        DestroyImmediate(roomTransform.GetTransform().gameObject);
        if (GetGameManager().DoesNotHaveStartRoom())
        {
            GetGameManager().SetStartRoom(room);
        }
    }
}
