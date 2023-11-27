using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "CardInfo/Monster")]
public class MonsterCardInfo : CardInfo
{
    public int damage;
    public int health;

    public virtual void Entrance()
    {
    }

    public virtual void Engage()
    {
    }

    public virtual void Exit()
    {
    }
}
