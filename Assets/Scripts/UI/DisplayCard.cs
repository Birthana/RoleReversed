using UnityEngine;

public class DisplayCard : MonoBehaviour
{
    private static readonly Vector3 MONSTER_CARD_SPRITE_SCALE = new Vector3(1.0f, 1.0f, 1.0f);
    private static readonly Vector3 ROOM_CARD_SPRITE_SCALE = new Vector3(0.5f, 0.5f, 1.0f);
    [SerializeField] private MonsterCardUI monsterCardUI;
    [SerializeField] private RoomCardUI roomCardUI;
    private CardInfo cardInfo;

    private MonsterCardUI GetMonsterCardUI()
    {
        if (monsterCardUI == null)
        {
            monsterCardUI = GetComponent<MonsterCardUI>();
        }

        return monsterCardUI;
    }

    private RoomCardUI GetRoomCardUI()
    {
        if (roomCardUI == null)
        {
            roomCardUI = GetComponent<RoomCardUI>();
        }

        return roomCardUI;
    }

    public void SetCardInfo(CardInfo newCardInfo)
    {
        cardInfo = newCardInfo;
        if (newCardInfo.IsMonster())
        {
            SetUpMonsterCard();
            GetMonsterCardUI().SetCardInfo((MonsterCardInfo)newCardInfo);
        }

        if (newCardInfo.IsRoom())
        {
            SetupRoomCard();
            GetRoomCardUI().SetCardInfo((RoomCardInfo)newCardInfo);
        }
    }

    private void SetUpMonsterCard()
    {
        if (GetMonsterCardUI().GetDamageUI() == null ||
            GetMonsterCardUI().GetHealthUI() == null ||
            GetRoomCardUI().GetCapacity() == null ||
            GetMonsterCardUI().GetCardSprite() == null)
        {
            return;
        }

        GetMonsterCardUI().GetDamageUI().gameObject.SetActive(true);
        GetMonsterCardUI().GetHealthUI().gameObject.SetActive(true);
        GetRoomCardUI().GetCapacity().gameObject.SetActive(false);
        GetMonsterCardUI().GetCardSprite().transform.localScale = MONSTER_CARD_SPRITE_SCALE;
    }

    private void SetupRoomCard()
    {
        if (GetMonsterCardUI().GetDamageUI() == null ||
            GetMonsterCardUI().GetHealthUI() == null ||
            GetRoomCardUI().GetCapacity() == null ||
            GetRoomCardUI().GetCardSprite() == null)
        {
            return;
        }

        GetMonsterCardUI().GetDamageUI().gameObject.SetActive(false);
        GetMonsterCardUI().GetHealthUI().gameObject.SetActive(false);
        GetRoomCardUI().GetCapacity().gameObject.SetActive(true);
        GetRoomCardUI().GetCardSprite().transform.localScale = ROOM_CARD_SPRITE_SCALE;
    }

    public CardInfo GetCardInfo() { return cardInfo; }

}
