using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Monster> monsters = new List<Monster>();

    private void Start()
    {
        MakeAttack();
    }

    public void MakeAttack()
    {
        var character = FindObjectOfType<Player>().GetComponent<Character>();
        foreach (var monster in monsters)
        {
            monster.Attack(character);
        }
    }
}
