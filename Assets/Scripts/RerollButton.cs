using UnityEngine;

public class RerollButton : MonoBehaviour
{
    private int rerollCost = 0;
    private SoulShop soulShop;

    private void Awake()
    {
        soulShop = FindObjectOfType<SoulShop>();
        SetCost(1);
    }

    private void OnMouseDown()
    {
        soulShop.Reroll();
    }

    public int GetCost() { return rerollCost; }

    public void SetCost(int cost)
    {
        rerollCost = cost;
        GetComponent<BasicUI>().Display(cost);
    }
}
