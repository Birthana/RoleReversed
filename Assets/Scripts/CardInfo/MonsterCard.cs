using UnityEngine;
using TMPro;

public class MonsterCard : Card
{
    private MonsterCardUI cardUI;
    private Transform selectedRoom;
    private MonsterCardInfo monsterCardInfo;

    private MonsterCardUI GetCardUI()
    {
        if (cardUI == null)
        {
            cardUI = (MonsterCardUI)Instantiate(monsterCardInfo.GetCardUI(), transform);
            cardUI.SetCardInfo(monsterCardInfo);
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
