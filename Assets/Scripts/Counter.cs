using System;
using UnityEngine;

[RequireComponent(typeof(BasicUI))]
public class Counter : MonoBehaviour
{
    public event Action<int> OnCountChange;
    public int maxCount;
    private int currentCount;

    private void Awake()
    {
        OnCountChange += GetComponent<BasicUI>().Display;
        ResetMaxCount();
    }

    public int GetCount() { return currentCount; }

    public void ResetMaxCount() { SetCount(maxCount); }

    public void SetMaxCount(int newMaxCount)
    {
        maxCount = newMaxCount;
        ResetMaxCount();
    }

    public void IncreaseMaxCount(int increase) { SetMaxCount(maxCount + increase); }

    public void DecreaseMaxCount(int decrease) { SetMaxCount(maxCount - decrease); }

    public void IncreaseCount(int increase) { SetCount(Mathf.Max(0, currentCount + increase)); }
    
    public void DecreaseCount(int decrease) { SetCount(Mathf.Max(0, currentCount - decrease)); }

    private void SetCount(int count)
    {
        currentCount = count;
        OnCountChange?.Invoke(currentCount);
    }
}
