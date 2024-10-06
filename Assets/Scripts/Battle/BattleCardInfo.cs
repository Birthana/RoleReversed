using System.Collections;
using UnityEngine;

public class BattleCardInfo : CardInfo
{
    protected Character character;
    private static readonly string BATTLE_CARD_UI_PREFAB = "Prefabs/BattleCardUI";

    public virtual void SetCharacter(Character newCharacter) { }

    public Character GetCharacter() { return character; }

    public virtual IEnumerator Play() { yield return null; }

    public override Card GetCardPrefab()
    {
        var cardPrefab = Resources.Load<MonsterCard>("Prefabs/MonsterCardPrefab");
        return cardPrefab;
    }

    public override CardUI GetCardUI()
    {
        var cardUI = Resources.Load<BattleCardUI>(BATTLE_CARD_UI_PREFAB);
        return cardUI;
    }
}
