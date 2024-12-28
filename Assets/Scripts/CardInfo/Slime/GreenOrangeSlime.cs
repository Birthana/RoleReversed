using UnityEngine;

[CreateAssetMenu(fileName = "GreenOrangeSlime", menuName = "CardInfo/Slime/GreenOrangeSlime")]
public class GreenOrangeSlime : MonsterCardInfo
{
    public TemporaryMonster tempMonsterCardInfo;

    public override void Exit(Monster self)
    {
        FindObjectOfType<EffectIcons>().SpawnExitIcon(self.GetCurrentPosition());
        var monster = self.GetCurrentRoom().SpawnTemporaryMonster(tempMonsterCardInfo);
        var numberOfDeckCards = FindObjectOfType<Deck>().GetSize();
        monster.IncreaseStats(numberOfDeckCards, numberOfDeckCards);
    }
}
