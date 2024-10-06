public class BattleCard : Card
{
    private BattleCardInfo battleCardInfo;

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        battleCardInfo = (BattleCardInfo)newCardInfo;
    }

    public override CardInfo GetCardInfo()
    {
        return battleCardInfo;
    }

    public override int GetCost()
    {
        return battleCardInfo.cost;
    }

    public override string GetName()
    {
        return battleCardInfo.cardName;
    }

    public override bool HasTarget()
    {
        //if (Mouse.IsOnBattleDeck())
        //{
            //return true;
        //}
        return false;
    }

    public override void Cast()
    {
        StartCoroutine(battleCardInfo.Play());
        base.Cast();
    }

}
