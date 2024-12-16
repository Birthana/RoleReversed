using System.Collections.Generic;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    public RectTransform canvas;
    public RectTransform boosterPositionCenter;
    public BoosterButton boosterPrefab;
    public float xOffset;
    public float y1Offset;
    public float y2Offset;
    private List<Vector2> positions = new List<Vector2>();
    private int count = 0;

    private void Awake()
    {
        positions.Add(new Vector2(-xOffset, -y1Offset));
        positions.Add(new Vector2(xOffset, -y1Offset));
        positions.Add(new Vector2(-xOffset, -y2Offset));
        positions.Add(new Vector2(xOffset, -y2Offset));
    }

    public void SpawnBooster(BoosterInfo info)
    {
        var booster = Instantiate(boosterPrefab, canvas);
        booster.Setup(info);
        booster.GetComponent<RectTransform>().anchoredPosition = GetOffsetPosition();
        count++;
    }

    private Vector2 GetOffsetPosition()
    {
        var offset = positions[count];
        var position = boosterPositionCenter.anchoredPosition + offset;
        return position;
    }
}
