using UnityEngine;
using System.Collections;

public class Monster : Character
{
    public bool isTemporary = false;

    public override void Awake()
    {
        base.Awake();
        Entrance();
    }

    public virtual void Entrance()
    {

    }

    public virtual void Engage()
    {

    }

    public virtual void Exit()
    {

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
