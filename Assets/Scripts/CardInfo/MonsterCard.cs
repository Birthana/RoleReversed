using UnityEngine;
using TMPro;

public class MonsterCard : Card
{
    [SerializeField] private MonsterCardUI cardUI;
    private Transform selectedRoom;
    private MonsterCardInfo monsterCardInfo;

    private MonsterCardUI GetCardUI()
    {
        if (cardUI == null)
        {
            cardUI = GetComponent<MonsterCardUI>();
        }

        return cardUI;
    }

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        monsterCardInfo = (MonsterCardInfo)newCardInfo;
        GetCardUI().SetCardInfo(monsterCardInfo);
    }

    public override CardInfo GetCardInfo()
    {
        return monsterCardInfo;
    }

    public override int GetCost()
    {
        return monsterCardInfo.cost;
    }

    public override string GetName()
    {
        return monsterCardInfo.cardName;
    }

    public override bool HasTarget()
    {
        if (Mouse.IsOnRoom())
        {
            selectedRoom = Mouse.GetHitTransform();
            if (selectedRoom.GetComponent<Room>().HasCapacity())
            {
                return true;
            }
        }

        return false;
    }

    public override void Cast()
    {
        SpawnMonster();
        base.Cast();
    }

    private void SpawnMonster()
    {
        var room = selectedRoom.GetComponent<Room>();
        room.SpawnMonster(monsterCardInfo);
    }
}
