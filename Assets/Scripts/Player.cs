using System.Collections;
using UnityEngine;

public class Player : Character
{
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
}
