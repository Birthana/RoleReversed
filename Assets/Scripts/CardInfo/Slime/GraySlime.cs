using UnityEngine;

[CreateAssetMenu(fileName = "GraySlime", menuName = "CardInfo/Slime/GraySlime")]
public class GraySlime : MonsterCardInfo
{
    public override void Exit(Monster self)
    {
        FindObjectOfType<EffectIcons>().SpawnExitIcon(self.GetCurrentPosition());
        FindObjectOfType<Deck>().DrawCardToHand();
    }
}
