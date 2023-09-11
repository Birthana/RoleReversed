using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDragger : MonoBehaviour
{
    private Monster selected;
    private IMouseWrapper mouse;

    private void Awake()
    {
        SetMouseWrapper(new MouseWrapper());
    }

    private void Update()
    {
        if (!(mouse is MouseWrapper))
        {
            return;
        }

        UpdateLoop();
    }

    public void UpdateLoop()
    {
        if (mouse.PlayerPressesLeftClick())
        {
            PickUp();
        }
        
        if (selected == null)
        {
            return;
        }

        MoveSelected();
    }

    public Monster GetSelected()
    {
        return selected;
    }

    public void SetMouseWrapper(IMouseWrapper mouse)
    {
        this.mouse = mouse;
    }

    public void PickUp()
    {
        if (selected == null && mouse.IsOnMonster())
        {
            selected = mouse.GetHitComponent<Monster>();
        }
    }

    public void MoveSelected()
    {
        var mousePosition = mouse.GetPosition();
        selected.transform.position = mousePosition;
    }
}
