using UnityEngine;

public class RoomCard : Card
{
    public Room roomPrefab;
    private Transform selectedSpace;

    public override bool HasTarget()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.Raycast(ray.origin, Vector2.zero, 100, 1 << LayerMask.NameToLayer("Space"));
        if (hit)
        {
            selectedSpace = hit.transform;
            return true;
        }

        return false;
    }

    public override void Cast()
    {
        SpawnRoom();
        base.Cast();
    }

    private void SpawnRoom()
    {
        var room = Instantiate(roomPrefab);
        room.transform.position = selectedSpace.position;
        Destroy(selectedSpace.gameObject);
        if (FindObjectOfType<GameManager>().DoesNotHaveStartRoom())
        {
            FindObjectOfType<GameManager>().SetStartRoom(room);
        }
    }
}
