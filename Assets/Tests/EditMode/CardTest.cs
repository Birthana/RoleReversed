using Moq;
using NUnit.Framework;
using UnityEngine;

public class CardTest : MonoBehaviour
{
    private MonsterCard card;
    private Hand hand;
    private CardDragger cardDragger;

    [SetUp]
    public void Setup()
    {
        card = new GameObject().AddComponent<MonsterCard>();
        hand = new GameObject().AddComponent<Hand>();
        cardDragger = new GameObject().AddComponent<CardDragger>();
        cardDragger.gameObject.AddComponent<HoverAnimation>();
        cardDragger.Awake();
    }

    [Test]
    public void GivenMonsterPrefabWithStats_CreateMonster_ExpectStats()
    {
        // Arrange
        var monster = new GameObject().AddComponent<Monster>();
        monster.gameObject.AddComponent<Damage>();
        monster.gameObject.AddComponent<Health>();
        monster.GetComponent<Damage>().maxCount = 2;
        monster.GetComponent<Health>().maxCount = 5;
        card.monsterPrefab = monster;

        // Act
        var newMonster = Instantiate(card.monsterPrefab);

        // Assert
        Assert.AreEqual(2, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(5, newMonster.GetComponent<Health>().maxCount);
    }

    [Test]
    public void XXX()
    {
        // Arrange
        var monster = new GameObject().AddComponent<BlackSlime>();
        monster.gameObject.AddComponent<Damage>();
        monster.gameObject.AddComponent<Health>();
        card.monsterPrefab = monster;

        var cardManager = new GameObject().AddComponent<CardManager>();
        cardManager.monsterCardPrefab = card.GetComponent<MonsterCard>();
        var monsterPrefab = new GameObject();
        monsterPrefab.AddComponent<Monster>();
        monsterPrefab.AddComponent<SpriteRenderer>();
        var cardInfo = ScriptableObject.CreateInstance<MonsterCardInfo>();
        cardInfo.prefab = monsterPrefab;
        cardManager.AddCommonCard(cardInfo);
        cardManager.AddRareCard(cardInfo);

        // Act
        var newMonster = Instantiate(card.monsterPrefab);
        newMonster.SetupStats();
        newMonster.Exit();

        // Assert
        Assert.AreEqual(1, hand.hand.Count);
        Assert.AreEqual(3, newMonster.GetComponent<Damage>().maxCount);
        Assert.AreEqual(3, newMonster.GetComponent<Health>().maxCount);
    }
}
