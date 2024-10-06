using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "HeavyAttackPlayer", menuName = "BattleCard/HeavyAttackPlayer")]
public class HeavyAttackPlayer : BattleCardInfo
{
    private int counter = 0;

    public override void SetCharacter(Character newCharacter)
    {
        character = newCharacter;
    }

    public override IEnumerator Play()
    {
        if (character.IsDead())
        {
            yield break;
        }

        counter++;
        if (counter == 2)
        {
            var player = FindObjectOfType<Player>();
            yield return character.MakeAttack(player);
            counter = 0;
        }
    }
}
