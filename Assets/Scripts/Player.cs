using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Player : Character
{
    public event Action OnRespawn;
    private Respawn respawnCounter;

    public override void Awake()
    {
        respawnCounter = FindObjectOfType<Respawn>();
        if (respawnCounter != null)
        {
            OnRespawn += respawnCounter.Increment;
        }

        base.Awake();
    }

    public void MakeAttackOn(Character character)
    {
        StartCoroutine(MakeAttack(character));
    }

    public IEnumerator MakeAttack(Character character)
    {
        var hoverAnimation = GetComponent<HoverAnimation>();
        hoverAnimation.ResetHoverAnimation();
        hoverAnimation.Hover(transform, new Vector2(0.5f, 0), 0.1f);
        yield return new WaitForSeconds(0.1f);
        hoverAnimation.PerformReturn();
        yield return new WaitForSeconds(0.1f);
        yield return TakeDamage(character);
        var playerSkills = FindObjectOfType<PlayerSkillManager>();
        if(playerSkills.skills.Count != 0)
        {
            yield return playerSkills.ShowSkills(1);
        }
    }

    private IEnumerator TakeDamage(Character character)
    {
        character.TakeDamage(GetDamage());
        var spriteRender = character.GetComponent<SpriteRenderer>();
        var damageAnimation = new DamageAnimation(spriteRender, Color.red, 0.1f);
        yield return StartCoroutine(damageAnimation.AnimateFromStartToEnd());
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(damageAnimation.AnimateFromEndToStart());
        if (character.IsDead())
        {
            transform.parent.GetComponent<Room>().Remove(character as Monster);
        }
    }

    public void GetStronger()
    {
        OnRespawn?.Invoke();
        int timesDied = respawnCounter.GetNumberOfTimesDied();
        int numberOfBuffs = (timesDied / 2) + 1;
        if (timesDied % 3 == 0)
        {
            FindObjectOfType<PlayerSkillManager>().AddRandomSkill();
        }

        for (int i = 0; i < numberOfBuffs; i++)
        {
            GainRandomStats();
            if(timesDied > 4)
            {
                health.IncreaseMaxHealth(1);
            }

            if (timesDied > 9)
            {
                IncreaseDamage(1);
                health.IncreaseMaxHealth(1);
            }
        }
    }

    private void GainRandomStats()
    {
        var rngIndex = UnityEngine.Random.Range(0, 3);
        if (rngIndex == 0)
        {
            IncreaseDamage(1);
        }

        if (rngIndex == 1)
        {
            health.IncreaseMaxHealth(2);
        }

        if (rngIndex == 2)
        {
            IncreaseDamage(1);
            health.IncreaseMaxHealth(1);
        }
    }
}
