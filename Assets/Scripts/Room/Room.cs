using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private static readonly string FIELD_MONSTER_PREFAB = "Prefabs/FieldMonster";
    private static readonly int MAX_NUMBER_OF_MONSTERS_IN_COLUMN = 3;
    private static readonly int MAX_ROOM_CAPACITY = 9;
    private static readonly float COLUMN_SPACING = 2.0f;
    public List<Monster> monsters = new List<Monster>();
    public Vector2 MONSTER_OFFSET = new Vector2(2, 0);
    public float SPACING;
    public float HORIZONTAL_SPACING;
    public float VERTICAL_SPACING;
    private Capacity capacity;
    protected RoomCardInfo cardInfo;

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
        transform.position = position;
    }

    public IEnumerator MakeAttack(Character character)
    {
        foreach (var monster in monsters)
        {
            if (FindObjectOfType<Player>().IsDead())
            {
                break;
            }

            if (monster.IsDead())
            {
                continue;
            }

            yield return monster.Attack(character);
        }
    }

    public IEnumerator MakeRandomAttack(Character character)
    {
        var randomIndex = Random.Range(0, monsters.Count);
        if (monsters[randomIndex].IsDead())
        {
            yield break;
        }

        yield return monsters[randomIndex].Attack(character);
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

    public void Add(Monster monster)
    {
        monsters.Add(monster);
        DisplayMonsters();
    }

    public void Leave(Monster monster)
    {
        monsters.Remove(monster);
        DisplayMonsters();
    }

    public void DisplayMonsters()
    {
        var blockCenterPosition = new BlockCenterPosition(new Vector2(MONSTER_OFFSET.x, 0), 
                                                          monsters.Count,
                                                          MAX_NUMBER_OF_MONSTERS_IN_COLUMN,
                                                          SPACING,
                                                          COLUMN_SPACING);
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].transform.localPosition = blockCenterPosition.GetVerticalLayoutPositionAt(i);
        }
    }

    public void Remove(Monster monster)
    {
        if (monster.isTemporary)
        {
            RemoveTemporaryMonster(monster);
            return;
        }

        monster.gameObject.SetActive(false);
        monster.Exit();
    }

    private void RemoveTemporaryMonster(Monster monster)
    {
        monsters.Remove(monster);
        Destroy(monster.gameObject);
        DisplayMonsters();
    }

    public Monster GetRandomMonster()
    {
        if (IsEmpty())
        {
            return null;
        }

        return GetAliveRandomMonster();
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
            if (!monster.IsDead())
            {
                return false;
            }
        }

        return true;
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

    private Monster CreateMonster(MonsterCardInfo monsterCardInfo)
    {
        var monsterPrefab = Resources.Load<Monster>(FIELD_MONSTER_PREFAB);
        var monster = Instantiate(monsterPrefab, transform);
        monster.Setup(monsterCardInfo);
        Add(monster);
        return monster;
    }

    public IEnumerator BattleStart()
    {
        yield return cardInfo.BattleStart(this);
    }

    public void SetCardInfo(RoomCardInfo roomCardInfo)
    {
        cardInfo = roomCardInfo;
    }
}
