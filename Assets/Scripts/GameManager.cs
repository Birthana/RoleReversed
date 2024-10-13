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
    private static readonly string GIFTS_FILE_PATH = "Prefabs/Gifts";
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
    [SerializeField] private ResetMonster resetMonster;
    private bool isRunning = false;
    private Coroutine coroutine;
    [SerializeField] private IFocusAnimation focusAnimation;
    private bool focusOnRoom = false;
    private Roommates request;
    private List<GameObject> requestBubbles = new List<GameObject>();
    [SerializeField] private List<Room> rooms = new List<Room>();
    private List<RoommateEffectInfo> giftInfos = new List<RoommateEffectInfo>();
    private List<Monster> movedMonsters = new List<Monster>();
    private SoulShop soulShop;

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
        gameOverScreen.SetActive(false);
        shopButton.SetActive(false);
        startButton.SetActive(false);
        SpawnPackInRandomSpot();
        GetResetMonster();
        var focusPosition = Vector3.left * 3;
        focusPosition.y = 1;
        GetFocusAnimation().SetFocusPosition(focusPosition);
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
        if (startRoom == null || isRunning || GetSoulShop().IsOpen())
        {
            return;
        }

        FindObjectOfType<ToolTipManager>().Clear();
        GiveRoommateBonus();
        CheckRoommateBonusActive();
        isRunning = true;
        resetMonster.SetMonstersLocked(new List<Monster>(FindObjectsOfType<Monster>()));
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
        room.AddRoommateEffect(giftInfos[UnityEngine.Random.Range(0, giftInfos.Count)], roommates);
        rooms.Add(room);
    }

    private void CheckRoommateBonusActive()
    {
        foreach(var room in rooms.ToList())
        {
            var roommates = room.GetRoommateMonsters();
            if (room.Equals(roommates[0].transform.parent.GetComponent<Room>()) &&
                room.Equals(roommates[1].transform.parent.GetComponent<Room>()))
            {
                continue;
            }
            
            room.RemoveAllRoommateEffects();
            rooms.Remove(room);
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
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        yield return UnfocusOn(currentRoom);
        ResetPlayerToStartRoom();
        resetMonster.ResetFieldMonsters();
        ResetRooms();
        FindObjectOfType<ActionManager>().ResetActions();
        EnableShopButton();
        FindObjectOfType<PlayerSoulCounter>().IncreaseSouls();
        FindObjectOfType<Deck>().DrawCardToHand();
        BuildConstructionRooms();
        FindObjectOfType<SoulShop>().EnableDraft();
        BuildStart();
        FindObjectOfType<SoulShop>().OpenShop();
    }

    private void BuildStart()
    {
        var allRooms = FindObjectsOfType<Room>();
        foreach (var room in allRooms)
        {
            StartCoroutine(room.BuildStart());
        }
    }

    private void ResetRooms()
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

    public void AddToTempMove(Monster movedMonster) { movedMonsters.Add(movedMonster); }

    public void CreateRoommateRequests()
    {
        request = new Roommates(resetMonster.GetMonsters(), rooms);
        var requestMates = request.CreateRequest();
        foreach (var monster in requestMates)
        {
            var requestBubble = Instantiate(requestPrefab, monster.transform);
            requestBubble.transform.localPosition = new Vector3(-1.5f, 1.0f, 0.0f);
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
}
