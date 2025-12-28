public class PackHand : Card
{
    private PackHandUI packHandUI;
    private PackInfo packInfo;

    private PackHandUI GetCardUI()
    {
        if (packHandUI == null)
        {
            packHandUI = (PackHandUI)Instantiate(packInfo.GetCardUI(), transform);
            packHandUI.SetCardInfo(packInfo);
        }

        return packHandUI;
    }

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        packInfo = (PackInfo)newCardInfo;
        GetCardUI().SetCardInfo(packInfo);
    }

    public override CardInfo GetCardInfo()
    {
        return packInfo;
    }

    public override bool HasTarget()
    {
        return Mouse.IsOnField();
    }

    public override void Cast()
    {
        GetHand().Remove(this);
        FindObjectOfType<DraftManager>().Draft(packInfo);
        base.Cast();
    }
}
