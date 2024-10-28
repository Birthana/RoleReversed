using UnityEngine;

[CreateAssetMenu(fileName = "PinkStatue", menuName = "CardInfo/Statue/PinkStatue")]
public class PinkStatue : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        var card = FindObjectOfType<Deck>().DrawCardToHand();
        if (card.GetCardInfo() is MonsterCardInfo)
        {
            var monsterCardInfo = (MonsterCardInfo)card.GetCardInfo();
            var monster = self.GetCurrentRoom().SpawnCopy(monsterCardInfo);
            monster.SetMaxStats(1, 1);
        }
    }
}
