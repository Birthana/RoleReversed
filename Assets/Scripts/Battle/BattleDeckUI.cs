using System.Collections;
using UnityEngine;

public class BattleDeckUI : MonoBehaviour
{
    public SpriteRenderer timerBar;
    public SpriteRenderer timerBarFrame;

    public void Play(BattleCardInfo cardInfo)
    {
        var cardUi = CreateBattleCardUI(cardInfo);
        Destroy(cardUi.gameObject, GameManager.ATTACK_TIMER / 1.5f);
    }

    private BattleCardUI CreateBattleCardUI(BattleCardInfo cardInfo)
    {
        var cardUi = Instantiate(cardInfo.GetCardUI(), transform) as BattleCardUI;
        cardUi.SetCardInfo(cardInfo);
        return cardUi;
    }

    public void Shuffle()
    {
        var anim = GetComponentInChildren<Animator>();
        anim.Play("BattleDeck_Shuffle");
    }

    public void Add(BattleCardInfo cardInfo)
    {
        var cardUi = CreateBattleCardUI(cardInfo);
        SetPosition(cardUi, cardInfo.GetCharacter().transform.position, new Vector3(1, 1, 0));
        StartCoroutine(MoveToBattleDeck(cardUi));
        Destroy(cardUi.gameObject, GameManager.ATTACK_TIMER);
    }

    private void SetPosition(BattleCardUI cardUI, Vector3 position, Vector3 scale)
    {
        cardUI.transform.parent = transform;
        cardUI.transform.position = position;
        cardUI.transform.localScale = scale;
    }

    private IEnumerator MoveToBattleDeck(BattleCardUI cardUi)
    {
        yield return new WaitForSeconds(GameManager.ATTACK_TIMER / 2.2f);
        var distanceToTravel = transform.position - cardUi.transform.position;
        var shakeAnimation = new ShakeAnimation(cardUi.transform, distanceToTravel, GameManager.ATTACK_TIMER / 2.0f);
        yield return shakeAnimation.AnimateFromStartToEnd();
    }
}
