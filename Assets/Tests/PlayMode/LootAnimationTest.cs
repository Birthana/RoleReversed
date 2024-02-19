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
        lootAnimation = TestHelper.GetLootAnimation();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<LootAnimation>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [UnityTest]
    public IEnumerator UsingLootAnimationAnimate_WaitForHalfSecond_ExpectNoSprite()
    {
        // Arrange
        var monsterCardInfo = TestHelper.GetAnyMonsterCardInfo();

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
        var monsterCardInfo = TestHelper.GetAnyMonsterCardInfo();

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
        var monsterCardInfo = TestHelper.GetAnyMonsterCardInfo();

        // Act
        lootAnimation.AnimateLoot(monsterCardInfo);
        yield return new WaitForSeconds(LootAnimation.SHOW_TIME);

        // Assert
        Assert.AreEqual(Vector3.zero, lootAnimation.transform.position);
    }

    [UnityTest]
    public IEnumerator UsingLootAnimationAnimateWithDelay_WaitForDelay_ExpectNoMove()
    {
        // Arrange
        var monsterCardInfo = TestHelper.GetAnyMonsterCardInfo();
        float delay = LootAnimation.SHOW_TIME;
        lootAnimation.SetDelay(delay * 1);

        // Act
        lootAnimation.AnimateLoot(monsterCardInfo);
        yield return new WaitForSeconds(delay);

        // Assert
        Assert.AreEqual(Vector3.zero, lootAnimation.transform.position);
    }
}
