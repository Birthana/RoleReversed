using UnityEngine;

public class BattleCardUI : CardUI
{
    [SerializeField] private SpriteRenderer characterUI;

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        var battleCard = (BattleCardInfo)newCardInfo;
        SetCharacter(battleCard.GetCharacter());
        SetDescription(battleCard.effectDescription);
    }

    private void SetCharacter(Character character)
    {
        if (character is Player)
        {
            characterUI.sprite = ((Player)character).GetComponent<SpriteRenderer>().sprite;
            return;
        }

        var monster = (Monster)character;
        characterUI.sprite = monster.cardInfo.fieldSprite;
    }
}
