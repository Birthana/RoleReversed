using System.Collections;
using UnityEngine;

public class Player : Character
{
    private int timesDied = 0;

    public override void Awake()
    {
        base.Awake();
    }

    public IEnumerator MakeAttack(Character character)
    {
        character.TakeDamage(GetDamage());
        if (character.IsDead())
        {
            transform.parent.GetComponent<Room>().Remove(character as Monster);
        }

        yield return new WaitForSeconds(0.5f);
    }

    public void GetStronger()
    {
        timesDied++;
        int numberOfBuffs = (timesDied / 2) + 1;
        for (int i = 0; i < numberOfBuffs; i++)
        {
            GainRandomStats();
        }
    }

    private void GainRandomStats()
    {
        var rngIndex = Random.Range(0, 3);
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
