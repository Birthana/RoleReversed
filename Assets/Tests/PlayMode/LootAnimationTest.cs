using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LootAnimationTest : MonoBehaviour
{
    public LootAnimation lootAnimation;
    public Deck deck;

    [SetUp]
    public void Setup()
    {
        deck = new GameObject().AddComponent<Deck>();
        deck.transform.position = new Vector2(3, 5);
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<LootAnimation>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    //[UnityTest]
    //public IEnumerator UsingLootAnimation_Animate_ExpectSprite()
    //{
    //    // Arrange
    //    Sprite sprite = Resources.Load<Sprite>("Sprites/card");
    //    var monsterCardInfo = TestHelper.GetAnyMonsterCardInfo();
    //    monsterCardInfo.cardSprite = sprite;
    //    var gameObject = new GameObject();
    //    gameObject.AddComponent<SpriteRenderer>();
    //    lootAnimation = gameObject.AddComponent<LootAnimation>();
    //    lootAnimation.gameObject.AddComponent<MonsterCardUI>();
    //    lootAnimation.gameObject.AddComponent<RoomCardUI>();

    //    // Act
    //    lootAnimation.AnimateLoot(monsterCardInfo);
    //    yield return null;

    //    // Assert
    //    Assert.AreEqual(sprite, lootAnimation.GetSprite());
    //}

    [UnityTest]
    public IEnumerator UsingLootAnimationAnimate_WaitForHalfSecond_ExpectNoSprite()
    {
        // Arrange
        Sprite sprite = Resources.Load<Sprite>("Sprites/card");
        var monsterCardInfo = TestHelper.GetAnyMonsterCardInfo();
        monsterCardInfo.fieldSprite = sprite;
        var gameObject = new GameObject();
        gameObject.AddComponent<SpriteRenderer>();
        lootAnimation = gameObject.AddComponent<LootAnimation>();
        lootAnimation.gameObject.AddComponent<MonsterCardUI>();
        lootAnimation.gameObject.AddComponent<RoomCardUI>();

        // Act
        lootAnimation.AnimateLoot(monsterCardInfo);
        yield return new WaitForSeconds(LootAnimation.ANIMATION_TIME + 0.1f);

        // Assert
        Assert.AreEqual(null, lootAnimation.GetSprite());
    }

    [UnityTest]
    public IEnumerator UsingLootAnimationAnimate_WaitForHalfSecond_ExpectPosition()
    {
        // Arrange
        Sprite sprite = Resources.Load<Sprite>("Sprites/card");
        var monsterCardInfo = TestHelper.GetAnyMonsterCardInfo();
        monsterCardInfo.fieldSprite = sprite;
        var gameObject = new GameObject();
        gameObject.AddComponent<SpriteRenderer>();
        lootAnimation = gameObject.AddComponent<LootAnimation>();
        lootAnimation.gameObject.AddComponent<MonsterCardUI>();
        lootAnimation.gameObject.AddComponent<RoomCardUI>();

        // Act
        lootAnimation.AnimateLoot(monsterCardInfo);
        yield return new WaitForSeconds(LootAnimation.ANIMATION_TIME + 0.1f);

        // Assert
        Assert.AreEqual(new Vector3(3, 5, 0), lootAnimation.transform.position);
    }

    [UnityTest]
    public IEnumerator UsingLootAnimationAnimate_WaitForABit_ExpectNoMove()
    {
        // Arrange
        Sprite sprite = Resources.Load<Sprite>("Sprites/card");
        var monsterCardInfo = TestHelper.GetAnyMonsterCardInfo();
        monsterCardInfo.fieldSprite = sprite;
        var gameObject = new GameObject();
        gameObject.AddComponent<SpriteRenderer>();
        lootAnimation = gameObject.AddComponent<LootAnimation>();
        lootAnimation.gameObject.AddComponent<MonsterCardUI>();
        lootAnimation.gameObject.AddComponent<RoomCardUI>();

        // Act
        lootAnimation.AnimateLoot(monsterCardInfo);
        yield return new WaitForSeconds(LootAnimation.SHOW_TIME);

        // Assert
        Assert.AreEqual(Vector3.zero, lootAnimation.transform.position);
    }

    //[UnityTest]
    //public IEnumerator UsingLootAnimationAnimateWithDelay_WaitForDelay_ExpectNoMove()
    //{
    //    // Arrange
    //    Sprite sprite = Resources.Load<Sprite>("Sprites/card");
    //    var monsterCardInfo = TestHelper.GetAnyMonsterCardInfo();
    //    monsterCardInfo.cardSprite = sprite;
    //    var gameObject = new GameObject();
    //    gameObject.AddComponent<SpriteRenderer>();
    //    lootAnimation = gameObject.AddComponent<LootAnimation>();
    //    lootAnimation.gameObject.AddComponent<MonsterCardUI>();
    //    lootAnimation.gameObject.AddComponent<RoomCardUI>();
    //    float delay = LootAnimation.SHOW_TIME;
    //    lootAnimation.SetDelay(delay * 1);

    //    // Act
    //    lootAnimation.AnimateLoot(monsterCardInfo);
    //    yield return new WaitForSeconds(delay);

    //    // Assert
    //    Assert.AreEqual(Vector3.zero, lootAnimation.transform.position);
    //    Assert.AreEqual(sprite, lootAnimation.GetSprite());
    //}
}
