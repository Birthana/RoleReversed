using System.Collections;
using UnityEngine;

public class LootAnimation : MonoBehaviour
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

    public void AnimateLoot()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        yield return ShowCard();
        yield return MoveToDeck();
        yield return HideCard();
        DestroyImmediate(gameObject);
    }

    private IEnumerator ShowCard()
    {
        yield return new WaitForSeconds(delay);
        yield return new WaitForSeconds(SHOW_TIME);
    }

    private IEnumerator HideCard()
    {
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator MoveToDeck()
    {
        var distanceToTravel = GetDeck().transform.position - transform.position;
        var shakeAnimation = new ShakeAnimation(transform, distanceToTravel, MOVE_TIME);
        yield return shakeAnimation.AnimateFromStartToEnd();
    }
}
