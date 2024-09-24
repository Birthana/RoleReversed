using System.Collections;

public class AttackPlayer : BattleCardInfo
{
    private Character character;

    public override void SetCharacter(Character newCharacter)
    {
        character = newCharacter;
    }

    public override IEnumerator Play()
    {
        var player = FindObjectOfType<Player>();
        yield return character.MakeAttack(player);
    }
}
