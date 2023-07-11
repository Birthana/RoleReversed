using UnityEngine;

public class CardDragger : MonoBehaviour
{
    private Card selectedCard;
    private Vector3 previousPosition;
    private int actionCount = 0;
    private int rerollCount = 0;

    private Hand hand;
    private ActionManager actionManager;
    private CardManager cardManager;

    private void Start()
    {
        hand = FindObjectOfType<Hand>();
        actionManager = FindObjectOfType<ActionManager>();
        cardManager = FindObjectOfType<CardManager>();
    }

    private void Update()
    {
        CheckToSelectCard();
        DragSelectCardToMouse();
        CheckToCastSelectCard();
        CheckToGainAction();
        CheckToRerollCard();
        CheckToReturnSelectCard();
    }

    private bool CardIsNotSelected() { return selectedCard == null; }

    private void CheckToGainAction()
    {
        if (CardIsNotSelected())
        {
            return;
        }

        if (PlayerIsOnActionButton())
        {
            UseSelectCardToPayForGainAction();
            if (PlayerPaidForGainAction())
            {
                GainAction();
            }
        }
    }

    private bool PlayerIsOnActionButton() { return Mouse.PlayerReleasesLeftClick() && Mouse.IsOnActionButton(); }

    private void RemoveSelectedCard()
    {
        hand.Remove(selectedCard);
        Destroy(selectedCard.gameObject);
    }

    private void UseSelectCardToPayForGainAction()
    {
        RemoveSelectedCard();
        actionCount++;
    }

    private bool PlayerPaidForGainAction() { return actionCount == 3; }

    private void GainAction()
    {
        actionCount = 0;
        actionManager.AddActions(1);
    }

    private void CheckToRerollCard()
    {
        if (CardIsNotSelected())
        {
            return;
        }

        if (PlayerIsOnRerollButton())
        {
            UseSelectedCardToPayForReroll();
            if (PlayerPaidForReroll())
            {
                Reroll();
            }
        }
    }

    private bool PlayerIsOnRerollButton() { return Mouse.PlayerReleasesLeftClick() && Mouse.IsOnRerollButton(); }

    private void UseSelectedCardToPayForReroll()
    {
        RemoveSelectedCard();
        rerollCount++;
    }

    private bool PlayerPaidForReroll() { return rerollCount == 2; }

    private void Reroll()
    {
        rerollCount = 0;
        hand.Add(cardManager.GetRandomCard());
    }

    private void CheckToSelectCard()
    {
        if (PlayerClicksOnHand())
        {
            PickUpCard();
        }
    }

    private void PickUpCard()
    {
        selectedCard = Mouse.GetHitComponent<Card>();
        previousPosition = selectedCard.transform.localPosition;
    }

    private bool PlayerClicksOnHand() { return Mouse.PlayerPressesLeftClick() && Mouse.IsOnHandButton(); }

    private void DragSelectCardToMouse()
    {
        if (CardIsNotSelected())
        {
            return;
        }

        MoveSelectedCardToMouse();
    }

    private void MoveSelectedCardToMouse()
    {
        Vector2 mousePosition = Mouse.GetPosition();
        selectedCard.transform.position = mousePosition;
    }

    private void CheckToCastSelectCard()
    {
        if (CardIsNotSelected())
        {
            return;
        }

        if (PlayerCanCastSelectedCard())
        {
            CastSelectedCard();
        }
    }

    private bool PlayerCanCastSelectedCard() { return Mouse.PlayerReleasesLeftClick() && CanCastSelectedCard(); }

    private bool CanCastSelectedCard() { return HaveEnoughActions(selectedCard) && selectedCard.HasTarget(); }

    private void CastSelectedCard()
    {
        selectedCard.Cast();
        selectedCard = null;
    }

    private bool HaveEnoughActions(Card card) { return actionManager.CanCast(card); }

    private void CheckToReturnSelectCard()
    {
        if (CardIsNotSelected())
        {
            return;
        }

        if (Mouse.PlayerReleasesLeftClick())
        {
            ReturnSelectedCard();
        }
    }

    private void ReturnSelectedCard()
    {
        selectedCard.transform.localPosition = previousPosition;
        selectedCard = null;
    }
}
