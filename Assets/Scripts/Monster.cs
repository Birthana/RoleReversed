using UnityEngine;

public class Monster : Character
{
    public override void Awake()
    {
        base.Awake();
        Entrance();
    }

    private void Entrance()
    {

    }

    private void Engage()
    {

    }

    public void Exit()
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
