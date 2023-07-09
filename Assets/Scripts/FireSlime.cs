using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlime : Monster
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
