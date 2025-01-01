using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IGameManager
{
    public void Awake();

    public bool IsRunning();
}

public struct RoommateRoom
{
    public Room room;
    public List<Monster> monsters;

    public RoommateRoom(Room room, List<Monster> monsters)
    {
        this.room = room;
        this.monsters = monsters;
    }
}

public class GameManager : MonoBehaviour, IGameManager
{
    public static readonly float ATTACK_TIMER = 0.5f;
    public Player playerPrefab;
    public Pack packPrefab;
    public Vector2 PLAYER_OFFSET;
    public GameObject respawnCounter;
    public GameOver gameOverScreen;
    public GameObject shopButton;
    public GameObject startButton;
    public AudioClip button;
    private Room startRoom;
    private Player player;
    private Room currentRoom;
    [SerializeField] private ResetMonster resetMonster;
    [SerializeField] private List<Room> totalRooms = new List<Room>();
    private bool isRunning = false;
    private Coroutine coroutine;
    [SerializeField] private IFocusAnimation focusAnimation;
    private bool focusOnRoom = false;
    private List<Monster> movedMonsters = new List<Monster>();
    private SoulShop soulShop;
    private SoundManager soundManager;

    private IFocusAnimation GetFocusAnimation()
    {
        if (focusAnimation == null)
        {
            focusAnimation = GetComponent<FocusAnimation>();
        }

        return focusAnimation;
    }

    private ResetMonster GetResetMonster()
    {
        if (resetMonster == null)
        {
            resetMonster = GetComponent<ResetMonster>();
        }

        return resetMonster;
    }

    private SoulShop GetSoulShop()
    {
        if (soulShop == null)
        {
            soulShop = FindObjectOfType<SoulShop>(true);
        }

        return soulShop;
    }

    public void Awake()
    {
        gameOverScreen.Hide();
        shopButton.SetActive(false);
        startButton.SetActive(false);
        SpawnPackInRandomSpot();
        GetResetMonster();
        var focusPosition = Vector3.zero;
        GetFocusAnimation().SetFocusPosition(focusPosition);
        GetFocusAnimation().SetFocusScale(2.5f);
        soundManager = GetComponent<SoundManager>();
    }

    public void EnableShopButton()
    {
        shopButton.SetActive(true);
    }

    public void EnableStartButton()
    {
        startButton.SetActive(true);
    }

    public void SetFocusAnimation(IFocusAnimation animation)
    {
        focusAnimation = animation;
    }

    public bool IsRunning() { return isRunning; }

    public void StartPlayerRun()
    {
        soundManager.Play(button);
        if (startRoom == null || isRunning || GetSoulShop().IsOpen())
        {
            return;
        }

        FindObjectOfType<BackgroundMusic>().SwitchToBattle();
        FindObjectOfType<ToolTipManager>().Clear();
        isRunning = true;
        resetMonster.SetMonstersLocked();
        coroutine = StartCoroutine(WalkThruDungeon());
    }

    public bool DoesNotHaveStartRoom() { return startRoom == null; }

    public void SetStartRoom(Room room)
    {
        player = Instantiate(playerPrefab);
        startRoom = room;
        SetRoom(startRoom);
        PlayerMoveTo(room);
        EnableStartButton();
    }

    public void ResetPlayer() { StartCoroutine(Reset()); }

    private IEnumerator Reset()
    {
        isRunning = false;
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        yield return UnfocusOn(currentRoom);
        ResetPlayerToStartRoom();
        ResetMovedMonsters();
        resetMonster.ResetFieldMonsters();
        ResetRooms();
        FindObjectOfType<ActionManager>().ResetActions();
        EnableShopButton();
        FindObjectOfType<PlayerSoulCounter>().IncreaseSouls();
        FindObjectOfType<Deck>().DrawCardToHand();
        BuildConstructionRooms();
        FindObjectOfType<BackgroundMusic>().SwitchToBuild();
        BuildStart();
        FindObjectOfType<SoulShop>().OpenShop();
        FindObjectOfType<SoulShop>().FreeReroll();
        if (CheckIfWon())
        {
            gameOverScreen.ShowWon();
        }
    }

    private bool CheckIfWon()
    {
        int actualRoomCount = 0;
        foreach(var room in totalRooms)
        {
            if (room is ConstructionRoom)
            {
                return false;
            }

            actualRoomCount++;
        }

        if (actualRoomCount == 15)
        {
            return true;
        }

        return false;
    }

    private void BuildStart()
    {
        var allRooms = FindObjectsOfType<Room>();
        foreach (var room in allRooms)
        {
            StartCoroutine(room.BuildStart());
        }
    }

    private void ResetMovedMonsters()
    {
        foreach (var movedMonster in movedMonsters)
        {
            if (movedMonster.isTemporary)
            {
                continue;
            }
 
            movedMonster.ResetToOriginRoom();
        }

        movedMonsters = new List<Monster>();
    }

    private void ResetRooms()
    {
        var allRooms = FindObjectsOfType<Room>();
        foreach (var room in allRooms)
        {
            room.DisplayMonsters();
        }
    }

    public void AddToTempMove(Monster movedMonster) { movedMonsters.Add(movedMonster); }

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

    private void BuildConstructionRooms()
    {
        foreach (var room in totalRooms.ToList())
        {
            if (room is not ConstructionRoom)
            {
                continue;
            }

            var constructionRoom = (ConstructionRoom)room;
            if (constructionRoom.CanBeBuilt())
            {
                constructionRoom.SpawnRoom();
            }
        }
    }

    private void SpawnPackInRandomSpot()
    {
        var newPack = Instantiate(packPrefab);
        newPack.LoadStarterPack();
        newPack.transform.position = new Vector3(UnityEngine.Random.Range(-3.0f, 3.0f), UnityEngine.Random.Range(-3.0f, 3.0f), 0);
    }

    private void SetRoom(Room room)
    {
        currentRoom = room;
    }

    private IEnumerator FocusOn(Room room)
    {
        yield return room.BattleStart();

        if (player.IsDead() || !isRunning)
        {
            yield break;
        }

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

    public IEnumerator UnfocusOnCurrentRoom()
    {
        if (focusOnRoom)
        {
            yield return UnfocusOn(currentRoom);
        }
    }

    private IEnumerator MakeAttacks()
    {
        yield return StartCoroutine(player.MakeAttack(currentRoom.GetRandomMonster()));
        yield return StartCoroutine(currentRoom.MakeAttacks(player));
    }

    public IEnumerator WalkThruDungeon()
    {
        ReduceTimerOnConstructionRooms();
        yield return FocusOnCurrentRoom();

        bool still_running = true;
        while (still_running)
        {
            yield return new WaitForSeconds(ATTACK_TIMER / 2);

            if (GameOver())
            {
                yield return UnfocusOnCurrentRoom();
                gameOverScreen.ShowLose();
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
        foreach (var monster in resetMonster.GetMonsters())
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

    public void AddToMonsters(Monster monster)
    {
        resetMonster.Add(monster);
    }

    public void AddToRooms(Room room)
    {
        if (DoesNotHaveStartRoom())
        {
            SetStartRoom(room);
        }

        totalRooms.Add(room);
    }

    public void RemoveRoom(Room room)
    {
        totalRooms.Remove(room);
        DestroyImmediate(room.gameObject);
    }

    public int GetNumberOfTempSlimes() { return resetMonster.GetNumberOfTempSlimes(); }
}
