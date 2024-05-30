using System.Collections;
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

    public void PlayChosenAnim()
    {
        StartCoroutine(PlayChosenAnimation());
    }

    private IEnumerator PlayChosenAnimation()
    {
        var chosenAnimation = new DamageAnimation(GetCardUI().GetComponent<SpriteRenderer>(), Color.green, 0.1f);
        var hoverAnimation = GetComponent<HoverAnimation>();
        hoverAnimation.ResetHoverAnimation();
        yield return StartCoroutine(chosenAnimation.AnimateFromStartToEnd());
        hoverAnimation.Hover(transform, new Vector2(0, 3.0f), 0.1f);
        yield return new WaitForSeconds(0.1f);
        hoverAnimation.PerformReturn();
        yield return StartCoroutine(chosenAnimation.AnimateFromEndToStart());
        yield return new WaitForSeconds(0.1f);

    }
}
