using System.Collections;
using System.Collections.Generic;
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
    private static readonly string GIFTS_FILE_PATH = "Prefabs/RoommateGifts";
    public GameObject requestPrefab;
    public Player playerPrefab;
    public Pack packPrefab;
    public Vector2 PLAYER_OFFSET;
    public GameObject respawnCounter;
    public GameObject gameOverScreen;
    public GameObject shopButton;
    public GameObject startButton;
    private Room startRoom;
    private Player player;
    private Room currentRoom;
    private List<Monster> monsters;
    private bool isRunning = false;
    private Coroutine coroutine;
    [SerializeField] private IFocusAnimation focusAnimation;
    private bool focusOnRoom = false;
    private Roommates request;
    private List<GameObject> requestBubbles = new List<GameObject>();
    private List<RoommateRoom> roommateRooms = new List<RoommateRoom>();
    private List<RoommateEffectInfo> giftInfos = new List<RoommateEffectInfo>();

    private IFocusAnimation GetFocusAnimation()
    {
        if (focusAnimation == null)
        {
            focusAnimation = GetComponent<FocusAnimation>();
        }

        return focusAnimation;
    }

    public void Awake()
    {
        gameOverScreen.SetActive(false);
        shopButton.SetActive(false);
        startButton.SetActive(false);
        SpawnPackInRandomSpot();
        GetFocusAnimation().SetFocusPosition(Vector3.up * 3);
        GetFocusAnimation().SetFocusScale(2.5f);
        LoadRoommateGifts();
    }

    private void LoadRoommateGifts()
    {
        var gifts = Resources.LoadAll<RoommateEffectInfo>(GIFTS_FILE_PATH);
        foreach (var gift in gifts)
        {
            giftInfos.Add(gift);
        }
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
        if (startRoom == null || isRunning || FindObjectOfType<DraftManager>().IsRunning())
        {
            return;
        }

        GiveRoommateBonus();
        CheckRoommateBonusActive();
        isRunning = true;
        monsters = new List<Monster>(FindObjectsOfType<Monster>());
        coroutine = StartCoroutine(WalkThruDungeon());
    }

    private void GiveRoommateBonus()
    {
        if (request == null)
        {
            return;
        }

        var roommates = request.GetRequest();

        ClearRequestBubbles();

        if (roommates.Count == 0)
        {
            return;
        }

        if (!request.MonstersAreInTheSameRoom(roommates))
        {
            return;
        }

        foreach (var roommate in roommates)
        {
            roommate.IncreaseStats(1, 1);
        }

        var room = roommates[0].transform.parent.GetComponent<Room>();
        room.AddRoommateEffect(giftInfos[Random.Range(0, giftInfos.Count)]);
        roommateRooms.Add(new RoommateRoom(room, roommates));
    }

    private void CheckRoommateBonusActive()
    {
        foreach(var room in roommateRooms)
        {
            var monsterRoom = room.monsters[0].transform.parent.GetComponent<Room>();
            if (room.room.Equals(monsterRoom))
            {
                continue;
            }
            
            room.room.RemoveAllRoommateEffects();
        }
    }

    private void ClearRequestBubbles()
    {
        foreach (var requestBubble in requestBubbles)
        {
            Destroy(requestBubble);
        }

        requestBubbles = new List<GameObject>();
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
        StopCoroutine(coroutine);
        yield return focusAnimation.UnfocusOn();
        new ChangeSortingLayer(currentRoom.gameObject).SetToDefault();
        ResetPlayerToStartRoom();
        ResetFieldMonsters();
        FindObjectOfType<ActionManager>().ResetActions();
        EnableShopButton();
        FindObjectOfType<PlayerSoulCounter>().IncreaseSouls();
        FindObjectOfType<Deck>().DrawCardToHand();
        FindObjectOfType<DraftManager>().Draft();
        BuildConstructionRooms();
        CreateRoommateRequests();
    }

    public void CreateRoommateRequests()
    {
        request = new Roommates(monsters);
        var requestMates = request.CreateRequest();
        foreach (var monster in requestMates)
        {
            var requestBubble = Instantiate(requestPrefab, monster.transform);
            requestBubbles.Add(requestBubble);
        }
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
        monsters = new List<Monster>(FindObjectsOfType<Monster>());
        foreach (var monster in monsters)
        {
            if (monster.isTemporary)
            {
                monster.transform.parent.GetComponent<Room>().Remove(monster);
            }
        }
        monsters = new List<Monster>(FindObjectsOfType<Monster>());
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

        if (player.IsDead())
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
