using UnityEngine;

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

    public void Attack(Character character)
    {
        character.TakeDamage(GetDamage());
        if (character.IsDead())
        {
            Debug.Log($"Player is Dead.");
            FindObjectOfType<GameManager>().ResetPlayer();
        }

        Engage();
    }
}
