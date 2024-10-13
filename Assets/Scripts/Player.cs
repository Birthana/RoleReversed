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

    public override IEnumerator MakeAttack(Character character)
    {
        var hoverAnimation = GetComponent<HoverAnimation>();
        hoverAnimation.ResetHoverAnimation();
        hoverAnimation.Hover(transform, new Vector2(0.5f, 0), 0.1f);
        yield return new WaitForSeconds(0.1f);
        hoverAnimation.PerformReturn();
        yield return new WaitForSeconds(0.1f);
        yield return TakeDamage(character);

        if (IsDead())
        {
            yield break;
        }

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
            GainRandomHealth();
            return;
        }

        GainRandomDamage();
        for (int i = 0; i < numberOfBuffs; i++)
        {
            health.IncreaseMaxHealth(1);

            if (timesDied > 4)
            {
                GainRandomStats();
                health.IncreaseMaxHealth(1);
            }

            if (timesDied > 9)
            {
                GainRandomStats();
                GainRandomHealth();
            }
        }
    }

    private void GainRandomStats()
    {
        var rngIndex = UnityEngine.Random.Range(0, 10);
        if (rngIndex == 0)
        {
            GainRandomDamage();
            GainRandomDamage();
            return;
        }

        if (rngIndex == 1)
        {
            GainRandomDamage();
            GainRandomHealth();
            return;
        }

        GainRandomHealth();
    }

    private void GainRandomDamage()
    {
        var rngIndex = UnityEngine.Random.Range(0, 4);
        if (rngIndex == 1)
        {
            return;
        }

        IncreaseDamage(1);
    }

    private void GainRandomHealth()
    {
        var rngIndex = UnityEngine.Random.Range(0, 10);
        if (rngIndex == 1 || rngIndex == 2)
        {
            health.IncreaseMaxHealth(2);
            return;
        }

        health.IncreaseMaxHealth(1);
    }
}
