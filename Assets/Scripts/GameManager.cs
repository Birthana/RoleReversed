using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
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

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        SpawnPackInRandomSpot();
        focusAnimation.SetFocusPosition(Vector3.up * 3);
        focusAnimation.SetFocusScale(2.5f);
    }

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
        SetRoom(startRoom);
        PlayerMoveTo(currentRoom);
        player.ResetStats();
        player.GetStronger();
        FindObjectOfType<ActionManager>().ResetActions();
        ResetAllMonsterStats();
        DestroyAllTempMonsters();
        FindObjectOfType<DraftManager>().Draft();
        FindObjectOfType<PlayerSoulCounter>().IncreaseSouls();
        FindObjectOfType<Deck>().DrawCardToHand();

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

    public IEnumerator WalkThruDungeon()
    {
        bool focusOnRoom = false;
        if (!currentRoom.IsEmpty())
        {
            new ChangeSortingLayer(currentRoom.gameObject).SetToCurrentRoom();
            yield return focusAnimation.FocusOn(currentRoom.transform);
            focusOnRoom = true;
        }

        bool still_running = true;
        while (still_running)
        {
            yield return new WaitForSeconds(0.25f);
            if (!currentRoom.IsEmpty())
            {
                yield return StartCoroutine(player.MakeAttack(currentRoom.GetRandomMonster()));
                yield return StartCoroutine(currentRoom.MakeAttack(player));
            }

            if (currentRoom.IsEmpty() && NoMoreMonsters())
            {
                gameOverScreen.SetActive(true);
                break;
            }

            if (currentRoom.IsEmpty())
            {
                if (focusOnRoom)
                {
                    yield return focusAnimation.UnfocusOn();
                    new ChangeSortingLayer(currentRoom.gameObject).SetToDefault();
                }

                GoToNextRoom();
                PlayerMoveTo(currentRoom);
                if (!currentRoom.IsEmpty())
                {
                    new ChangeSortingLayer(currentRoom.gameObject).SetToCurrentRoom();
                    yield return focusAnimation.FocusOn(currentRoom.transform);
                }
            }
        }
    }

    private bool NoMoreMonsters()
    {
        foreach (var monster in monsters)
        {
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
