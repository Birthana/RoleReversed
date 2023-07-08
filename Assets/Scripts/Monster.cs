using UnityEngine;

[RequireComponent(typeof(Character))]
public class Monster : MonoBehaviour
{
    private Character stats;

    private void Awake()
    {
        stats = GetComponent<Character>();
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
        character.TakeDamage(stats.GetDamage());
        Engage();
    }
}
