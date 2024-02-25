using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    public void Awake();

    public bool IsRunning();
}

public class GameManager : MonoBehaviour, IGameManager
{
    public Player playerPrefab;
    public Pack packPrefab;
    public Vector2 PLAYER_OFFSET;
    public GameObject respawnCounter;
    public GameObject gameOverScreen;
    private Room startRoom;
    private Player player;
    private Room currentRoom;
    private Monster[] monsters;
    private bool isRunning = false;
    private Coroutine coroutine;
    [SerializeField] private FocusAnimation focusAnimation;
    private bool focusOnRoom = false;

    public void Awake()
    {
        gameOverScreen.SetActive(false);
        SpawnPackInRandomSpot();
        focusAnimation.SetFocusPosition(Vector3.up * 3);
        focusAnimation.SetFocusScale(2.5f);
    }

    public bool IsRunning() { return isRunning; }

    public void StartPlayerRun()
    {
        if(startRoom == null || isRunning)
        {
            return;
        }

        isRunning = true;
        monsters = FindObjectsOfType<Monster>();
        coroutine = StartCoroutine(WalkThruDungeon());
    }

    public bool DoesNotHaveStartRoom() { return startRoom == null; }

    public void SetStartRoom(Room room)
    {
        player = Instantiate(playerPrefab);
        startRoom = room;
        SetRoom(startRoom);
        PlayerMoveTo(room);
    }

    public void ResetPlayer() { StartCoroutine(Reset()); }

    private IEnumerator Reset()
    {
        isRunning = false;
        StopCoroutine(coroutine);
        yield return focusAnimation.UnfocusOn();
        new ChangeSortingLayer(currentRoom.gameObject).SetToDefault();
        ResetPlayerToStartRoom();
        ResetFieldMonsters();
        FindObjectOfType<ActionManager>().ResetActions();
        FindObjectOfType<PlayerSoulCounter>().IncreaseSouls();
        FindObjectOfType<Deck>().DrawCardToHand();
        FindObjectOfType<DraftManager>().Draft();
        BuildConstructionRooms();
    }

    private void ResetPlayerToStartRoom()
    {
        SetRoom(startRoom);
        PlayerMoveTo(currentRoom);
        ResetPlayerStats();
    }

    private void ResetPlayerStats()
    {
        player.ResetStats();
        player.GetStronger();
    }

    private void ResetFieldMonsters()
    {
        ResetAllMonsterStats();
        DestroyAllTempMonsters();
    }

    private void BuildConstructionRooms()
    {
        var constructionRooms = FindObjectsOfType<ConstructionRoom>();
        if (constructionRooms.Length == 0)
        {
            return;
        }

        foreach (var room in constructionRooms)
        {
            if (room.CanBeBuilt())
            {
                room.SpawnRoom();
            }
        }
    }

    private void DestroyAllTempMonsters()
    {
        monsters = FindObjectsOfType<Monster>();
        foreach (var monster in monsters)
        {
            if (monster.isTemporary)
            {
                monster.transform.parent.GetComponent<Room>().Remove(monster);
            }
        }
    }

    private void ResetAllMonsterStats()
    {
        foreach (var monster in monsters)
        {
            monster.gameObject.SetActive(true);
            monster.ResetStats();
        }
    }

    private void SpawnPackInRandomSpot()
    {
        var newPack = Instantiate(packPrefab);
        newPack.LoadStarterPack();
        newPack.transform.position = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0);
    }

    private void SetRoom(Room room)
    {
        currentRoom = room;
    }

    private IEnumerator FocusOn(Room room)
    {
        yield return room.BattleStart();
        new ChangeSortingLayer(room.gameObject).SetToCurrentRoom();
        yield return focusAnimation.FocusOn(room.transform);
    }
    private IEnumerator UnfocusOn(Room room)
    {
        yield return focusAnimation.UnfocusOn();
        new ChangeSortingLayer(room.gameObject).SetToDefault();
    }

    private bool ShouldEnterRoom() { return !currentRoom.IsEmpty() && !(currentRoom is ConstructionRoom); }

    private bool ShouldExitRoom() { return !ShouldEnterRoom(); }

    private bool GameOver() { return currentRoom.IsEmpty() && NoMoreMonsters(); }

    private IEnumerator FocusOnCurrentRoom()
    {
        if (ShouldEnterRoom())
        {
            yield return FocusOn(currentRoom);
            focusOnRoom = true;
        }
    }

    private IEnumerator UnfocusOnCurrentRoom()
    {
        if (focusOnRoom)
        {
            yield return UnfocusOn(currentRoom);
        }
    }

    private IEnumerator MakeAttacks()
    {
        yield return StartCoroutine(player.MakeAttack(currentRoom.GetRandomMonster()));
        yield return StartCoroutine(currentRoom.MakeAttack(player));
    }

    public IEnumerator WalkThruDungeon()
    {
        ReduceTimerOnConstructionRooms();
        yield return FocusOnCurrentRoom();

        bool still_running = true;
        while (still_running)
        {
            yield return new WaitForSeconds(0.25f);

            if (GameOver())
            {
                yield return UnfocusOnCurrentRoom();
                gameOverScreen.SetActive(true);
                break;
            }

            if (ShouldExitRoom())
            {
                yield return UnfocusOnCurrentRoom();
                GoToNextRoom();
                PlayerMoveTo(currentRoom);
                yield return FocusOnCurrentRoom();
                continue;
            }

            yield return MakeAttacks();
        }
    }

    private void ReduceTimerOnConstructionRooms()
    {
        var constructionRooms = FindObjectsOfType<ConstructionRoom>();
        if (constructionRooms.Length == 0)
        {
            return;
        }

        foreach(var room in constructionRooms)
        {
            room.ReduceTimer();
        }
    }

    private bool NoMoreMonsters()
    {
        foreach (var monster in monsters)
        {
            if (monster.transform.parent.GetComponent<Room>() is ConstructionRoom)
            {
                continue;
            }

            if (!monster.IsDead())
            {
                return false;
            }
        }

        return true;
    }

    private void GoToNextRoom()
    {
        SetRoom(currentRoom.GetNextRoom());
    }

    private void PlayerMoveTo(Room room)
    {
        player.transform.SetParent(room.transform);
        player.transform.localPosition = PLAYER_OFFSET;
    }
}
