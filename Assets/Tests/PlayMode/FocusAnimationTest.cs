using Moq;
using System.Linq;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FocusAnimationTest : MonoBehaviour
{
    private Room room;
    private FocusAnimation focusAnimation;
    private static Vector3 ANY_POSITION = new Vector3(3, 5, 0);

    [SetUp]
    public void Setup()
    {
        room = TestHelper.GetRoom();
        room.transform.position = ANY_POSITION;
        focusAnimation = new GameObject().AddComponent<FocusAnimation>();
        focusAnimation.SetFocusPosition(Vector3.up);
        focusAnimation.SetFocusScale(3);
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<Room>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<FocusAnimation>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [UnityTest]
    public IEnumerator UsingFocusAnimation_Focus_ExpectFocusPosition()
    {
        // Arrange

        // Act
        focusAnimation.Focus(room.transform);
        yield return new WaitForSeconds(0.25f);

        // Assert
        Assert.AreEqual(Vector3.up, room.transform.position);
    }

    [UnityTest]
    public IEnumerator UsingFocusAnimation_Focus_ExpectFocusScale()
    {
        // Arrange

        // Act
        focusAnimation.Focus(room.transform);
        yield return new WaitForSeconds(0.5f + 0.1f);

        // Assert
        Assert.AreEqual(3, room.transform.localScale.x);
    }

    [UnityTest]
    public IEnumerator UsingFocusAnimation_FocusTwice_ExpectFirstFocusPositionAndScale()
    {
        // Arrange
        var secondRoom = TestHelper.GetRoom();
        secondRoom.transform.position = ANY_POSITION;

        // Act
        focusAnimation.Focus(room.transform);
        yield return new WaitForSeconds(0.5f);
        focusAnimation.Focus(secondRoom.transform);
        yield return new WaitForSeconds(1.0f);

        // Assert
        Assert.AreEqual(ANY_POSITION, room.transform.position);
        Assert.AreEqual(1, room.transform.localScale.x);
    }
}
