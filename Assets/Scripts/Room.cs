using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Monster> monsters = new List<Monster>();
    public int maxCapacity;
    private int currentCapacity;
    public Vector2 MONSTER_OFFSET = new Vector2(2, 0);
    public float SPACING;
    public float HORIZONTAL_SPACING;
    public float VERTICAL_SPACING;

    private void Awake()
    {
        currentCapacity = maxCapacity;
    }

    public IEnumerator MakeAttack(Character character)
    {
        foreach (var monster in monsters)
        {
            if (monster.IsDead())
            {
                continue;
            }

            yield return new WaitForSeconds(1.0f);
            monster.Attack(character);
        }
    }

    public bool HasCapacity(int cost) { return currentCapacity >= cost; }

    public void ReduceCapacity(int cost) { currentCapacity -= cost; }

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
        monster.gameObject.SetActive(false);
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
        var adjacentRooms = GetAdjacentRooms();
        var rngIndex = Random.Range(0, adjacentRooms.Count);
        return adjacentRooms[rngIndex];
    }

    private List<Room> GetAdjacentRooms()
    {
        var adjacentRooms = new List<Room>();
        foreach (var position in GetAdjacentPositions())
        {
            Debug.Log($"Check {position}");
            var ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(position));
            var hit = Physics2D.Raycast(ray.origin, Vector2.zero, 100, 1 << LayerMask.NameToLayer("Room"));
            if (hit)
            {
                Debug.Log($"Found a Room.");
                adjacentRooms.Add(hit.transform.GetComponent<Room>());
            }
        }

        return adjacentRooms;
    }

    private List<Vector3> GetAdjacentPositions()
    {
        var adjacentPositions = new List<Vector3>();
        adjacentPositions.Add(transform.position + new Vector3(HORIZONTAL_SPACING, 0, -10));
        adjacentPositions.Add(transform.position + new Vector3(-HORIZONTAL_SPACING, 0, -10));
        adjacentPositions.Add(transform.position + new Vector3(0, VERTICAL_SPACING, -10));
        adjacentPositions.Add(transform.position + new Vector3(0, -VERTICAL_SPACING, -10));
        return adjacentPositions;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(HORIZONTAL_SPACING, 0, 0), Vector3.one);
        Gizmos.DrawWireCube(transform.position + new Vector3(-HORIZONTAL_SPACING, 0, 0), Vector3.one);
        Gizmos.DrawWireCube(transform.position + new Vector3(0, VERTICAL_SPACING, 0), Vector3.one);
        Gizmos.DrawWireCube(transform.position + new Vector3(0, -VERTICAL_SPACING, 0), Vector3.one);
    }
}
