using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDragger : MonoBehaviour
{
    private Card selectedCard;
    private Vector3 previousPosition;
    private int actionCount = 0;

    private void Update()
    {
        CheckToSelectCard();
        DragSelectCardToMouse();
        CheckToCastSelectCard();
        CheckToGainAction();
        CheckToRerollCard();
        CheckToReturnSelectCard();
    }

    private void CheckToGainAction()
    {
        if (selectedCard == null)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, Vector2.zero, 100, 1 << LayerMask.NameToLayer("Action"));
            if (hit)
            {
                FindObjectOfType<Hand>().Remove(selectedCard);
                Destroy(selectedCard.gameObject);
                actionCount++;
                if (actionCount == 3)
                {
                    actionCount = 0;
                    FindObjectOfType<ActionManager>().AddActions(1);
                }
            }
        }
    }

    private void CheckToRerollCard()
    {
        if (selectedCard == null)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, Vector2.zero, 100, 1 << LayerMask.NameToLayer("Reroll"));
            if (hit)
            {
                FindObjectOfType<Hand>().Remove(selectedCard);
                Destroy(selectedCard.gameObject);
                actionCount++;
                if (actionCount == 2)
                {
                    actionCount = 0;
                    var card =FindObjectOfType<CardManager>().GetRandomCard();
                    FindObjectOfType<Hand>().Add(card);
                }
            }
        }
    }

    private void CheckToSelectCard()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, Vector2.zero, 100, 1 << LayerMask.NameToLayer("Hand"));
            if (hit)
            {
                selectedCard = hit.transform.GetComponent<Card>();
                previousPosition = selectedCard.transform.localPosition;
            }
        }
    }

    private void DragSelectCardToMouse()
    {
        if (selectedCard == null)
        {
            return;
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectedCard.transform.position = mousePosition;
    }

    private void CheckToCastSelectCard()
    {
        if (selectedCard == null)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (CanCast(selectedCard) && selectedCard.HasTarget())
            {
                selectedCard.Cast();
                selectedCard = null;
            }
        }
    }

    private bool CanCast(Card card)
    {
        return FindObjectOfType<ActionManager>().CanCast(card);
    }

    private void CheckToReturnSelectCard()
    {
        if (selectedCard == null)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedCard.transform.localPosition = previousPosition;
            selectedCard = null;
        }
    }

}
