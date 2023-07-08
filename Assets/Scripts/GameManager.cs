using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Room startRoom;
    private Player player;
    private Room currentRoom;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        SetRoom(startRoom);
        StartCoroutine(WalkThruDungeon());
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
            if (currentRoom.IsEndRoom())
            {
                still_running = false;
            }

            var character = player.GetComponent<Character>();
            yield return StartCoroutine(currentRoom.MakeAttack(character));
            yield return new WaitForSeconds(0.5f);
            SetRoom(currentRoom.GetNextRoom());
        }
    }
}
