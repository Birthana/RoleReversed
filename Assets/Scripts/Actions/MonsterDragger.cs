using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDragger : MonoBehaviour
{
    private Monster selected;
    private Room returnRoom;
    private IMouseWrapper mouse;
    private IGameManager gameManager;

    public void SetGameManager(IGameManager newGameManager)
    {
        gameManager = newGameManager;
    }

    private void Awake()
    {
        SetMouseWrapper(new MouseWrapper());
        SetGameManager(FindObjectOfType<GameManager>());
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
        if (gameManager.IsRunning())
        {
            return;
        }

        if (mouse.PlayerPressesLeftClick())
        {
            PickUp();
        }

        if (selected == null)
        {
            return;
        }

        MoveSelected();

        if (mouse.PlayerReleasesLeftClick())
        {
            if (mouse.IsOnRoom())
            {
                var newRoom = mouse.GetHitComponent<Room>();
                if(newRoom.GetCapacity() == 0)
                {
                    ReturnToRoom();
                    return;
                }
                returnRoom.Leave(selected);
                returnRoom.IncreaseCapacity(1);
                selected.transform.position = newRoom.transform.position;
                selected.transform.SetParent(newRoom.transform);
                newRoom.Add(selected);
                newRoom.ReduceCapacity(1);
                selected = null;
                returnRoom = null;
            }
            else
            {
                ReturnToRoom();
            }

        }

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
            returnRoom = selected.GetComponentInParent<Room>();
        }
    }

    public void MoveSelected()
    {
        var mousePosition = mouse.GetPosition();
        selected.transform.position = mousePosition;
    }

    private void ReturnToRoom()
    {
        selected = null;
        returnRoom.DisplayMonsters();
        returnRoom = null;
    }
}
