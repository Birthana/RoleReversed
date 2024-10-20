using UnityEngine;

[CreateAssetMenu(fileName = "GoblinGuard", menuName = "CardInfo/Goblin/GoblinGuard")]
public class GoblinGuard : MonsterCardInfo
{
    public override void Engage(EffectInput input)
    {
        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(input.position);
        if (input.monster != null)
        {
            input.monster.Unlock();
        }
    }
}
