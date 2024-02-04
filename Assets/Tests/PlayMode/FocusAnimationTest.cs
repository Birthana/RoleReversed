using Moq;
using System.Linq;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FocusAnimationTest : MonoBehaviour
{
    [SetUp]
    public void Setup()
    {
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
        var room = new GameObject().AddComponent<Room>();
        room.transform.position = new Vector3(3, 5, 0);
        var focusAnimation = new GameObject().AddComponent<FocusAnimation>();
        focusAnimation.SetFocusPosition(Vector3.up);

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
        var room = new GameObject().AddComponent<Room>();
        room.transform.position = new Vector3(3, 5, 0);
        var focusAnimation = new GameObject().AddComponent<FocusAnimation>();
        focusAnimation.SetFocusPosition(Vector3.up);
        focusAnimation.SetFocusScale(3);

        // Act
        focusAnimation.Focus(room.transform);
        yield return new WaitForSeconds(0.5f);

        // Assert
        Assert.AreEqual(3, room.transform.localScale.x);
    }

    [UnityTest]
    public IEnumerator UsingFocusAnimation_FocusTwice_ExpectFirstFocusPositionAndScale()
    {
        // Arrange
        var room = new GameObject().AddComponent<Room>();
        room.transform.position = new Vector3(3, 5, 0);
        var focusAnimation = new GameObject().AddComponent<FocusAnimation>();
        focusAnimation.SetFocusPosition(Vector3.up);
        focusAnimation.SetFocusScale(3);
        var secondRoom = new GameObject().AddComponent<Room>();
        secondRoom.transform.position = new Vector3(3, 5, 0);

        // Act
        focusAnimation.Focus(room.transform);
        yield return new WaitForSeconds(0.5f);
        focusAnimation.Focus(secondRoom.transform);
        yield return new WaitForSeconds(1.0f);

        // Assert
        Assert.AreEqual(new Vector3(3, 5, 0), room.transform.position);
        Assert.AreEqual(1, room.transform.localScale.x);
    }
}
