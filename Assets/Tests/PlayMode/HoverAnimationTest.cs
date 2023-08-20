using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HoverAnimationTest : MonoBehaviour
{
    private Card ANY_CARD;
    private HoverAnimation hoverAnimation;
    private readonly static float END_OF_ANIMATION = 1.0f;
    private readonly static float ANY_VERTICAL_MOVE = 2.0f;

    [SetUp]
    public void Setup()
    {
        ANY_CARD = new GameObject().AddComponent<Card>();
        hoverAnimation = new GameObject().AddComponent<HoverAnimation>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.FindObjectsOfType<Card>().ToList().ForEach(o => Object.DestroyImmediate(o.gameObject));
        Object.FindObjectsOfType<HoverAnimation>().ToList().ForEach(o => Object.DestroyImmediate(o.gameObject));
    }

    [UnityTest]
    public IEnumerator GivenHoverAnimation_Hover_ExpectHoverIsRunning()
    {
        // Arrange

        // Act
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);
        yield return null;

        // Assert
        Assert.AreEqual(true, hoverAnimation.IsRunning());
    }

    [UnityTest]
    public IEnumerator GivenHover_WaitForEndOfHover_ExpectHoverIsNotRunning()
    {
        // Arrange
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);

        // Act
        yield return new WaitForSeconds(END_OF_ANIMATION);

        // Assert
        Assert.AreEqual(false, hoverAnimation.IsRunning());
    }

    [UnityTest]
    public IEnumerator GivenHover_WaitForEndOfHover_ExpectPositionIsAtEnd()
    {
        // Arrange
        var startPosition = ANY_CARD.transform.position;
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);

        // Act
        yield return new WaitForSeconds(END_OF_ANIMATION);

        // Assert
        Assert.AreEqual(startPosition + (Vector3) new Vector2(0, ANY_VERTICAL_MOVE), ANY_CARD.transform.position);
    }

    [UnityTest]
    public IEnumerator GivenHoverAndStopHover_WaitForEndOfStopHover_ExpectPositionIsAtStart()
    {
        // Arrange
        var startPosition = ANY_CARD.transform.position;
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);
        yield return new WaitForSeconds(END_OF_ANIMATION);
        hoverAnimation.PerformReturn();

        // Act
        yield return new WaitForSeconds(END_OF_ANIMATION);

        // Assert
        Assert.AreEqual(startPosition, ANY_CARD.transform.position);
    }

    [UnityTest]
    public IEnumerator GivenHoverForHalfTimeAndStopHover_WaitForEndOfStopHover_ExpectPositionIsAtStart()
    {
        // Arrange
        var startPosition = ANY_CARD.transform.position;
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);
        yield return new WaitForSeconds(END_OF_ANIMATION / 2);
        hoverAnimation.PerformReturn();

        // Act
        yield return new WaitForSeconds(END_OF_ANIMATION);

        // Assert
        Assert.AreEqual(startPosition, ANY_CARD.transform.position);
    }

    [UnityTest]
    public IEnumerator GivenASecondCardInterruptingFirstHover_WaitForSecondHover_ExpectPositionsAreCorrect()
    {
        // Arrange
        var startPosition1 = ANY_CARD.transform.position;
        var ANY_CARD_2 = new GameObject().AddComponent<Card>();
        var startPosition2 = ANY_CARD_2.transform.position;
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);
        yield return new WaitForSeconds(END_OF_ANIMATION / 2);
        hoverAnimation.PerformReturn();
        hoverAnimation.Hover(ANY_CARD_2, ANY_VERTICAL_MOVE, END_OF_ANIMATION);

        // Act
        yield return new WaitForSeconds(END_OF_ANIMATION * 2);

        // Assert
        Assert.AreEqual(startPosition1, ANY_CARD.transform.position);
        Assert.AreEqual(startPosition2 + (Vector3)new Vector2(0, ANY_VERTICAL_MOVE), ANY_CARD_2.transform.position);
    }

    [UnityTest]
    public IEnumerator GivenASecondInterruptingHover_WaitForEndOfAnimation_ExpectBackAtStart()
    {
        // Arrange
        var startPosition1 = ANY_CARD.transform.position;
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);
        yield return new WaitForSeconds(0.1f);
        hoverAnimation.PerformReturn();
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);
        hoverAnimation.PerformReturn();

        // Act
        yield return new WaitForSeconds(END_OF_ANIMATION * 2);

        // Assert
        Assert.AreEqual(startPosition1, ANY_CARD.transform.position);
    }

    [UnityTest]
    public IEnumerator GivenHover_ResetHoverAnimation_ExpectHoverAnimationIsNotRunning()
    {
        // Arrange
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);
        yield return new WaitForSeconds(END_OF_ANIMATION / 2);

        // Act
        hoverAnimation.ResetHoverAnimation();

        // Assert
        Assert.AreEqual(false, hoverAnimation.IsRunning());
    }
}
