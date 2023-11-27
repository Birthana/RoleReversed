using UnityEngine;

[CreateAssetMenu(fileName = "RedSlime", menuName = "CardInfo/RedSlime")]
public class RedSlime : MonsterCardInfo
{
    public override void Exit()
    {
        FindObjectOfType<Player>().TakeDamage(2);
        if (FindObjectOfType<Player>().IsDead())
        {
            Debug.Log($"Player is Dead.");
            FindObjectOfType<GameManager>().ResetPlayer();
        }
    }
}
