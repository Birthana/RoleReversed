using UnityEngine;

[CreateAssetMenu(fileName = "BlueSlime", menuName = "CardInfo/Slime/BlueSlime")]
public class BlueSlime : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        self.BecomeTemp();
        self.Lock();
        FindObjectOfType<PlayerSoulCounter>().IncreaseSouls();
    }
}
