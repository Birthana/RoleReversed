using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDragger : MonoBehaviour
{
    private Monster monster;
    private Room returnRoom;
    private IMouseWrapper mouse;
    private IGameManager gameManager;
    private DraftManager draftManager;
    [SerializeField] private SoulShop soulShop;

    public void SetGameManager(IGameManager newGameManager)
    {
        gameManager = newGameManager;
    }

    public DraftManager GetDraftManager()
    {
        if (draftManager == null)
        {
            draftManager = FindObjectOfType<DraftManager>();
        }

        return draftManager;
    }

    private SoulShop GetSoulShop()
    {
        if (soulShop == null)
        {
            soulShop = FindObjectOfType<SoulShop>();
        }

        return soulShop;
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

    private bool DraftManagerIsRunning()
    {
        if (GetDraftManager() == null)
        {
            return false;
        }

        return GetDraftManager().IsRunning();
    }

    public void UpdateLoop()
    {
        if (gameManager.IsRunning() || DraftManagerIsRunning() || GetSoulShop().IsOpen())
        {
            return;
        }

        TryToPickUpMonster();

        if (monster == null)
        {
            return;
        }

        MoveSelected();
        TryToDropMonster();
    }

    private void TryToPickUpMonster()
    {
        if (mouse.IsOnMonster())
        {
            var monster = mouse.GetHitComponent<Monster>();
            var position = monster.gameObject.transform.position + (Vector3.up * 3);
            FindObjectOfType<ToolTipManager>().SetText(monster.cardInfo.effectDescription, position);
        }

        if (mouse.IsOnRoom())
        {
            var room = mouse.GetHitComponent<Room>();
            var position = room.gameObject.transform.position + (Vector3.up * 4.5f);
            FindObjectOfType<ToolTipManager>().SetText(room.GetCardInfo().effectDescription, 
                                                       position,
                                                       room.GetRoommateEffects(),
                                                       gameManager.GetRoommateRoom(room));
        }

        if (mouse.PlayerPressesLeftClick())
        {
            PickUp();
        }
    }

    private void TryToDropMonster()
    {
        if (!mouse.PlayerReleasesLeftClick())
        {
            return;
        }

        if (!mouse.IsOnRoom())
        {
            ReturnToRoom();
            return;
        }

        var newRoom = mouse.GetHitComponent<Room>();
        if (newRoom.GetCapacity() == 0)
        {
            ReturnToRoom();
            return;
        }

        DropMonsterTo(newRoom);
    }

    private void DropMonsterTo(Room room)
    {
        LeaveCurrentRoom();
        MoveTo(room);
        monster = null;
    }

    private void LeaveCurrentRoom()
    {
        returnRoom.Leave(monster);
        returnRoom.IncreaseCapacity(1);
        returnRoom = null;
    }

    private void MoveTo(Room room)
    {
        monster.transform.SetParent(room.transform);
        room.Add(monster);
        room.ReduceCapacity(1);
    }

    public Monster GetSelected() { return monster; }

    public void SetMouseWrapper(IMouseWrapper mouse)
    {
        this.mouse = mouse;
    }

    public void PickUp()
    {
        if (monster == null && mouse.IsOnMonster())
        {
            monster = mouse.GetHitComponent<Monster>();
            returnRoom = monster.GetComponentInParent<Room>();
        }
    }

    public void MoveSelected()
    {
        FindObjectOfType<ToolTipManager>().Clear();
        var mousePosition = mouse.GetPosition();
        monster.transform.position = mousePosition;
    }

    private void ReturnToRoom()
    {
        monster = null;
        returnRoom.DisplayMonsters();
        returnRoom = null;
    }
}
