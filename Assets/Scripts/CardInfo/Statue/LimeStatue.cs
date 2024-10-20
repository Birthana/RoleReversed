using UnityEngine;

[CreateAssetMenu(fileName = "LimeStatue", menuName = "CardInfo/Statue/LimeStatue")]
public class LimeStatue : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        var parentRoom = self.GetCurrentRoom();
        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        var monsters = parentRoom.GetComponentsInChildren<Monster>();
        foreach (var monster in monsters)
        {
            if (monster == self)
            {
                continue;
            }

            monster.TemporaryIncreaseStats(0, 2);
        }
    }
}
