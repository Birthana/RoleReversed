using UnityEngine;

[CreateAssetMenu(fileName = "GraySlime", menuName = "CardInfo/GraySlime")]
public class GraySlime : MonsterCardInfo
{
    public override void Exit()
    {
        var card = FindObjectOfType<CardManager>().CreateRandomCard();
        FindObjectOfType<Hand>().Add(card);
    }
}
