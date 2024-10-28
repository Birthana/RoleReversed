using UnityEngine;

[CreateAssetMenu(fileName = "BlueSlime", menuName = "CardInfo/Slime/BlueSlime")]
public class BlueSlime : MonsterCardInfo
{
    public override void Entrance(Monster self)
    {
        FindObjectOfType<EffectIcons>().SpawnEntranceIcon(self.GetCurrentPosition());
        var card = FindObjectOfType<Deck>().DrawCardToHand();
        if (card == null)
        {
            return;
        }

        if (card.GetCardInfo().IsSlime())
        {
            FindObjectOfType<ActionManager>().AddActions(2);
        }
    }
}
