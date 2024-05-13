using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class TestHelper
{
    public static readonly string ANY_CARD_NAME_1 = "ANY CARD NAME 1";
    public static readonly string ANY_CARD_NAME_2 = "ANY CARD NAME 2";
    public static readonly string ANY_CARD_NAME_3 = "ANY CARD NAME 3";
    public static readonly string ANY_CARD_NAME_4 = "ANY CARD NAME 4";
    public static readonly string ANY_CARD_NAME_5 = "ANY CARD NAME 5";
    public static readonly string ANY_CARD_NAME_6 = "ANY CARD NAME 6";
    public static readonly string ANY_CARD_TEXT = "ANY CARD TEXT";

    public static float RoundToNearestHundredth(float floatPointNumber)
    {
        return Mathf.Round(floatPointNumber * 100) / 100;
    }

    public static GameManager GetGameManager()
    {
        var gameManager = new GameObject().AddComponent<GameManager>();
        gameManager.gameOverScreen = new GameObject();
        gameManager.shopButton = new GameObject();
        gameManager.startButton = new GameObject();
        gameManager.packPrefab = GetPack();
        gameManager.playerPrefab = new GameObject().AddComponent<Player>();
        return gameManager;
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
        draftManager.lootAnimation = GetLootAnimation();
        draftManager.SPACING = spacing;
        var button = new GameObject().AddComponent<Button>();
        button.gameObject.AddComponent<Image>();
        draftManager.SetupShowFieldButton(button);
        draftManager.Awake();
        return draftManager;
    }

    public static LootAnimation GetLootAnimation()
    {
        var lootAnimation = new GameObject().AddComponent<LootAnimation>();
        lootAnimation.gameObject.AddComponent<SpriteRenderer>();
        lootAnimation.gameObject.AddComponent<MonsterCardUI>();
        lootAnimation.gameObject.AddComponent<RoomCardUI>();
        return lootAnimation;
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
        cardManager.AddCommonCard(GetMonsterCardInfo(ANY_CARD_NAME_1, ANY_CARD_TEXT));
        cardManager.AddCommonCard(GetMonsterCardInfo(ANY_CARD_NAME_2, ANY_CARD_TEXT));
        cardManager.AddCommonCard(GetMonsterCardInfo(ANY_CARD_NAME_3, ANY_CARD_TEXT));
        cardManager.AddRareCard(GetRoomCardInfo(2, ANY_CARD_NAME_4, ANY_CARD_TEXT));
        cardManager.AddRareCard(GetRoomCardInfo(1, ANY_CARD_NAME_5, ANY_CARD_TEXT));
        cardManager.AddRareCard(GetRoomCardInfo(1, ANY_CARD_NAME_6, ANY_CARD_TEXT));
        cardManager.Awake();
        return cardManager;
    }

    public static MonsterCard GetMonsterCard()
    {
        var monsterCard = new GameObject().AddComponent<MonsterCard>();
        monsterCard.gameObject.AddComponent<MonsterCardUI>();
        monsterCard.gameObject.AddComponent<SpriteRenderer>();
        var cardDescription = new GameObject().AddComponent<TextMeshPro>();
        cardDescription.transform.SetParent(monsterCard.transform);
        monsterCard.SetCardInfo(GetAnyMonsterCardInfo());
        return monsterCard;
    }

    public static RoomCard GetRoomCard()
    {
        var roomCard = new GameObject().AddComponent<RoomCard>();
        roomCard.gameObject.AddComponent<RoomCardUI>();
        roomCard.gameObject.AddComponent<SpriteRenderer>();
        return roomCard;
    }

    public static MonsterCardInfo GetAnyMonsterCardInfo()
    {
        return GetMonsterCardInfo(ANY_CARD_NAME_1, ANY_CARD_TEXT);
    }

    public static MonsterCardInfo GetAnyOtherMonsterCardInfo()
    {
        return GetMonsterCardInfo(ANY_CARD_NAME_2, ANY_CARD_TEXT);
    }

    public static MonsterCardInfo GetMonsterCardInfo(string cardName, string cardEffect)
    {
        var monsterCardInfo = ScriptableObject.CreateInstance<MonsterCardInfo>();
        monsterCardInfo.cardName = cardName;
        monsterCardInfo.cost = 1;
        monsterCardInfo.effectDescription = cardEffect;
        monsterCardInfo.damage = 1;
        monsterCardInfo.health = 1;
        Sprite sprite = Resources.Load<Sprite>("Sprites/card");
        monsterCardInfo.fieldSprite = sprite;
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
        draftCard.gameObject.AddComponent<MonsterCardUI>();
        draftCard.gameObject.AddComponent<RoomCardUI>();
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
        room.SetCapacity(2);
        return room;
    }

    public static Player GetPlayerInRoom(int damage, int health)
    {
        var player = GetPlayer(damage, health);
        var room = GetRoom();
        player.transform.SetParent(room.transform);
        return player;
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
        optionInfo.description = "ANY_TEXT_1";
        optionInfo.packPrefab = new GameObject().AddComponent<Pack>();
        return optionInfo;
    }

    public static RandomMonsterDraw GetRandomMonsterOptionInfo()
    {
        var oneCardPackOptionInfo = ScriptableObject.CreateInstance<RandomMonsterDraw>();
        oneCardPackOptionInfo.cost = 1;
        oneCardPackOptionInfo.description = "ANY_TEXT_2";
        oneCardPackOptionInfo.packPrefab = new GameObject().AddComponent<Pack>();
        return oneCardPackOptionInfo;
    }

    public static RandomTwoMonsters GetRandomTwoMonstersOptionInfo()
    {
        var oneCardPackOptionInfo = ScriptableObject.CreateInstance<RandomTwoMonsters>();
        oneCardPackOptionInfo.cost = 1;
        oneCardPackOptionInfo.description = "ANY_TEXT_2";
        oneCardPackOptionInfo.packPrefab = new GameObject().AddComponent<Pack>();
        return oneCardPackOptionInfo;
    }

    public static RandomTwoRooms GetRandomTwoRoomsOptionInfo()
    {
        var oneCardPackOptionInfo = ScriptableObject.CreateInstance<RandomTwoRooms>();
        oneCardPackOptionInfo.cost = 1;
        oneCardPackOptionInfo.description = "ANY_TEXT_3";
        oneCardPackOptionInfo.packPrefab = new GameObject().AddComponent<Pack>();
        return oneCardPackOptionInfo;
    }

    public static RandomMonsterAction GetRandomMonsterActionOptionInfo()
    {
        var oneCardPackOptionInfo = ScriptableObject.CreateInstance<RandomMonsterAction>();
        oneCardPackOptionInfo.cost = 1;
        oneCardPackOptionInfo.description = "ANY_TEXT_4";
        oneCardPackOptionInfo.packPrefab = new GameObject().AddComponent<Pack>();
        return oneCardPackOptionInfo;
    }

    public static RandomRoomDraw GetRandomRoomOptionInfo()
    {
        var oneCardPackOptionInfo = ScriptableObject.CreateInstance<RandomRoomDraw>();
        oneCardPackOptionInfo.cost = 1;
        oneCardPackOptionInfo.description = "ANY_TEXT_3";
        oneCardPackOptionInfo.packPrefab = new GameObject().AddComponent<Pack>();
        return oneCardPackOptionInfo;
    }

    public static Deck GetDeck()
    {
        var deck = new GameObject().AddComponent<Deck>();
        deck.gameObject.AddComponent<DeckCount>();
        deck.gameObject.AddComponent<SpriteRenderer>();
        deck.Add(GetAnyMonsterCardInfo());
        return deck;
    }

    public static Pack GetPack()
    {
        var pack = new GameObject().AddComponent<Pack>();
        pack.gameObject.AddComponent<SpriteRenderer>();
        pack.gameObject.AddComponent<Animator>();
        pack.lootAnimation = GetLootAnimation();
        return pack;
    }

    public static Drop GetDrop()
    {
        var drop = new GameObject().AddComponent<Drop>();
        drop.gameObject.AddComponent<SpriteRenderer>();
        drop.frontDeckBox = new GameObject().AddComponent<SpriteRenderer>();
        return drop;
    }

    public static SoulShop GetSoulShop(Option option, OptionInfo optionInfo)
    {
        var soulShop = new GameObject().AddComponent<SoulShop>();
        soulShop.optionPrefab = option;
        soulShop.optionInfos.Add(optionInfo);
        return soulShop;
    }

    public static ConstructionRoomCardInfo GetConstructionRoomCardInfo()
    {
        var constructionRoomCardInfo = ScriptableObject.CreateInstance<ConstructionRoomCardInfo>();
        constructionRoomCardInfo.cardName = "Any Name.";
        constructionRoomCardInfo.cost = 2;
        constructionRoomCardInfo.effectDescription = "Any Effect.";
        constructionRoomCardInfo.timer = 2;
        constructionRoomCardInfo.capacity = 1;
        constructionRoomCardInfo.roomCardInfo = GetRoomCardInfo(2, "Any Name.", "Any Effect.");
        return constructionRoomCardInfo;
    }

    public static ToolTipManager GetToolTipManager()
    {
        var toolTip = new GameObject().AddComponent<ToolTipManager>();
        var parent = new GameObject();
        var text = new GameObject().AddComponent<TextMeshPro>();
        text.transform.SetParent(parent.transform);
        toolTip.toolTipPrefab = parent;
        return toolTip;
    }
}
