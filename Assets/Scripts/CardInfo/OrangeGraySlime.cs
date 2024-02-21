using UnityEngine;

[CreateAssetMenu(fileName = "OrangeGraySlime", menuName = "CardInfo/OrangeGraySlime")]
public class OrangeGraySlime : MonsterCardInfo
{
    public override void Entrance(Character self)
    {
        var room = self.GetComponentInParent<Room>();
        var monster = room.GetRandomMonster();
        FindObjectOfType<Deck>().DrawSpecificCardToHand(monster.cardInfo);
    }
}
