using UnityEngine;

public class CardDragger : MonoBehaviour
{
    private Card selectedCard;
    private Vector3 previousPosition;
    private int rerollCount = 0;

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

    private bool CardIsNotSelected() { return selectedCard == null; }

    private void CheckToGainAction()
    {
        if (PlayerIsOnActionButton())
        {
            RemoveSelectedCard();
            gainActions.UseSelectCardToPayForGainAction();
            if (gainActions.PlayerPaidForGainAction())
            {
                gainActions.GainAction();
            }
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
            if (reroll.PlayerPaidForReroll())
            {
                reroll.Reroll();
            }
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

    private void PickUpCard()
    {
        selectedCard = Mouse.GetHitComponent<Card>();
        previousPosition = selectedCard.transform.localPosition;
    }

    private bool PlayerClicksOnHand() { return Mouse.PlayerPressesLeftClick() && Mouse.IsOnHandButton(); }

    private void DragSelectCardToMouse() { MoveSelectedCardToMouse(); }

    private void MoveSelectedCardToMouse()
    {
        Vector2 mousePosition = Mouse.GetPosition();
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

    private void CheckToReturnSelectCard()
    {
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
