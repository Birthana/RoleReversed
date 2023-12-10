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

    public void SetupStats(int damage, int health)
    {
        GetComponent<Damage>().maxCount = damage;
        GetComponent<Damage>().ResetDamage();
        GetComponent<Health>().maxCount = health;
        GetComponent<Health>().RestoreFullHealth();
    }

    public void Entrance()
    {
        if(cardInfo != null)
        {
            cardInfo.Entrance();
        }
    }

    public void Engage()
    {
        if (cardInfo != null)
        {
            cardInfo.Engage();
        }
    }

    public void Exit()
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
