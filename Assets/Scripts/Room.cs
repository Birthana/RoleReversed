using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Capacity))]
public class Room : MonoBehaviour
{
    public List<Monster> monsters = new List<Monster>();
    public Vector2 MONSTER_OFFSET = new Vector2(2, 0);
    public float SPACING;
    public float HORIZONTAL_SPACING;
    public float VERTICAL_SPACING;
    private Capacity capacity;

    private void Awake()
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

            yield return new WaitForSeconds(0.5f);
            monster.Attack(character);
        }
    }

    public bool HasCapacity() { return capacity.HasCapacity(); }

    public void ReduceCapacity(int decrease) { capacity.DecreaseCapacity(decrease); }

    public void Add(Monster monster)
    {
        monsters.Add(monster);
        DisplayMonsters();
    }

    private void DisplayMonsters()
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].transform.localPosition = CalcPositionAt(i);
        }
    }

    private Vector3 CalcPositionAt(int cardIndex)
    {
        float positionOffset = CalcPositionOffsetAt(cardIndex);
        return new Vector3(CalcX(), CalcY(positionOffset), CalcZ());
    }

    float CalcPositionOffsetAt(int index) { return index - ((float)monsters.Count - 1) / 2; }

    float CalcX() { return MONSTER_OFFSET.x; }

    float CalcY(float positionOffset) { return Mathf.Sin(positionOffset * Mathf.Deg2Rad) * SPACING * 10; }

    float CalcZ() { return 0; }

    public void Remove(Monster monster)
    {
        if (monster.isTemporary)
        {
            monsters.Remove(monster);
            Destroy(monster.gameObject);
            DisplayMonsters();
            return;
        }

        monster.gameObject.SetActive(false);
        monster.Exit();
    }

    public Monster GetRandomMonster()
    {
        Monster monster = null;
        do
        {
            var rngIndex = Random.Range(0, monsters.Count);
            if (!monsters[rngIndex].IsDead())
            {
                monster = monsters[rngIndex];
            }
        } while (monster == null);

        return monster;
    }

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
}
