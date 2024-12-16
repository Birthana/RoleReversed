using System.Collections;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    public void MoveMonster(Monster monster, Room newRoom)
    {
        StartCoroutine(Move(monster, monster.GetCurrentRoom(), newRoom));
    }

    private IEnumerator Move(Monster monster, Room startRoom, Room endRoom)
    {
        var distanceToTravel = endRoom.transform.position - startRoom.transform.position;
        var shakeAnimation = new ShakeAnimation(monster.transform, distanceToTravel, 0.1f);
        yield return shakeAnimation.AnimateFromStartToEnd();
    }
}
