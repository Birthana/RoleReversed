using System.Collections;

public class AttackMonster : BattleCardInfo
{
    private Room room;

    public override void SetCharacter(Character newCharacter)
    {
        character = newCharacter;
    }

    public void SetRoom(Room newRoom)
    {
        room = newRoom;
    }

    public override IEnumerator Play()
    {
        yield return character.MakeAttack(room.GetRandomMonster());
    }
}
