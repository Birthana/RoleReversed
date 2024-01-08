using UnityEngine;

public class OptionInfo : ScriptableObject
{
    public int cost;
    public Sprite sprite;
    public string description;

    public virtual void Choose()
    {
        FindObjectOfType<PlayerSoulCounter>().DecreaseSouls(cost);
        FindObjectOfType<SoulShop>().CloseShop();
    }
}
