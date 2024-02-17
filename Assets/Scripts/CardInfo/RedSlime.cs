using UnityEngine;

[CreateAssetMenu(fileName = "RedSlime", menuName = "CardInfo/RedSlime")]
public class RedSlime : MonsterCardInfo
{
    public override void Exit(Character self)
    {
        FindObjectOfType<Player>().TakeDamage(2);
        if (FindObjectOfType<Player>().IsDead())
        {
            FindObjectOfType<GameManager>().ResetPlayer();
        }
    }
}
