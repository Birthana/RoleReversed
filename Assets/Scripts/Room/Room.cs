using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    public Vector2 MONSTER_OFFSET = new Vector2(2, 0);
    private static readonly string FIELD_MONSTER_PREFAB = "Prefabs/FieldMonster";
    private static readonly int MAX_NUMBER_OF_MONSTERS_IN_COLUMN = 3;
    private static readonly int MAX_ROOM_CAPACITY = 9;
    private static readonly float MONSTER_SPACING = 10.0f;
    private static readonly float COLUMN_SPACING = 2.0f;

    private event Func<Room, IEnumerator> OnBattleStart;
    private List<RoommateEffectInfo> addedBattleStartEffects = new List<RoommateEffectInfo>();
    public List<Monster> monsters = new List<Monster>();
    public List<Monster> movedMonsters = new List<Monster>();
    private Capacity capacity;
    protected RoomCardInfo cardInfo;
    private Vector3 startPosition;
    private List<Monster> roommateMonsters = new List<Monster>();

    protected Capacity GetCapacityComponent()
    {
        if (capacity == null)
        {
            capacity = GetComponent<Capacity>();
        }

        return capacity;
    }

    public RoomCardInfo GetCardInfo()
    {
        return cardInfo;
    }

    public virtual void Setup(RoomCardInfo roomCardInfo, Vector3 position)
    {
        GetComponent<SpriteRenderer>().sprite = roomCardInfo.fieldSprite;
        SetCapacity(roomCardInfo.capacity);
        SetCardInfo(roomCardInfo);
        startPosition = position;
        transform.position = startPosition;
    }

    public Vector3 GetStartPosition() { return startPosition; }

    public IEnumerator MakeAttacks(Player player)
    {
        var rngMonsters = new List<Monster>(monsters);
        while (rngMonsters.Count != 0)
        {
            var rngMonster = rngMonsters[Random.Range(0, rngMonsters.Count)];
            rngMonsters.Remove(rngMonster);

            if (rngMonster.IsDead() || rngMonster.GetCurrentRoom() != this)
            {
                continue;
            }

            if (FindObjectOfType<Player>().IsDead())
            {
                yield break;
            }

            yield return rngMonster.MakeAttack(player);
        }
    }

    public IEnumerator MakeRandomAttack(Character character)
    {
        var randomIndex = Random.Range(0, monsters.Count);
        if (monsters[randomIndex].IsDead())
        {
            yield break;
        }

        yield return monsters[randomIndex].MakeAttack(character);
    }

    public void SetCapacity(int capacity)
    {
        GetCapacityComponent().maxCount = capacity;
        GetCapacityComponent().ResetMaxCapacity();
    }

    public int GetMaxCapacity()
    {
        return GetCapacityComponent().maxCount;
    }

    public int GetCapacity()
    {
        return GetCapacityComponent().GetCount();
    }

    public bool HasCapacity() { return GetCapacityComponent().HasCapacity(); }

    public void IncreaseCapacity(int increase)
    {
        GetCapacityComponent().IncreaseCapacity(increase);
    }

    public void IncreaseMaxCapacity(int increase)
    {
        if (GetMaxCapacity() >= MAX_ROOM_CAPACITY)
        {
            return;
        }

        var capacityCheck = Mathf.Min(increase, MAX_ROOM_CAPACITY - GetMaxCapacity());
        GetCapacityComponent().IncreaseMaxCapacity(capacityCheck);
    }
    
    public void ReduceCapacity(int decrease) { GetCapacityComponent().DecreaseCapacity(decrease); }

    public void ReduceMaxCapacity(int decrease) { GetCapacityComponent().DecreaseMaxCapacity(decrease); }

    public void Enter(Monster monster)
    {
        Add(monster);
        if (ShouldNotTrackCapacity(monster))
        {
            return;
        }

        ReduceCapacity(1);
    }

    public void Add(Monster monster)
    {
        monster.transform.parent = transform;
        monster.transform.localScale = Vector3.one;
        monsters.Add(monster);
        DisplayMonsters();
    }

    private bool ShouldTrackCapacity(Monster monster)
    {
        return !monster.isTemporary;
    }

    private bool ShouldNotTrackCapacity(Monster monster)
    {
        return !ShouldTrackCapacity(monster);
    }

    public void LeaveEffectCapacity(Monster monster)
    {
        if (ShouldTrackCapacity(monster))
        {
            IncreaseCapacity(1);
        }

        Leave(monster);
    }

    public void LeaveTemporary(Monster monster)
    {
        movedMonsters.Add(monster);
        Leave(monster);
    }

    public void Leave(Monster monster)
    {
        monsters.Remove(monster);
        DisplayMonsters();
    }

    public void DisplayMonsters()
    {
        var blockCenterPosition = new BlockCenterPosition(new Vector2(MONSTER_OFFSET.x, 0),
                                                          GetAliveMonsterCount(),
                                                          MAX_NUMBER_OF_MONSTERS_IN_COLUMN,
                                                          MONSTER_SPACING,
                                                          COLUMN_SPACING);
        int count = 0;
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].IsDead())
            {
                continue;
            }

            monsters[i].transform.localPosition = blockCenterPosition.GetVerticalLayoutPositionAt(count);
            count++;
        }
    }

    private int GetAliveMonsterCount()
    {
        int count = 0;
        foreach(var monster in monsters)
        {
            if (monster.IsDead())
            {
                continue;
            }

            count++;
        }

        return count;
    }

    public void Remove(Monster monster)
    {
        monster.gameObject.SetActive(false);
        monster.Exit();
        DisplayMonsters();
    }

    public void RemoveTemporary(Monster monster)
    {
        LeaveEffectCapacity(monster);
        Destroy(monster.gameObject);
    }

    public Monster GetRandomMonster()
    {
        if (IsEmpty())
        {
            return null;
        }

        return GetAliveRandomMonster();
    }

    public Monster GetRandomMonsterNot(Monster exception)
    {
        if (IsEmpty())
        {
            return null;
        }

        if (monsters.Count == 1)
        {
            return null;
        }

        return GetAliveRandomMonsterNot(exception);
    }

    private Monster GetAliveRandomMonster()
    {
        Monster monster;
        do
        {
            var rngIndex = Random.Range(0, monsters.Count);
            monster = monsters[rngIndex];
        } while (MonsterIsDead(monster));

        return monster;
    }

    private Monster GetAliveRandomMonsterNot(Monster exception)
    {
        Monster monster;
        do
        {
            var rngIndex = Random.Range(0, monsters.Count);
            monster = monsters[rngIndex];
        } while (MonsterIsDead(monster) || monster == exception);

        return monster;
    }

    private bool MonsterIsDead(Monster monster) { return monster.IsDead(); }

    public Room GetNextRoom()
    {
        var adjacentRooms = new RoomTransform(transform).GetAdjacentRooms();
        var rngIndex = Random.Range(0, adjacentRooms.Count);
        return adjacentRooms[rngIndex];
    }

    public bool IsEmpty()
    {
        if (monsters.Count == 0)
        {
            return true;
        }

        foreach (var monster in monsters)
        {
            if (!monster.IsDead() && monster.GetCurrentRoom() == this)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsFull()
    {
        return capacity.GetCount() == 0;
    }

    public Monster SpawnMonster(MonsterCardInfo monsterCardInfo)
    {
        var monster = CreateMonster(monsterCardInfo);
        ReduceCapacity(1);
        return monster;
    }

    public Monster SpawnTemporaryMonster(MonsterCardInfo monsterCardInfo)
    {
        var monster = CreateMonster(monsterCardInfo);
        new ChangeSortingLayer(monster.gameObject).SetToCurrentRoom();
        return monster;
    }

    public Monster SpawnCopy(MonsterCardInfo monsterCardInfo)
    {
        var monster = CreateMonster(monsterCardInfo);
        new ChangeSortingLayer(monster.gameObject).SetToCurrentRoom();
        monster.isTemporary = true;
        return monster;
    }

    public Monster SpawnTemporaryMonsterInDifferentRoom(MonsterCardInfo monsterCardInfo)
    {
        var monster = CreateMonster(monsterCardInfo);
        new ChangeSortingLayer(monster.gameObject).SetToDefault();
        return monster;
    }

    private Monster CreateMonster(MonsterCardInfo monsterCardInfo)
    {
        var monsterPrefab = Resources.Load<Monster>(FIELD_MONSTER_PREFAB);
        var monster = Instantiate(monsterPrefab, transform);
        var copy = Instantiate(monsterCardInfo);
        monster.Setup(copy);
        Add(monster);
        monster.Entrance();
        FindObjectOfType<GameManager>().AddToMonsters(monster);
        return monster;
    }

    public IEnumerator BattleStart()
    {
        if (OnBattleStart != null)
        {
            foreach (var function in OnBattleStart.GetInvocationList())
            {
                yield return function?.DynamicInvoke(this);
            }
        }

        yield return cardInfo.BattleStart(this);
    }

    public IEnumerator BuildStart()
    {
        yield return cardInfo.BuildStart(this);
    }

    public void SetCardInfo(RoomCardInfo roomCardInfo)
    {
        cardInfo = roomCardInfo;
    }

    public void AddRoommateEffect(RoommateEffectInfo roommateInfo, List<Monster> roommates)
    {
        OnBattleStart += roommateInfo.BattleStart;
        addedBattleStartEffects.Add(roommateInfo);
        roommateMonsters = roommates;
    }

    public void RemoveAllRoommateEffects()
    {
        if (addedBattleStartEffects.Count == 0)
        {
            return;
        }

        foreach (var roommateEffect in addedBattleStartEffects)
        {
            OnBattleStart -= roommateEffect.BattleStart;
        }

        addedBattleStartEffects = new List<RoommateEffectInfo>();
        roommateMonsters = null;
    }

    public List<RoommateEffectInfo> GetRoommateEffects()
    {
        return addedBattleStartEffects;
    }

    public void HighlightMonsters()
    {
        if (HasHighlightedMonsters())
        {
            return;
        }

        roommateMonsters[0].Highlight();
        roommateMonsters[1].Highlight();
    }

    public void ClearMonsterHighlight()
    {
        if (HasNoHighlightedMonsters())
        {
            return;
        }

        roommateMonsters[0].UnHighlight();
        roommateMonsters[1].UnHighlight();
    }

    private bool HasNoHighlightedMonsters()
    {
        if (roommateMonsters == null)
        {
            return true;
        }

        return !HasHighlightedMonsters();
    }

    private bool HasHighlightedMonsters()
    {
        if (roommateMonsters == null)
        {
            return true;
        }

        return roommateMonsters[0].IsHighlighted();
    }

    public List<Monster> GetRoommateMonsters() { return roommateMonsters; }

    public List<Room> GetAdjacentRooms()
    {
        return new RoomTransform(transform).GetAdjacentRooms(GetStartPosition());
    }

    public Monster PullRandomAdjacentRoomMonster()
    {
        var roomMonster = GetRandomRoomMonster();
        if (roomMonster == null)
        {
            return null;
        }

        roomMonster.Pull(this);
        new ChangeSortingLayer(roomMonster.gameObject).SetToCurrentRoom();
        return roomMonster;
    }

    public Monster PushRandomRoomMonster(Monster exception)
    {
        var roomMonster = GetRandomMonsterNot(exception);
        if (roomMonster == null)
        {
            return null;
        }

        var room = GetRandomAdjacentRoom();
        if (room == null)
        {
            return null;
        }

        roomMonster.Push(GetRandomAdjacentRoom());
        new ChangeSortingLayer(roomMonster.gameObject).SetToDefault();
        return roomMonster;
    }

    public Monster PushRoomMonster(Monster monster)
    {
        var room = GetRandomAdjacentRoom();
        if (room == null)
        {
            return null;
        }

        monster.Push(GetRandomAdjacentRoom());
        new ChangeSortingLayer(monster.gameObject).SetToDefault();
        return monster;
    }

    private Monster GetRandomRoomMonster()
    {
        return new RandomMonster(GetAdjacentRooms()).Get();
    }

    public bool HasAdjacentRoom() { return GetAdjacentRooms().Count != 0; }

    public bool HasNoAdjacentRoom() { return !HasAdjacentRoom(); }

    private Room GetRandomAdjacentRoom()
    {
        var adjacentRooms = GetAdjacentRooms();
        if (HasNoAdjacentRoom())
        {
            return null;
        }

        return adjacentRooms[Random.Range(0, adjacentRooms.Count)];
    }

    public void UnlockAdjacentRooms()
    {
        var adjacentRooms = GetAdjacentRooms();
        if (HasNoAdjacentRoom())
        {
            return;
        }

        foreach(var room in adjacentRooms)
        {
            UnlockMonsters(room.monsters);
        }
    }

    public void UnlockMonsters(List<Monster> monstersToUnlock)
    {
        foreach (var monster in monstersToUnlock)
        {
            monster.Unlock();
        }
    }

    public void SpawnRandomCopyNot(Monster monster)
    {
        var rngMonster = GetRandomMonsterNot(monster);
        if (rngMonster == null)
        {
            return;
        }

        var copy = SpawnCopy(rngMonster.cardInfo);
        copy.SetMaxStats(1, 1);
    }
}
