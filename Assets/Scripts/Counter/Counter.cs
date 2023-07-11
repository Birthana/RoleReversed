using System;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public event Action<int> OnCountChange;
    public int maxCount;
    public BasicUI ui;
    private int currentCount;

    private void Awake()
    {
        OnCountChange += ui.Display;
        ResetMaxCount();
    }

    protected int GetCount() { return currentCount; }

    protected void ResetMaxCount() { SetCount(maxCount); }

    protected void SetMaxCount(int newMaxCount)
    {
        maxCount = newMaxCount;
        ResetMaxCount();
    }

    protected void IncreaseMaxCount(int increase) { SetMaxCount(maxCount + increase); }

    protected void DecreaseMaxCount(int decrease) { SetMaxCount(maxCount - decrease); }

    protected void IncreaseCount(int increase) { SetCount(Mathf.Max(0, currentCount + increase)); }
    
    protected void DecreaseCount(int decrease) { SetCount(Mathf.Max(0, currentCount - decrease)); }

    private void SetCount(int count)
    {
        currentCount = count;
        OnCountChange?.Invoke(currentCount);
    }
}
