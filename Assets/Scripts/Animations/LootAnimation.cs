using System.Collections;
using UnityEngine;

public class LootAnimation : MonoBehaviour
{
    public static readonly float SHOW_TIME = 0.30f;
    public static readonly float MOVE_TIME = 0.20f;
    public static readonly float ANIMATION_TIME = SHOW_TIME + MOVE_TIME;
    private float delay = 0.0f;

    public Sprite GetSprite() { return GetComponent<SpriteRenderer>().sprite; }

    public void SetDelay(float delayTime) { delay = delayTime; }

    public void AnimateLoot(Sprite spriteToAnimate)
    {
        StartCoroutine(Animate(spriteToAnimate));
    }

    private IEnumerator Animate(Sprite spriteToAnimate)
    {
        GetComponent<SpriteRenderer>().sprite = spriteToAnimate;
        yield return new WaitForSeconds(delay);
        yield return new WaitForSeconds(SHOW_TIME);
        var deck = FindObjectOfType<Deck>();
        var shakeAnimation = new ShakeAnimation(transform, deck.transform.position - transform.position, MOVE_TIME);
        yield return shakeAnimation.AnimateFromStartToEnd();
        GetComponent<SpriteRenderer>().sprite = null;
        yield return new WaitForSeconds(0.1f);
        DestroyImmediate(gameObject);
    }
}
