using UnityEngine;

[CreateAssetMenu(fileName = "GoblinLumberjack", menuName = "CardInfo/Goblin/GoblinLumberjack")]
public class GoblinLumberjack : MonsterCardInfo
{
    private Monster monsterSelf;

    public override void Global(Monster self)
    {
        monsterSelf = self;
        FindObjectOfType<GlobalEffects>().AddToDraftExit(OnDraftExitBuildRoom);
    }

    public void OnDraftExitBuildRoom(Card card)
    {
        var cardInfo = card.GetCardInfo();
        if (cardInfo is MonsterCardInfo || monsterSelf == null || monsterSelf.IsDead())
        {
            return;
        }

        var randomSpace = FindObjectOfType<SpaceManager>().GetRandomAdjacentSpace(monsterSelf.GetCurrentRoom().transform.position);
        if (randomSpace == null)
        {
            return;
        }

        var roomCard = (RoomCard)card;
        roomCard.CastForFreeAt(randomSpace);
    }
}
