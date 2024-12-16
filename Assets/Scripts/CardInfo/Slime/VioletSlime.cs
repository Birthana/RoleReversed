using UnityEngine;

[CreateAssetMenu(fileName = "VioletSlime", menuName = "CardInfo/Slime/VioletSlime")]
public class VioletSlime : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        self.BecomeTemp();
        self.Lock();
        FindObjectOfType<ActionManager>().AddActions(2);
    }
}
