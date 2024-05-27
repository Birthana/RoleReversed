using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Rest", menuName = "PlayerSkill/Rest")]
public class Rest : SkillInfo
{
    public override IEnumerator Cast(Room room)
    {
        var player = FindObjectOfType<Player>();
        player.RestoreHealth(5);
        yield return new WaitForSeconds(0.1f);
    }
}
