using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class TestHelper
{
    public static readonly string ANY_CARD_NAME_1 = "ANY CARD NAME 1";
    public static readonly string ANY_CARD_NAME_2 = "ANY CARD NAME 2";
    public static readonly string ANY_CARD_NAME_3 = "ANY CARD NAME 3";
    public static readonly string ANY_CARD_NAME_4 = "ANY CARD NAME 4";
    public static readonly string ANY_CARD_TEXT = "ANY CARD TEXT";

    public static float RoundToNearestHundredth(float floatPointNumber)
    {
        return Mathf.Round(floatPointNumber * 100) / 100;
    }

    public static Hand GetHand()
    {
        var hand = new GameObject().AddComponent<Hand>();
        hand.SPACING = 5.0f;
        return hand;
    }

    public static DraftManager GetDraftManager(int spacing)
    {
        var draftManager = new GameObject().AddComponent<DraftManager>();
        draftManager.SPACING = spacing;
        draftManager.Awake();
        return draftManager;
    }

    public static CardDragger GetCardDragger()
    {
        var cardDragger = new GameObject().AddComponent<CardDragger>();
        cardDragger.gameObject.AddComponent<HoverAnimation>();
        cardDragger.Awake();
        return cardDragger;
    }

    public static CardManager GetCardManager()
    {
        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = GetMonsterCard();
        cardManager.roomCardPrefab = GetRoomCard();
        cardManager.AddCommonCard(GetMonsterCardInfo(ANY_CARD_NAME_1, ANY_CARD_TEXT));
        cardManager.AddCommonCard(GetMonsterCardInfo(ANY_CARD_NAME_2, ANY_CARD_TEXT));
        cardManager.AddCommonCard(GetMonsterCardInfo(ANY_CARD_NAME_3, ANY_CARD_TEXT));
        cardManager.AddRareCard(GetRoomCardInfo(2, ANY_CARD_NAME_4, ANY_CARD_TEXT));
        cardManager.AddRareCard(GetRoomCardInfo(1, ANY_CARD_NAME_4, ANY_CARD_TEXT));
        return cardManager;
    }

    private static MonsterCard GetMonsterCard()
    {
        var monsterCard = new GameObject().AddComponent<MonsterCard>();
        monsterCard.gameObject.AddComponent<SpriteRenderer>();
        var cardDescription = new GameObject().AddComponent<TextMeshPro>();
        cardDescription.transform.SetParent(monsterCard.transform);
        return monsterCard;
    }

    private static RoomCard GetRoomCard()
    {
        var roomCard = new GameObject().AddComponent<RoomCard>();
        roomCard.gameObject.AddComponent<SpriteRenderer>();
        return roomCard;
    }

    private static MonsterCardInfo GetMonsterCardInfo(string cardName, string cardEffect)
    {
        var monsterCardInfo = ScriptableObject.CreateInstance<MonsterCardInfo>();
        monsterCardInfo.cardName = cardName;
        monsterCardInfo.cost = 2;
        monsterCardInfo.effectDescription = cardEffect;
        return monsterCardInfo;
    }

    public static RoomCardInfo GetRoomCardInfo(int cost, string cardName, string cardEffect)
    {
        var roomCardInfo = ScriptableObject.CreateInstance<RoomCardInfo>();
        roomCardInfo.cardName = cardName;
        roomCardInfo.cost = cost;
        roomCardInfo.effectDescription = cardEffect;
        return roomCardInfo;
    }

    public static DraftCard GetRoomDraftCard()
    {
        var roomCardInfo = GetRoomCardInfo(1, ANY_CARD_NAME_4, ANY_CARD_TEXT);
        var draftCard = new GameObject().AddComponent<DraftCard>();
        draftCard.gameObject.AddComponent<SpriteRenderer>();
        var cardDescription = new GameObject().AddComponent<TextMeshPro>();
        cardDescription.transform.SetParent(draftCard.transform);
        draftCard.SetCardInfo(roomCardInfo);
        return draftCard;
    }

    public static Player GetPlayer(int damage, int health)
    {
        var player = new GameObject().AddComponent<Player>();
        player.gameObject.AddComponent<Damage>().IncreaseMaxDamage(damage);
        player.gameObject.AddComponent<Health>().IncreaseMaxHealth(health);
        player.Awake();
        return player;
    }

    public static Room GetRoom()
    {
        var room = new GameObject().AddComponent<Room>();
        room.gameObject.AddComponent<Capacity>();
        room.Awake();
        room.SetCapacity(2);
        return room;
    }

    public static SelectionScreen GetSelectionScreen()
    {
        var selectionScreen = new GameObject().AddComponent<SelectionScreen>();
        selectionScreen.transform.position = new Vector2(3, 5);
        selectionScreen.SetMaxSelection(3);
        return selectionScreen;
    }

    public static Option GetOption()
    {
        var option = new GameObject().AddComponent<Option>();
        var cost = new GameObject().AddComponent<TextMeshPro>();
        cost.transform.SetParent(option.transform);
        var description = new GameObject().AddComponent<TextMeshPro>();
        description.transform.SetParent(option.transform);
        return option;
    }

    public static StarterPack GetStarterPackOptionInfo()
    {
        var optionInfo = ScriptableObject.CreateInstance<StarterPack>();
        optionInfo.cost = 1;
        optionInfo.description = "ANY_TEXT";
        optionInfo.packPrefab = new GameObject().AddComponent<Pack>();
        return optionInfo;
    }

    public static RandomMonster GetRandomMonsterOptionInfo()
    {
        var oneCardPackOptionInfo = ScriptableObject.CreateInstance<RandomMonster>();
        oneCardPackOptionInfo.cost = 1;
        oneCardPackOptionInfo.description = "ANY_TEXT";
        oneCardPackOptionInfo.packPrefab = new GameObject().AddComponent<Pack>();
        return oneCardPackOptionInfo;
    }
}
