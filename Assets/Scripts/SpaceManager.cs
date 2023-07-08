using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    public GameObject spacePrefab;
    public Vector2 numberOfSpaces;
    public float HORIZONTAL_SPACING;
    public float VERTICAL_SPACING;

    private void Start()
    {
        for (int i = 0; i < numberOfSpaces.x; i++)
        {
            for (int j = 0; j < numberOfSpaces.y; j++)
            {
                var space = Instantiate(spacePrefab, transform);
                space.transform.localPosition = new Vector2(i * HORIZONTAL_SPACING, j * VERTICAL_SPACING);
            }
        }
    }
}
