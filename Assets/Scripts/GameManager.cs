using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Room startRoom;
    public Vector2 PLAYER_OFFSET;
    private Player player;
    private Room currentRoom;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        SetRoom(startRoom);
        StartCoroutine(WalkThruDungeon());
    }

    public void ResetPlayer()
    {
        StopAllCoroutines();
        SetRoom(startRoom);
        PlayerMoveTo(currentRoom);
        player.ResetStats();
    }

    private void SetRoom(Room room)
    {
        currentRoom = room;
    }

    public IEnumerator WalkThruDungeon()
    {
        bool still_running = true;
        while (still_running)
        {
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(player.MakeAttack(currentRoom.GetRandomMonster()));
            yield return StartCoroutine(currentRoom.MakeAttack(player));

            if (currentRoom.IsEmpty() && currentRoom.IsEndRoom())
            {
                SetRoom(startRoom);
                PlayerMoveTo(currentRoom);
                break;
            }

            if (currentRoom.IsEmpty())
            {
                GoToNextRoom();
                PlayerMoveTo(currentRoom);
            }
        }
    }

    private void GoToNextRoom()
    {
        SetRoom(currentRoom.GetNextRoom());
    }

    private void PlayerMoveTo(Room room)
    {
        player.transform.SetParent(room.transform);
        player.transform.localPosition = PLAYER_OFFSET;
    }
}
