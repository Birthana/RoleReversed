using UnityEngine;

public class RerollButton : MonoBehaviour
{
    private SoulShop soulShop;

    private void Awake()
    {
        soulShop = FindObjectOfType<SoulShop>();
    }

    private void OnMouseDown()
    {
        soulShop.Reroll();
    }
}
