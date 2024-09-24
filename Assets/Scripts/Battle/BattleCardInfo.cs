using System.Collections;

public class BattleCardInfo : CardInfo
{
    public virtual void SetCharacter(Character newCharacter) { }

    public virtual IEnumerator Play() { yield return null; }
}
