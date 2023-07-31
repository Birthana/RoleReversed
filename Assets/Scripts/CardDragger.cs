using System.Collections;
using UnityEngine;

public class CardDragger : MonoBehaviour
{
    private Card selectedCard;
    private Card hoverCard;
    private Coroutine coroutine;

    private Hand hand;
    private GainActionManager gainActions;
    private RerollManager reroll;

    private void Start()
    {
        hand = FindObjectOfType<Hand>();
        gainActions = GetComponent<GainActionManager>();
        reroll = GetComponent<RerollManager>();
    }

    private void Update()
    {
        CheckToSelectCard();
        HoverCard();
        if (CardIsNotSelected())
        {
            return;
        }

        CheckToAddCardToSelection();
        if (CardIsNotSelected())
        {
            return;
        }

        DragSelectCardToMouse();
        CheckToCastSelectCard();
        CheckToGainAction();
        CheckToRerollCard();
        if (CardIsNotSelected())
        {
            return;
        }

        CheckToReturnSelectCard();
    }

    private void HoverCard()
    {
        if (Mouse.IsOnHand())
        {
            if (hoverCard == null)
            {
                hoverCard = Mouse.GetHitComponent<Card>();
            }

            if (CardIsNotTheSame(hoverCard))
            {
                hoverCard = Mouse.GetHitComponent<Card>();
                StopCoroutine(coroutine);
                coroutine = null;
                hand.DisplayHand();
            }

            if (coroutine == null)
            {
                coroutine = StartCoroutine(Hover(hoverCard));
            } 
        }
    }

    private IEnumerator Hover(Card card)
    {
        var shakeAnimation = new ShakeAnimation(card.transform, 0.5f, 0.1f);
        yield return StartCoroutine(shakeAnimation.AnimateFromStartToEnd());
        bool stillRunning = true;
        while (stillRunning)
        {
            if (!Mouse.IsOnHand() || CardIsNotTheSame(card))
            {
                yield return StartCoroutine(shakeAnimation.AnimateFromEndToStart());
                stillRunning = false;
            }

            yield return null;
        }

        StopCoroutine(coroutine);
        coroutine = null;
        hoverCard = null;
    }

    private bool CardIsNotTheSame(Card card)
    {
        if (Mouse.IsOnHand())
        {
            var mouseCard = Mouse.GetHitComponent<Card>();
            if (!card.Equals(mouseCard))
            {
                return true;
            }
        }

        return false;
    }

    public virtual Card GetSelectedCard() { return selectedCard; }

    private bool CardIsNotSelected() { return selectedCard == null; }

    private void CheckToGainAction()
    {
        if (PlayerIsOnActionButton())
        {
            RemoveSelectedCard();
            gainActions.UseSelectCardToPayForGainAction();
        }
    }

    private bool PlayerIsOnActionButton() { return Mouse.PlayerReleasesLeftClick() && Mouse.IsOnActionButton(); }

    private void RemoveSelectedCard()
    {
        hand.Remove(selectedCard);
        Destroy(selectedCard.gameObject);
    }

    private void CheckToRerollCard()
    {
        if (PlayerIsOnRerollButton())
        {
            RemoveSelectedCard();
            reroll.UseSelectedCardToPayForReroll();
        }
    }

    private bool PlayerIsOnRerollButton() { return Mouse.PlayerReleasesLeftClick() && Mouse.IsOnRerollButton(); }

    private void CheckToSelectCard()
    {
        if (PlayerClicksOnHand())
        {
            PickUpCard();
        }
    }

    public virtual void PickUpCard()
    {
        selectedCard = Mouse.GetHitComponent<Card>();
    }

    private bool PlayerClicksOnHand() { return Mouse.PlayerPressesLeftClick() && Mouse.IsOnHand(); }

    private void DragSelectCardToMouse() { MoveSelectedCardToMouse(); }

    private void MoveSelectedCardToMouse()
    {
        Vector2 mousePosition = Mouse.GetPosition();
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        selectedCard.transform.position = mousePosition;
    }

    private void CheckToCastSelectCard()
    {
        if (PlayerCanCastSelectedCard())
        {
            CastSelectedCard();
        }
    }

    private bool PlayerCanCastSelectedCard() { return Mouse.PlayerReleasesLeftClick() && CanCastSelectedCard(); }

    private bool CanCastSelectedCard() { return gainActions.HaveEnoughActions(selectedCard) && selectedCard.HasTarget(); }

    private void CastSelectedCard()
    {
        selectedCard.Cast();
        selectedCard = null;
    }

    private void CheckToAddCardToSelection()
    {
        var selectionScreen = FindObjectOfType<SelectionScreen>();
        if (Mouse.PlayerReleasesLeftClick() && Mouse.IsOnSelection())
        {
            if (selectionScreen.IsFull())
            {
                ReturnSelectedCard();
            }

            hand.Remove(selectedCard);
            selectionScreen.AddToSelection(selectedCard);
            selectedCard = null;
        }
    }

    private void CheckToReturnSelectCard()
    {
        if (Mouse.PlayerReleasesLeftClick())
        {
            ReturnSelectedCard();
        }
    }

    private void ReturnSelectedCard()
    {
        hand.DisplayHand();
        selectedCard = null;
    }
}
