using UnityEngine;
using System.Collections;

public class Monster : Character
{
    public bool isTemporary = false;
    public MonsterCardInfo cardInfo;

    public override void Awake()
    {
        base.Awake();
    }

    public void Setup(MonsterCardInfo monsterCardInfo)
    {
        cardInfo = monsterCardInfo;
        GetComponent<SpriteRenderer>().sprite = cardInfo.fieldSprite;
        Entrance();
        Setup(cardInfo.GetDamage(), cardInfo.GetHealth());
    }

    public void Setup(int damage, int health)
    {
        if (cardInfo is TemporaryMonster)
        {
            isTemporary = true;
        }

        GetComponent<Damage>().maxCount = damage;
        GetComponent<Damage>().ResetDamage();
        GetComponent<Health>().maxCount = health;
        GetComponent<Health>().RestoreFullHealth();
    }

    public void Entrance()
    {
        if(cardInfo != null)
        {
            cardInfo.Entrance(this);
        }
    }

    public void Engage()
    {
        if (cardInfo != null)
        {
            cardInfo.Engage(this);
        }
    }

    public void Exit()
    {
        if (cardInfo != null)
        {
            cardInfo.Exit(this);
        }
    }

    public IEnumerator Attack(Character character)
    {
        yield return PlayAttackAnimation();
        character.TakeDamage(GetDamage());
        var spriteRender = character.GetComponent<SpriteRenderer>();
        if(spriteRender != null)
        {
            var damageAnimation = new DamageAnimation(spriteRender, Color.red, 0.1f);
            yield return StartCoroutine(damageAnimation.AnimateFromStartToEnd());
            yield return new WaitForSeconds(0.1f);
            yield return StartCoroutine(damageAnimation.AnimateFromEndToStart());
        }

        Engage();
        if (character.IsDead())
        {
            FindObjectOfType<GameManager>().ResetPlayer();
            yield break;
        }
    }

    private IEnumerator PlayAttackAnimation()
    {
        var hoverAnimation = GetComponent<HoverAnimation>();
        hoverAnimation.ResetHoverAnimation();
        hoverAnimation.Hover(transform, new Vector2(-0.5f, 0), 0.1f);
        yield return new WaitForSeconds(0.1f);
        hoverAnimation.PerformReturn();
        yield return new WaitForSeconds(0.1f);
    }
}
