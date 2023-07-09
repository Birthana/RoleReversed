using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pack : MonoBehaviour
{
    public int numberOfCards = 5;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, Vector2.zero, 100, 1 << LayerMask.NameToLayer("Pack"));
            if (hit)
            {
                OpenPack();
                Destroy(gameObject);
            }
        }
    }

    public void OpenPack()
    {
        var hand = FindObjectOfType<Hand>();
        var rngCards = FindObjectOfType<CardManager>();
        hand.Add(rngCards.GetMonsterCard());
        hand.Add(rngCards.GetRoomCard());
        for (int i = 0; i < numberOfCards - 2; i++)
        {
            hand.Add(rngCards.GetRandomCard());
        }
    }
}
