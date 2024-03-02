using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    [SerializeField] private List<Vector3> spaces = new List<Vector3>();
    public GameObject spacePrefab;
    public Vector2 numberOfSpaces;
    public float HORIZONTAL_SPACING;
    public float VERTICAL_SPACING;

    private void Start()
    {
        SetupSpaces();
        var centerPosition = new Vector2((int)numberOfSpaces.x / 2 * HORIZONTAL_SPACING,
                                         (int)numberOfSpaces.y / 2 * VERTICAL_SPACING);
        SpawnSpace(centerPosition);
    }

    public int GetCount() { return spaces.Count; }

    public void SetupSpaces()
    {
        for (int i = 0; i < numberOfSpaces.x; i++)
        {
            for (int j = 0; j < numberOfSpaces.y; j++)
            {
                spaces.Add(new Vector2(i * HORIZONTAL_SPACING, j * VERTICAL_SPACING));
            }
        }
    }

    public void SpawnSpace(Vector2 position)
    {
        if (!spaces.Contains(position))
        {
            return;
        }

        spaces.Remove(position);
        var space = Instantiate(spacePrefab, transform);
        space.transform.localPosition = position;
    }

    public void SpawnSpaces(Vector2 position)
    {
        SpawnSpace(new Vector2(position.x + HORIZONTAL_SPACING, position.y));
        SpawnSpace(new Vector2(position.x - HORIZONTAL_SPACING, position.y));
        SpawnSpace(new Vector2(position.x, position.y + VERTICAL_SPACING));
        SpawnSpace(new Vector2(position.x, position.y - VERTICAL_SPACING));
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var space in spaces)
        {
            Gizmos.DrawSphere(space + transform.position, 0.5f);
        }
    }
}
