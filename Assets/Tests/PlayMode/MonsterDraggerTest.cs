using Moq;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MonsterDraggerTest : MonoBehaviour
{
    private Monster expectedMonster;

    [SetUp]
    public void Setup()
    {
        var gameObject = new GameObject();
        expectedMonster = gameObject.AddComponent<Monster>();
        var basicUI = new GameObject().AddComponent<BasicUI>();
        gameObject.AddComponent<Damage>().ui = basicUI;
        gameObject.AddComponent<Health>().ui = basicUI;
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<Monster>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<MonsterDragger>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    [UnityTest]
    public IEnumerator GivenMonster_PickUp_ExpectMonster()
    {
        // Arrange
        var mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
        mock.Setup(x => x.IsOnMonster()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Monster>()).Returns(expectedMonster);
        var monsterDragger = new GameObject().AddComponent<MonsterDragger>();
        monsterDragger.SetMouseWrapper(mock.Object);

        // Act
        monsterDragger.PickUp();
        yield return null;

        // Assert
        mock.VerifyAll();
        Assert.AreEqual(expectedMonster, monsterDragger.GetSelected());
    }

    [UnityTest]
    public IEnumerator XXX()
    {
        // Arrange
        var expectedPosition = new Vector2(3, 5);
        var mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnMonster()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Monster>()).Returns(expectedMonster);
        mock.Setup(x => x.GetPosition()).Returns(expectedPosition);
        var monsterDragger = new GameObject().AddComponent<MonsterDragger>();
        monsterDragger.SetMouseWrapper(mock.Object);

        // Act
        monsterDragger.UpdateLoop();
        yield return null;

        // Assert
        mock.VerifyAll();
        Assert.AreEqual(expectedPosition, (Vector2)expectedMonster.transform.position);
    }
}
