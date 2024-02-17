using UnityEngine;

[CreateAssetMenu(fileName = "GraySlime", menuName = "CardInfo/GraySlime")]
public class GraySlime : MonsterCardInfo
{
    public override void Exit(Character self)
    {
        FindObjectOfType<Deck>().DrawCardToHand();
    }
}
