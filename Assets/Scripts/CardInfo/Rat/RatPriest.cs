using UnityEngine;

[CreateAssetMenu(fileName = "RatPriest", menuName = "CardInfo/Rat/RatPriest")]
public class RatPriest : MonsterCardInfo
{
    public override void Engage(EffectInput input)
    {
        if (input.player.IsDead())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnEngageIcon(input.position);
        var roomMonsters = input.room.GetComponentsInChildren<Monster>();
        foreach (var monster in roomMonsters)
        {
            monster.TemporaryIncreaseStats(0, 1);
        }
    }
}
