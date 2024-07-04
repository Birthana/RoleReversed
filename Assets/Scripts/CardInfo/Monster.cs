using UnityEngine;
using System.Collections;

public class Monster : Character
{
    public bool isTemporary = false;
    public MonsterCardInfo cardInfo;
    private DamageAnimation damageAnim;
    private bool isHighlighted = false;

    public void Setup(MonsterCardInfo monsterCardInfo)
    {
        cardInfo = monsterCardInfo;
        var spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = cardInfo.fieldSprite;
        Entrance();
        Setup(cardInfo.GetDamage(), cardInfo.GetHealth());
        damageAnim = new DamageAnimation(spriteRender, Color.green, 0.01f);
    }

    public void Setup(int damage, int health)
    {
        if (cardInfo is TemporaryMonster)
        {
            isTemporary = true;
        }

        SetMaxStats(damage, health);
        GetDamageComponent().ResetDamage();
        GetHealthComponent().RestoreFullHealth();
    }

    private void SetMaxStats(int damage, int health)
    {
        GetDamageComponent().maxCount = damage;
        GetHealthComponent().maxCount = health;
    }

    public void IncreaseStats(int damage, int health)
    {
        GetDamageComponent().IncreaseMaxDamageWithoutReset(damage);
        GetHealthComponent().IncreaseMaxHealthWithoutReset(health);
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
            cardInfo.Engage(this, null);
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

    public bool IsHighlighted() { return isHighlighted; }

    public void Highlight()
    {
        if (!damageAnim.IsAtStart())
        {
            return;
        }

        isHighlighted = true;
        StartCoroutine(damageAnim.AnimateFromStartToEnd());
    }

    public void UnHighlight()
    {
        StartCoroutine(damageAnim.AnimateFromEndToStart());
        isHighlighted = false;
    }
}
