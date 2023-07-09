using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Player : Character
{
    public event Action<int> OnRespawn;
    private int timesDied = 0;

    public override void Awake()
    {
        OnRespawn += FindObjectOfType<GameManager>().respawnCounter.GetComponent<BasicUI>().Display;
        OnRespawn?.Invoke(timesDied);
        base.Awake();
    }

    public IEnumerator MakeAttack(Character character)
    {
        GetComponent<Animator>().Play("Player");
        character.TakeDamage(GetDamage());
        if (character.IsDead())
        {
            transform.parent.GetComponent<Room>().Remove(character as Monster);
        }

        yield return new WaitForSeconds(0.25f);
        GetComponent<Animator>().Play("Player_Idle");
    }

    public void GetStronger()
    {
        timesDied++;
        OnRespawn?.Invoke(timesDied);
        int numberOfBuffs = (timesDied / 2) + 1;
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
