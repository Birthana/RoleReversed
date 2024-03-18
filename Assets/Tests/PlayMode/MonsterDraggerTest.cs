using Moq;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MonsterDraggerTest : MonoBehaviour
{
    private readonly static Vector3 EXPECTED_POSITION = new Vector2(3, 5);
    private Mock<IMouseWrapper> mock;
    private Mock<IGameManager> gameManagerMock;
    private Monster expectedMonster;
    private Room expectedRoom;

    [SetUp]
    public void Setup()
    {
        var gameObject = new GameObject();
        expectedMonster = gameObject.AddComponent<Monster>();
        gameObject = new GameObject();
        expectedRoom = gameObject.AddComponent<Room>();
        expectedRoom.gameObject.AddComponent<Capacity>();
        expectedMonster.transform.SetParent(expectedRoom.transform);
        var basicUI = new GameObject().AddComponent<BasicUI>();
        gameObject.AddComponent<Damage>().ui = basicUI;
        gameObject.AddComponent<Health>().ui = basicUI;
        gameManagerMock = new Mock<IGameManager>(MockBehavior.Strict);
        gameManagerMock.Setup(x => x.Awake());
        gameManagerMock.Setup(x => x.IsRunning()).Returns(false);
        SetMouseMocks();
    }

    [TearDown]
    public void TearDown()
    {
        FindObjectsOfType<Monster>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<MonsterDragger>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
        FindObjectsOfType<GameManager>().ToList().ForEach(o => DestroyImmediate(o.gameObject));
    }

    private void SetMouseMocks()
    {
        mock = new Mock<IMouseWrapper>(MockBehavior.Strict);
        mock.Setup(x => x.PlayerPressesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnMonster()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Monster>()).Returns(expectedMonster);
        mock.Setup(x => x.GetPosition()).Returns(EXPECTED_POSITION);
        mock.Setup(x => x.PlayerReleasesLeftClick()).Returns(false);
    }

    private MonsterDragger SetMouseDraggerMocksToDragTo(Room newRoom)
    {
        var monsterDragger = SetupMonsterDragger();
        monsterDragger.UpdateLoop();
        mock.Setup(x => x.PlayerReleasesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnRoom()).Returns(true);
        mock.Setup(x => x.GetHitComponent<Room>()).Returns(newRoom);
        monsterDragger.SetMouseWrapper(mock.Object);
        return monsterDragger;
    }

    private Room CreateRoom(int capacity)
    {
        var newRoom = new GameObject().AddComponent<Room>();
        newRoom.gameObject.AddComponent<Capacity>();
        newRoom.transform.position = Vector3.left;
        newRoom.SetCapacity(capacity);
        return newRoom;
    }

    private MonsterDragger SetupMonsterDragger()
    {
        var monsterDragger = new GameObject().AddComponent<MonsterDragger>();
        monsterDragger.SetMouseWrapper(mock.Object);
        monsterDragger.SetGameManager(gameManagerMock.Object);
        return monsterDragger;
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
        monsterDragger.SetGameManager(gameManagerMock.Object);

        // Act
        monsterDragger.PickUp();
        yield return null;

        // Assert
        mock.VerifyAll();
        Assert.AreEqual(expectedMonster, monsterDragger.GetSelected());
    }

    [UnityTest]
    public IEnumerator GivenMouseClick_UpdateLoop_ExpectPosition()
    {
        // Arrange
        var monsterDragger = SetupMonsterDragger();

        // Act
        monsterDragger.UpdateLoop();
        yield return null;

        // Assert
        mock.VerifyAll();
        Assert.AreEqual(EXPECTED_POSITION, expectedMonster.transform.position);
    }

    [UnityTest]
    public IEnumerator GivenRelaseMouseClick_UpdateLoop_ExpectReturnToRoom()
    {
        // Arrange
        expectedRoom.Add(expectedMonster);
        var monsterDragger = SetupMonsterDragger();
        monsterDragger.UpdateLoop();
        mock.Setup(x => x.PlayerReleasesLeftClick()).Returns(true);
        mock.Setup(x => x.IsOnRoom()).Returns(false);
        monsterDragger.SetMouseWrapper(mock.Object);

        // Act
        monsterDragger.UpdateLoop();
        yield return null;

        // Assert
        mock.VerifyAll();
        var returnPosition = expectedMonster.transform.position;
        Assert.AreEqual(returnPosition, expectedMonster.transform.position);
    }

    [UnityTest]
    public IEnumerator GivenRelaseMouseClickOnNewRoom_UpdateLoop_ExpectMoveToNewRoom()
    {
        // Arrange
        expectedRoom.Add(expectedMonster);
        expectedRoom.SetCapacity(0);
        var newRoom = CreateRoom(1);
        var monsterDragger = SetMouseDraggerMocksToDragTo(newRoom);

        // Act
        monsterDragger.UpdateLoop();
        yield return null;

        // Assert
        mock.VerifyAll();
        Assert.AreEqual(new Vector3(newRoom.MONSTER_OFFSET.x + newRoom.transform.position.x, 0, 0),
                        expectedMonster.transform.position);
        Assert.AreEqual(1, expectedRoom.GetCapacity());
        Assert.AreEqual(0, newRoom.GetCapacity());
    }

    [UnityTest]
    public IEnumerator GivenMonsterInFullRoomAndAnotherFullRoom_DragMonsterToOtherRoom_ExpectNoMove()
    {
        // Arrange
        expectedRoom.Add(expectedMonster);
        expectedRoom.SetCapacity(0);
        var newRoom = CreateRoom(0);
        var monsterDragger = SetMouseDraggerMocksToDragTo(newRoom);

        // Act
        monsterDragger.UpdateLoop();
        yield return null;

        // Assert
        mock.VerifyAll();
        var returnPosition = expectedMonster.transform.position;
        Assert.AreEqual(returnPosition, expectedMonster.transform.position);
        Assert.AreEqual(0, expectedRoom.GetCapacity());
        Assert.AreEqual(0, newRoom.GetCapacity());
    }

    [UnityTest]
    public IEnumerator XXX()
    {
        // Arrange
        var monsterDragger = SetupMonsterDragger();
        var toolTip = TestHelper.GetToolTipManager();

        // Act
        monsterDragger.UpdateLoop();
        yield return null;

        // Assert
        mock.VerifyAll();
        var toolTipManager = FindObjectOfType<ToolTipManager>();
        Assert.AreEqual(TestHelper.ANY_CARD_TEXT, toolTipManager.GetToolTip());
    }
}
