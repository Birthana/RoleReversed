using UnityEngine;

[CreateAssetMenu(fileName = "RedBrownSlime", menuName = "CardInfo/Slime/RedBrownSlime")]
public class RedBrownSlime : MonsterCardInfo
{
    public override void Exit(Monster self)
    {
        var randomMonster = self.GetCurrentRoom().GetRandomMonster();
        if (randomMonster != null)
        {
            self.SpawnExitIcon();
            randomMonster.IncreaseStats(1, 1);
        }
    }
}
