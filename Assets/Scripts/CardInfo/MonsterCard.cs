using System.Collections;
using UnityEngine;

public class MonsterCard : Card
{
    private MonsterCardUI cardUI;
    private Transform selectedRoom;
    private MonsterCardInfo monsterCardInfo;
    private Coroutine currentAnim;

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

    public void IncreaseStats(int damage, int health)
    {
        monsterCardInfo.damage += damage;
        monsterCardInfo.health += health;
        GetCardUI().SetCardInfo(monsterCardInfo);
    }

    public void ReduceCost(int cost)
    {
        monsterCardInfo.cost = Mathf.Max(0, monsterCardInfo.cost - cost);
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
        GetHand().Remove(this);
        SpawnMonster();
        base.Cast();
    }

    public void CastForFreeAt(Room room)
    {
        selectedRoom = room.transform;
        GetHand().Remove(this);
        FindObjectOfType<Drop>().Add(GetCardInfo());
        SpawnMonster();
        DestroyImmediate(gameObject);
    }

    private void SpawnMonster()
    {
        var room = selectedRoom.GetComponent<Room>();
        room.SpawnMonster(monsterCardInfo);
    }

    public void PlayChosenAnim()
    {
        if (currentAnim != null)
        {
            StopAllCoroutines();
        }

        currentAnim = StartCoroutine(PlayChosenAnimation());
    }

    public IEnumerator PlayChosenAnimation()
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
        currentAnim = null;
    }

    public void MakeHandAttack(EffectInput input)
    {
        PlayChosenAnim();
        input.player.TakeDamage(this);
        input.monster = null;
        input.card = this;
        FindObjectOfType<GlobalEffects>().HandEngage(input);
    }
}
