using UnityEngine;

[CreateAssetMenu(fileName = "RatShaman", menuName = "CardInfo/Rat/RatShaman")]
public class RatShaman : MonsterCardInfo
{
    private Monster monsterSelf;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToHandEngage(OnHandEngagePlayCard);
    }

    public void OnHandEngagePlayCard(Monster self, Card cardSelf)
    {
        var room = monsterSelf.GetCurrentRoom();
        if (room.HasCapacity())
        {
            ((MonsterCard)cardSelf).CastForFreeAt(room);
        }
    }
}
