using UnityEngine;

[CreateAssetMenu(fileName = "TrashRat", menuName = "CardInfo/Rat/TrashRat")]
public class TrashRat : MonsterCardInfo
{
    public CardInfo ratCardInfo;

    public override void Exit(Monster self)
    {
        if (GetPlayer().IsDead())
        {
            return;
        }

        var hand = FindObjectOfType<Hand>();
        if (hand.IsFull())
        {
            return;
        }

        FindObjectOfType<EffectIcons>().SpawnExitIcon(self.GetCurrentPosition());
        var input = new EffectInput(GetPlayer(), self.GetCurrentRoom(), self.GetCurrentPosition(), self);
        hand.AddAndAttack(ratCardInfo, input);
        
        if (GetPlayer().IsDead())
        {
            FindObjectOfType<GameManager>().ResetPlayer();
            return;
        }

    }
}
