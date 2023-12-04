using UnityEngine;
using System.Collections;

public class Monster : Character
{
    public bool isTemporary = false;
    public int damageStat;
    public int healthStat;
    public MonsterCardInfo cardInfo;

    public override void Awake()
    {
        base.Awake();
    }

    public void SetupStats()
    {
        GetComponent<Damage>().maxCount = damageStat;
        GetComponent<Damage>().ResetDamage();
        GetComponent<Health>().maxCount = healthStat;
        GetComponent<Health>().RestoreFullHealth();
    }

    public virtual void Entrance()
    {
        if(cardInfo != null)
        {
            cardInfo.Entrance();
        }
    }

    public virtual void Engage()
    {
        if (cardInfo != null)
        {
            cardInfo.Engage();
        }
    }

    public virtual void Exit()
    {
        if (cardInfo != null)
        {
            cardInfo.Exit();
        }
    }

    public IEnumerator Attack(Character character)
    {
        character.TakeDamage(GetDamage());
        var spriteRender = character.GetComponent<SpriteRenderer>();
        var damageAnimation = new DamageAnimation(spriteRender, Color.red, 0.1f);
        yield return StartCoroutine(damageAnimation.AnimateFromStartToEnd());
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(damageAnimation.AnimateFromEndToStart());
        if (character.IsDead())
        {
            FindObjectOfType<GameManager>().ResetPlayer();
            yield break;
        }

        Engage();
    }
}
