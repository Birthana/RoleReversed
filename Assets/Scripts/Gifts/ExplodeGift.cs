using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplodeGift", menuName = "RoomateEffect/Explode")]
public class ExplodeGift : RoommateEffectInfo
{
    public override IEnumerator BattleStart(Room room)
    {
        var player = FindObjectOfType<Player>();
        player.TakeDamage(2);
        if (player.IsDead())
        {
            FindObjectOfType<GameManager>().ResetPlayer();
            yield break;
        }

        yield return null;
    }
}
