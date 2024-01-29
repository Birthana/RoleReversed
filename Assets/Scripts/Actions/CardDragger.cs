using System.Collections;
using UnityEngine;

public interface ICardDragger
{
    public void UpdateLoop();
    public void ResetHover();
}

public class CardDragger : MonoBehaviour, ICardDragger
{
    private Card selectedCard;
    private Card hoverCard;
    private HoverAnimation hoverAnimation;

    private Hand hand;
    private GainActionManager gainActions;
    private RerollManager reroll;
    private SelectionScreen selectionScreen;

    private IMouseWrapper mouseWrapper;

    public void SetMouseWrapper(IMouseWrapper wrapper)
    {
        mouseWrapper = wrapper;
    }

    public void Awake()
    {
        SetMouseWrapper(new MouseWrapper());
        hand = FindObjectOfType<Hand>();
        gainActions = GetComponent<GainActionManager>();
        reroll = GetComponent<RerollManager>();
        hoverAnimation = GetComponent<HoverAnimation>();
        selectionScreen = FindObjectOfType<SelectionScreen>();
    }

    private void Update()
    {
        UpdateLoop();
    }

    public void UpdateLoop()
    {
        CheckToSelectCard();
        HoverCard();
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
        if (mouseWrapper.IsOnHand() && CardIsNotSelected())
        {
            if (!CardIsNotTheSame(hoverCard))
            {
                return;
            }

            if (hoverCard != null)
            {
                hoverAnimation.PerformReturn();
            }

            hoverCard = mouseWrapper.GetHitComponent<Card>();
            hoverAnimation.Hover(hoverCard, new Vector2(0, 0.5f), 0.1f);
        }
    }

    private bool CardIsNotTheSame(Card card)
    {
        if (card == null)
        {
            return true;
        }

        if (mouseWrapper.IsOnHand())
        {
            var mouseCard = mouseWrapper.GetHitComponent<Card>();
            if (!card.gameObject.Equals(mouseCard.gameObject))
            {
                return true;
            }
        }

        return false;
    }

    public Card GetSelectedCard() { return selectedCard; }

    private bool CardIsNotSelected() { return selectedCard == null; }

    private void CheckToGainAction()
    {
        if (PlayerIsOnActionButton())
        {
            RemoveSelectedCard();
            gainActions.UseSelectCardToPayForGainAction();
        }
    }

    private bool PlayerIsOnActionButton() { return mouseWrapper.PlayerReleasesLeftClick() && Mouse.IsOnActionButton(); }

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

    private bool PlayerIsOnRerollButton() { return mouseWrapper.PlayerReleasesLeftClick() && Mouse.IsOnRerollButton(); }

    private void CheckToSelectCard()
    {
        if (PlayerClicksOnHand())
        {
            PickUpCard();
        }
    }

    public void PickUpCard()
    {
        selectedCard = mouseWrapper.GetHitComponent<Card>();
    }

    private bool PlayerClicksOnHand() { return mouseWrapper.PlayerPressesLeftClick() && mouseWrapper.IsOnHand(); }
    
    private void DragSelectCardToMouse() { MoveSelectedCardToMouse(); }

    public void ResetHover()
    {
        hoverAnimation.ResetHoverAnimation();
        hoverCard = null;
    }

    private void MoveSelectedCardToMouse()
    {
        ResetHover();
        Vector2 mousePosition = mouseWrapper.GetPosition();
        selectedCard.transform.position = mousePosition;
    }

    private void CheckToCastSelectCard()
    {
        if (PlayerCanCastSelectedCard())
        {
            CastSelectedCard();
        }
    }

    private bool PlayerCanCastSelectedCard() { return mouseWrapper.PlayerReleasesLeftClick() && CanCastSelectedCard(); }

    private bool CanCastSelectedCard() { return gainActions.HaveEnoughActions(selectedCard) && selectedCard.HasTarget(); }

    private void CastSelectedCard()
    {
        selectedCard.Cast();
        selectedCard = null;
    }

    public void CheckToAddCardToSelection()
    {
        if (mouseWrapper.PlayerReleasesLeftClick() && mouseWrapper.IsOnSelection())
        {
            if (selectionScreen.IsFull())
            {
                ReturnSelectedCard();
            }

            ResetHover();
            hand.Remove(selectedCard);
            selectionScreen.AddToSelection(selectedCard);
            selectedCard = null;
        }
    }

    private void CheckToReturnSelectCard()
    {
        if (mouseWrapper.PlayerReleasesLeftClick())
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
