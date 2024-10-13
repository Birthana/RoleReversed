using UnityEngine;

[CreateAssetMenu(fileName = "UnlockCardsBonus", menuName = "SpaceInfo/UnlockCardsBonus")]
public class UnlockCardsBonus : SpaceInfo
{
    public BoosterInfo boosterInfo;

    public override void BuildEffect()
    {
        FindObjectOfType<SoulShop>().OpenShop();
        var boosterManager = FindObjectOfType<BoosterManager>();
        boosterManager.SpawnBooster(boosterInfo);
        boosterInfo.Unlock();
    }
}
