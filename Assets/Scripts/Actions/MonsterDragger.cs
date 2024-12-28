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

    public void UpdateLoop()
    {
        if (gameManager.IsRunning() || GetSoulShop().IsOpen())
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
        if (monster != null)
        {
            return;
        }

        if (!mouse.IsOnMonster() && !mouse.IsOnHand() && !mouse.IsOnRoom())
        {
            FindObjectOfType<ToolTipManager>().Clear();
            return;
        }

        if (mouse.IsOnMonster())
        {
            var monster = mouse.GetHitComponent<Monster>();
            var position = monster.gameObject.transform.position + (Vector3.up * 3.5f);
            FindObjectOfType<ToolTipManager>().Set(monster.cardInfo, position);
        }

        if (!mouse.IsOnMonster() && !mouse.IsOnHand() && mouse.IsOnRoom())
        {
            var room = mouse.GetHitComponent<Room>();
            var position = room.gameObject.transform.position + (Vector3.up * 4f);
            FindObjectOfType<ToolTipManager>().Set(room.GetCardInfo(), position);
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
        FindObjectOfType<ToolTipManager>().Toggle();
        monster.MoveTo(room);
        returnRoom = null;
        monster = null;
    }

    public Monster GetSelected() { return monster; }

    public void SetMouseWrapper(IMouseWrapper mouse)
    {
        this.mouse = mouse;
    }

    public void PickUp()
    {
        if (monster == null && mouse.IsOnMonster() && MonsterIsNotAssigned())
        {
            monster = mouse.GetHitComponent<Monster>();
            returnRoom = monster.GetComponentInParent<Room>();
            FindObjectOfType<ToolTipManager>().Toggle();
        }
    }

    private bool MonsterIsNotAssigned()
    {
        var hoveredMonster = mouse.GetHitComponent<Monster>();
        return !hoveredMonster.IsAssigned();
    }

    public void MoveSelected()
    {
        var mousePosition = mouse.GetPosition();
        monster.transform.position = mousePosition;
    }

    private void ReturnToRoom()
    {
        FindObjectOfType<ToolTipManager>().Toggle();
        monster = null;
        returnRoom.DisplayMonsters();
        returnRoom = null;
    }
}
