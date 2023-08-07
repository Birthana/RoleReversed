using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HoverAnimationTest : MonoBehaviour
{
    private readonly static float END_OF_ANIMATION = 1.0f;
    private readonly static float ANY_VERTICAL_MOVE = 2.0f;

    [UnityTest]
    public IEnumerator GivenHoverAnimation_Hover_ExpectHoverIsRunning()
    {
        // Arrange
        var gameObject = new GameObject();
        var ANY_CARD = new GameObject().AddComponent<Card>();
        var hoverAnimation = gameObject.AddComponent<HoverAnimation>();

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
        var gameObject = new GameObject();
        var ANY_CARD = new GameObject().AddComponent<Card>();
        var hoverAnimation = gameObject.AddComponent<HoverAnimation>();
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
        var gameObject = new GameObject();
        var ANY_CARD = new GameObject().AddComponent<Card>();
        var hoverAnimation = gameObject.AddComponent<HoverAnimation>();
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
        var gameObject = new GameObject();
        var ANY_CARD = new GameObject().AddComponent<Card>();
        var hoverAnimation = gameObject.AddComponent<HoverAnimation>();
        var startPosition = ANY_CARD.transform.position;
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);
        yield return new WaitForSeconds(END_OF_ANIMATION);
        hoverAnimation.StopHover(ANY_CARD);

        // Act
        yield return new WaitForSeconds(END_OF_ANIMATION);

        // Assert
        Assert.AreEqual(startPosition, ANY_CARD.transform.position);
    }

    [UnityTest]
    public IEnumerator GivenHoverForHalfTimeAndStopHover_WaitForEndOfStopHover_ExpectPositionIsAtStart()
    {
        // Arrange
        var gameObject = new GameObject();
        var ANY_CARD = new GameObject().AddComponent<Card>();
        var hoverAnimation = gameObject.AddComponent<HoverAnimation>();
        var startPosition = ANY_CARD.transform.position;
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);
        yield return new WaitForSeconds(END_OF_ANIMATION / 2);
        hoverAnimation.StopHover(ANY_CARD);

        // Act
        yield return new WaitForSeconds(END_OF_ANIMATION);

        // Assert
        Assert.AreEqual(startPosition, ANY_CARD.transform.position);
    }

    [UnityTest]
    public IEnumerator GivenASecondCardInterruptingFirstHover_WaitForSecondHover_ExpectPositionsAreCorrect()
    {
        // Arrange
        var ANY_CARD = new GameObject().AddComponent<Card>();
        var hoverAnimation = new GameObject().AddComponent<HoverAnimation>();
        var startPosition1 = ANY_CARD.transform.position;
        var ANY_CARD_2 = new GameObject().AddComponent<Card>();
        var startPosition2 = ANY_CARD_2.transform.position;
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);
        yield return new WaitForSeconds(END_OF_ANIMATION / 2);
        hoverAnimation.StopHover(ANY_CARD);
        hoverAnimation.Hover(ANY_CARD_2, ANY_VERTICAL_MOVE, END_OF_ANIMATION);

        // Act
        yield return new WaitForSeconds(END_OF_ANIMATION);

        // Assert
        Assert.AreEqual(startPosition1, ANY_CARD.transform.position);
        Assert.AreEqual(startPosition2 + (Vector3)new Vector2(0, ANY_VERTICAL_MOVE), ANY_CARD_2.transform.position);
    }

    [UnityTest]
    public IEnumerator GivenASecondInterruptingHover_WaitForEndOfAnimation_ExpectBackAtStart()
    {
        // Arrange
        var ANY_CARD = new GameObject().AddComponent<Card>();
        var hoverAnimation = new GameObject().AddComponent<HoverAnimation>();
        var startPosition1 = ANY_CARD.transform.position;
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);
        yield return new WaitForSeconds(0.1f);
        hoverAnimation.StopHover(ANY_CARD);
        hoverAnimation.Hover(ANY_CARD, ANY_VERTICAL_MOVE, END_OF_ANIMATION);
        hoverAnimation.StopHover(ANY_CARD);

        // Act
        yield return new WaitForSeconds(END_OF_ANIMATION);

        // Assert
        Assert.AreEqual(startPosition1, ANY_CARD.transform.position);
    }
}
