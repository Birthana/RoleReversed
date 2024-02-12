using UnityEngine;

public class DisplayCard : MonoBehaviour
{
    private static readonly Vector3 MONSTER_CARD_SPRITE_SCALE = new Vector3(1.0f, 1.0f, 1.0f);
    private static readonly Vector3 ROOM_CARD_SPRITE_SCALE = new Vector3(0.5f, 0.5f, 1.0f);
    [SerializeField] private MonsterCardUI monsterCardUI;
    [SerializeField] private RoomCardUI roomCardUI;
    [SerializeField] private BasicUI damageUI;
    [SerializeField] private BasicUI healthUI;
    [SerializeField] private BasicUI capacityUI;
    [SerializeField] private SpriteRenderer cardSprite;
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
        if (damageUI == null || healthUI == null || capacityUI == null || cardSprite == null)
        {
            return;
        }

        damageUI.gameObject.SetActive(true);
        healthUI.gameObject.SetActive(true);
        capacityUI.gameObject.SetActive(false);
        cardSprite.transform.localScale = MONSTER_CARD_SPRITE_SCALE;
    }

    private void SetupRoomCard()
    {
        if (damageUI == null || healthUI == null || capacityUI == null || cardSprite == null)
        {
            return;
        }

        damageUI.gameObject.SetActive(false);
        healthUI.gameObject.SetActive(false);
        capacityUI.gameObject.SetActive(true);
        cardSprite.transform.localScale = ROOM_CARD_SPRITE_SCALE;
    }

    public CardInfo GetCardInfo() { return cardInfo; }

}
