using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Capacity))]
public class Room : MonoBehaviour
{
    private static readonly string FIELD_MONSTER_PREFAB = "Prefabs/FieldMonster";
    private static readonly int MAX_NUMBER_OF_MONSTERS_IN_COLUMN = 3;
    private static readonly float COLUMN_SPACING = 2.0f;
    public List<Monster> monsters = new List<Monster>();
    public Vector2 MONSTER_OFFSET = new Vector2(2, 0);
    public float SPACING;
    public float HORIZONTAL_SPACING;
    public float VERTICAL_SPACING;
    private Capacity capacity;

    public void Awake()
    {
        capacity = GetComponent<Capacity>();
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

    public void SetCapacity(int capacity)
    {
        this.capacity.maxCount = capacity;
        this.capacity.ResetMaxCapacity();
    }

    public int GetCapacity()
    {
        return capacity.GetCount();
    }

    public bool HasCapacity() { return capacity.HasCapacity(); }

    public void IncreaseCapacity(int increase) { capacity.IncreaseCapacity(increase); }
    
    public void ReduceCapacity(int decrease) { capacity.DecreaseCapacity(decrease); }

    public bool CurrentCapacityIsMaxCapacity() { return capacity.CurrentCapacityIsMaxCapcity(); }

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
        if(monsters.Count == 0)
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
        var roomTransform = new RoomTransform(transform);
        var adjacentRooms = roomTransform.GetAdjacentRooms();
        var rngIndex = Random.Range(0, adjacentRooms.Count);
        return adjacentRooms[rngIndex];
    }

    public bool IsEmpty()
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

    public void SpawnMonster(MonsterCardInfo monsterCardInfo)
    {
        CreateMonster(monsterCardInfo);
        ReduceCapacity(1);
    }

    public void SpawnTemporaryMonster(MonsterCardInfo monsterCardInfo)
    {
        var monster = CreateMonster(monsterCardInfo);
        new ChangeSortingLayer(monster.gameObject).SetToCurrentRoom();
    }

    private Monster CreateMonster(MonsterCardInfo monsterCardInfo)
    {
        var monsterPrefab = Resources.Load<Monster>(FIELD_MONSTER_PREFAB);
        var monster = Instantiate(monsterPrefab, transform);
        monster.Setup(monsterCardInfo);
        Add(monster);
        return monster;
    }
}
