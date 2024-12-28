using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    public static readonly float HORIZONTAL_SPACING = 5.0f;
    public static readonly float VERTICAL_SPACING = 5.0f;

    private List<SpaceToBuild> spaces = new List<SpaceToBuild>();
    private List<BonusSpace> bonusSpaces = new List<BonusSpace>();
    public SpaceToBuild spacePrefab;
    public BonusSpace spaceBonusPrefab;
    public Vector2 numberOfSpaces;
    public List<SpaceInfo> spaceInfos = new List<SpaceInfo>();

    private void Start()
    {
        SetupSpaces();
        SpawnBonusSpaces();
        var centerPosition = GetCenterPosition();
        ShowSpace(centerPosition);
    }

    private Vector2 GetCenterPosition()
    {
        return new Vector2((int)numberOfSpaces.x / 2 * HORIZONTAL_SPACING,
                           (int)numberOfSpaces.y / 2 * VERTICAL_SPACING);
    }

    public int GetCount() { return spaces.Count; }

    public SpaceToBuild GetRandomAdjacentSpace(Vector3 position)
    {
        foreach (var space in spaces)
        {
            if (!space.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (!IsAdjacent(position, space.gameObject.transform.position))
            {
                continue;
            }

            return space;
        }

        foreach (var space in bonusSpaces)
        {
            if (!space.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (!IsAdjacent(position, space.gameObject.transform.position))
            {
                continue;
            }

            return space;
        }

        return null;
    }

    private bool IsAdjacent(Vector3 currentRoom, Vector3 space)
    {
        if ((currentRoom.x + HORIZONTAL_SPACING == space.x && currentRoom.y == space.y) ||
            (currentRoom.x - HORIZONTAL_SPACING == space.x && currentRoom.y == space.y) ||
            (currentRoom.x == space.x && currentRoom.y + VERTICAL_SPACING == space.y) ||
            (currentRoom.x == space.x && currentRoom.y - VERTICAL_SPACING == space.y))
        {
            return true;
        }

        return false;
    }

    public void SetupSpaces()
    {
        for (int i = 0; i < numberOfSpaces.x; i++)
        {
            for (int j = 0; j < numberOfSpaces.y; j++)
            {
                var position = new Vector2(i * HORIZONTAL_SPACING, j * VERTICAL_SPACING);
                var newSpace = SpawnSpace(spacePrefab, position);
                newSpace.gameObject.SetActive(false);
                spaces.Add(newSpace);
            }
        }
    }

    private void ShowSpace(Vector2 position)
    {
        foreach (var space in spaces)
        {
            if (space.transform.localPosition == new Vector3(position.x, position.y, 0.0f))
            {
                space.gameObject.SetActive(true);
            }
        }
    }

    private SpaceToBuild SpawnSpace(SpaceToBuild prefab, Vector2 position)
    {
        var space = Instantiate(prefab, transform);
        space.transform.localPosition = position;
        return space;
    }

    public void RemoveSpace(Vector2 position)
    {
        RemoveSpaceAt(position);
        RemoveBonusSpaceAt(position);
        ShowSpace(new Vector2(position.x + HORIZONTAL_SPACING, position.y));
        ShowSpace(new Vector2(position.x - HORIZONTAL_SPACING, position.y));
        ShowSpace(new Vector2(position.x, position.y + VERTICAL_SPACING));
        ShowSpace(new Vector2(position.x, position.y - VERTICAL_SPACING));
    }

    private void RemoveSpaceAt(Vector2 position)
    {
        foreach (var space in spaces)
        {
            if (space.transform.localPosition == new Vector3(position.x, position.y, 0.0f))
            {
                spaces.Remove(space);
                DestroyImmediate(space.gameObject);
                return;
            }
        }
    }

    private void RemoveBonusSpaceAt(Vector2 position)
    {
        foreach (var bonusSpace in bonusSpaces)
        {
            if (bonusSpace.transform.localPosition == new Vector3(position.x, position.y, 0.0f))
            {
                bonusSpaces.Remove(bonusSpace);
                bonusSpace.BuildEffect();
                DestroyImmediate(bonusSpace.gameObject);
                return;
            }
        }
    }

    private void SpawnBonusSpaces()
    {
        for (int i = 0; i < spaceInfos.Count; i++)
        {
            var position = GetRandomPosition();
            var bonusSpace = (BonusSpace)SpawnSpace(spaceBonusPrefab, position);
            bonusSpace.Setup(spaceInfos[i]);
            bonusSpaces.Add(bonusSpace);
        }
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 position;
        do
        {
            position = new Vector2(Random.Range(0, (int)numberOfSpaces.x) * HORIZONTAL_SPACING,
                                   Random.Range(0, (int)numberOfSpaces.y) * VERTICAL_SPACING);
        } while (SpaceIsInvalid(position));

        return position;
    }

    private bool SpaceIsInvalid(Vector2 position)
    {
        var centerPosition = GetCenterPosition();
        return BonusSpaceExists(position) || position == centerPosition ||
               position == new Vector2(centerPosition.x + HORIZONTAL_SPACING, centerPosition.y) ||
               position == new Vector2(centerPosition.x - HORIZONTAL_SPACING, centerPosition.y) ||
               position == new Vector2(centerPosition.x, centerPosition.y + VERTICAL_SPACING) ||
               position == new Vector2(centerPosition.x, centerPosition.y - VERTICAL_SPACING);
    }

    private bool BonusSpaceExists(Vector2 position)
    {
        foreach (var bonusSpace in bonusSpaces)
        {
            if (bonusSpace.transform.localPosition == new Vector3(position.x, position.y, 0.0f))
            {
                return true;
            }
        }

        return false;
    }
}
