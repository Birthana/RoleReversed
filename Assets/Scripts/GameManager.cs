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

            yield return new WaitForSeconds(0.5f);
            var character = player.GetComponent<Character>();
            yield return StartCoroutine(player.MakeAttack(currentRoom.GetRandomMonster().GetComponent<Character>()));
            yield return StartCoroutine(currentRoom.MakeAttack(character));

            if (currentRoom.IsEmpty())
            {
                SetRoom(currentRoom.GetNextRoom());
                player.transform.SetParent(currentRoom.transform);
                player.transform.localPosition = Vector3.zero;
            }
        }
    }

}
