using System;
using System.Collections;
using UnityEngine;

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

        var healthBar = FindObjectOfType<HealthBar>();
        GetHealthComponent().OnHealthChange += healthBar.Display;
        GetHealthComponent().OnCountChange += healthBar.GetHealthUI().Display;
        base.Awake();
    }

    public override IEnumerator MakeAttack(Character character)
    {
        var hoverAnimation = GetComponent<HoverAnimation>();
        hoverAnimation.ResetHoverAnimation();
        hoverAnimation.Hover(transform, new Vector2(0.5f, 0), 0.1f);
        yield return new WaitForSeconds(0.1f);
        PlayAttackSound();
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

    private void PlayAttackSound()
    {
        var audioClip = Resources.Load<AudioClip>("Music/Player_Attack");
        GetSoundManager().Play(audioClip);
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
                GainRandomHealth();
            }
        }
    }

    private void GainRandomStats()
    {
        var rngIndex = UnityEngine.Random.Range(0, 10);
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
        var rngIndex = UnityEngine.Random.Range(0, 5);
        if (rngIndex == 1)
        {
            return;
        }

        IncreaseDamage(1);
    }

    private void GainRandomHealth()
    {
        var rngIndex = UnityEngine.Random.Range(0, 10);
        if (rngIndex == 1)
        {
            health.IncreaseMaxHealth(2);
            return;
        }

        health.IncreaseMaxHealth(1);
    }
}
