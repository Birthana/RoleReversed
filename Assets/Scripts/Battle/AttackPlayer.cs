using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackPlayer", menuName = "BattleCard/AttackPlayer")]
public class AttackPlayer : BattleCardInfo
{
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

        var player = FindObjectOfType<Player>();
        yield return character.MakeAttack(player);
    }
}
