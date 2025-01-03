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
    private HoverAnimation hoverAnimation;

    private Hand hand;
    private Drop drop;

    private IMouseWrapper mouseWrapper;
    private DraftManager draftManager;
    private GameManager gameManager;
    private SoulShop soulShop;
    private EndSound handSound;


    private HoverCard hoverCard_;
    private Coroutine coroutine;

    public void SetMouseWrapper(IMouseWrapper wrapper)
    {
        mouseWrapper = wrapper;
    }

    public void Awake()
    {
        SetMouseWrapper(new MouseWrapper());
        hand = FindObjectOfType<Hand>();
        hoverAnimation = GetComponent<HoverAnimation>();
        drop = FindObjectOfType<Drop>();
        var toolTip = FindObjectOfType<ToolTipManager>();
        hoverCard_ = new HoverCard(toolTip, hoverAnimation);
    }

    private void Update()
    {
        UpdateLoop();
    }

    public DraftManager GetDraftManager()
    {
        if (draftManager == null)
        {
            draftManager = FindObjectOfType<DraftManager>();
        }

        return draftManager;
    }

    public GameManager GetGameManager()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        return gameManager;
    }

    public SoulShop GetSoulShop()
    {
        if (soulShop == null)
        {
            soulShop = FindObjectOfType<SoulShop>(true);
        }

        return soulShop;
    }

    public EndSound GetHandSound()
    {
        if (handSound == null)
        {
            handSound = FindObjectOfType<Hand>().GetComponent<EndSound>();
        }

        return handSound;
    }

    public void UpdateLoop()
    {
        if (GetGameManager().IsRunning() || GetSoulShop().IsOpen())
        {
            return;
        }

        CheckToSelectCard();
        TryToHoverCard();
        if (CardIsNotSelected())
        {
            return;
        }

        DragSelectCardToMouse();
        CheckToCastSelectCard();
        if (CardIsNotSelected())
        {
            return;
        }

        CheckToReturnSelectCard();
    }

    private bool PlayerIsHoveringHand() { return mouseWrapper.IsOnHand() && CardIsNotSelected(); }

    private bool PlayerIsNotHoveringHand() { return !PlayerIsHoveringHand(); }

    private bool CardIsNotSelected() { return selectedCard == null; }

    private void TryToHoverCard()
    {
        if (PlayerIsNotHoveringHand())
        {
            if (coroutine == null)
            {
                coroutine = StartCoroutine(TryToStop());
            }

            return;
        }

        if (CardIsTheSame(hoverCard_.Get()))
        {
            if (coroutine == null)
            {
                coroutine = StartCoroutine(TryToStop());
            }

            return;
        }

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        GetHandSound().Play();
        var card = mouseWrapper.GetHitComponent<Card>();
        hoverCard_.Hover(card);
    }

    private IEnumerator TryToStop()
    {
        yield return new WaitForSeconds(0.25f);
        GetHandSound().Stop();
    }

    private bool CardIsTheSame(Card card) { return !CardIsNotTheSame(card); }

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


    private void CheckToSelectCard()
    {
        if (PlayerClicksOnHand())
        {
            PickUpCard();
        }
    }

    public void PickUpCard()
    {
        FindObjectOfType<ToolTipManager>().Toggle();
        selectedCard = mouseWrapper.GetHitComponent<Card>();
    }

    private bool PlayerClicksOnHand() { return mouseWrapper.PlayerPressesLeftClick() && mouseWrapper.IsOnHand(); }
    
    private void DragSelectCardToMouse() { MoveSelectedCardToMouse(); }

    public void ResetHover()
    {
        hoverAnimation.ResetHoverAnimation();
        hoverCard_.Reset();
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
            FindObjectOfType<ToolTipManager>().Toggle();
            CastSelectedCard();
        }
    }

    private bool PlayerCanCastSelectedCard() { return mouseWrapper.PlayerReleasesLeftClick() && CanCastSelectedCard(); }

    private bool CanCastSelectedCard()
    {
        return FindObjectOfType<ActionManager>().CanCast(selectedCard) && selectedCard.HasTarget();
    }

    private void CastSelectedCard()
    {
        selectedCard.Cast();
        drop.Add(selectedCard.GetCardInfo());
        selectedCard = null;
    }

    private void CheckToReturnSelectCard()
    {
        if (mouseWrapper.PlayerReleasesLeftClick())
        {
            FindObjectOfType<ToolTipManager>().Toggle();
            ReturnSelectedCard();
        }
    }

    private void ReturnSelectedCard()
    {
        hand.DisplayHand();
        selectedCard = null;
    }
}
