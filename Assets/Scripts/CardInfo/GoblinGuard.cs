using UnityEngine;

[CreateAssetMenu(fileName = "GoblinGuard", menuName = "CardInfo/GoblinGuard")]
public class GoblinGuard : MonsterCardInfo
{
    public override void Engage(Monster characterSelf, Card cardSelf)
    {
        characterSelf.SpawnEntranceIcon();
        characterSelf.Unlock();
    }
}
