using System.Collections;
using UnityEngine;

public class LootAnimation : DisplayCard
{
    public static readonly float SHOW_TIME = 0.30f;
    public static readonly float MOVE_TIME = 0.20f;
    public static readonly float ANIMATION_TIME = SHOW_TIME + MOVE_TIME;
    private float delay = 0.0f;
    private SpriteRenderer spriteRenderer;
    private Deck deck;

    private SpriteRenderer GetSpriteRenderer()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        return spriteRenderer;
    }

    private Deck GetDeck()
    {
        if (deck == null)
        {
            deck = FindObjectOfType<Deck>();
        }

        return deck;
    }

    public Sprite GetSprite() { return GetSpriteRenderer().sprite; }

    public void SetDelay(float delayTime) { delay = delayTime; }

    public void AnimateLoot(CardInfo cardInfo)
    {
        StartCoroutine(Animate(cardInfo));
    }

    private IEnumerator Animate(CardInfo cardInfo)
    {
        yield return ShowCard(cardInfo);
        yield return MoveToDeck();
        yield return HideCard();
        DestroyImmediate(gameObject);
    }

    private void ChangeCard(CardInfo cardInfo)
    {
        if (cardInfo == null)
        {
            return;
        }

        SetCardInfo(cardInfo);
    }

    private IEnumerator ShowCard(CardInfo cardInfo)
    {
        ChangeCard(cardInfo);
        yield return new WaitForSeconds(delay);
        yield return new WaitForSeconds(SHOW_TIME);
    }

    private IEnumerator HideCard()
    {
        ChangeCard(null);
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator MoveToDeck()
    {
        var distanceToTravel = GetDeck().transform.position - transform.position;
        var shakeAnimation = new ShakeAnimation(transform, distanceToTravel, MOVE_TIME);
        yield return shakeAnimation.AnimateFromStartToEnd();
    }
}
