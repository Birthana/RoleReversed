using System.Collections;
using UnityEngine;

public class BattleDeckUI : MonoBehaviour
{
    public SpriteRenderer timerBar;
    public SpriteRenderer timerBarFrame;

    private void Awake()
    {
        FindObjectOfType<BattleDeck>().AddToOnCardPlayed(Play);
        FindObjectOfType<BattleDeck>().AddToOnCardAdded(Add);
        FindObjectOfType<BattleDeck>().AddToOnShuffle(Shuffle);
        FindObjectOfType<BattleDeck>().AddToOnBattleDeckWait(Timer);
    }

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

    public void Timer(float time)
    {
        StartCoroutine(StartTimer(time));
    }

    private IEnumerator StartTimer(float time)
    {
        var timer = time;
        ShowTimer(time);

        while (timer > 0.0f)
        {
            ScaleTimer(timer, time);
            yield return new WaitForSeconds(0.01f);
            timer -= 0.01f;
        }

        HideTimer(time);
        yield return null;
    }

    private void ShowTimer(float time)
    {
        ScaleTimer(time, time);
        timerBarFrame.gameObject.SetActive(true);
    }
    private void HideTimer(float time)
    {
        ScaleTimer(0.0f, time);
        timerBarFrame.gameObject.SetActive(false);
    }

    private void ScaleTimer(float currentTime, float maxTime)
    {
        timerBar.transform.localScale = new Vector3(currentTime / maxTime, 1.0f, 0.0f);
    }
}
