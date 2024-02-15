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

    private RoomCardUI GetCardUI()
    {
        if (cardUI == null)
        {
            cardUI = GetComponent<RoomCardUI>();
        }

        return cardUI;
    }


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        roomPrefab = Resources.Load<Room>(FIELD_ROOM_PREFAB);
        roomCardInfo = (RoomCardInfo)newCardInfo;
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
            roomTransform = new RoomTransform(Mouse.GetHitTransform());
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
        if (gameManager.DoesNotHaveStartRoom())
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
        room.GetComponent<SpriteRenderer>().sprite = roomCardInfo.fieldSprite;
        room.SetCapacity(roomCardInfo.capacity);
        room.SetCardInfo(roomCardInfo);
        room.transform.position = roomTransform.GetTransform().position;
        Destroy(roomTransform.GetTransform().gameObject);
        if (gameManager.DoesNotHaveStartRoom())
        {
            gameManager.SetStartRoom(room);
        }
    }
}
