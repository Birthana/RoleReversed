using System;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public event Action<int> OnCountChange;
    public int maxCount;
    public BasicUI ui;
    private int currentCount;

    protected virtual void Awake()
    {
        if (ui == null)
        {
            return;
        }

        OnCountChange += ui.Display;
        ResetMaxCount();
    }

    public int GetCount() { return currentCount; }

    protected void ResetMaxCount() { SetCount(maxCount); }

    protected void SetMaxCount(int newMaxCount)
    {
        maxCount = newMaxCount;
        ResetMaxCount();
    }

    protected void IncreaseMaxCount(int increase) { SetMaxCount(maxCount + increase); }

    protected void IncreaseMaxCountWithoutReset(int increase)
    {
        maxCount += increase;
        IncreaseCount(increase);
    }

    protected void DecreaseMaxCount(int decrease) { SetMaxCount(Mathf.Max(0, maxCount - decrease)); }

    protected void DecreaseMaxCountWithoutReset(int decrease)
    {
        maxCount -= decrease;
        DecreaseCount(decrease);
    }

    protected void IncreaseCount(int increase) { SetCount(Mathf.Max(0, currentCount + increase)); }
    
    public void DecreaseCount(int decrease) { SetCount(Mathf.Max(0, currentCount - decrease)); }

    private void SetCount(int count)
    {
        currentCount = count;
        OnCountChange?.Invoke(currentCount);
    }
}
