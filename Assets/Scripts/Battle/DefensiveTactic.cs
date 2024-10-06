using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DefensiveTactic", menuName = "BattleCard/DefensiveTactic")]
public class DefensiveTactic : BattleCardInfo
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

        var monster = (Monster)character;
        monster.TemporaryIncreaseStats(0, 5);
    }
}
