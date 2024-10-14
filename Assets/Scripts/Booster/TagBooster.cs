using UnityEngine;

[CreateAssetMenu(fileName = "TagBooster", menuName = "BoosterInfo/TagBooster")]
public class TagBooster : BoosterInfo
{
    public Tag tag;

    public override void CreatePack()
    {
        //var packPrefab = Resources.Load<Pack>("Prefabs/Pack");
        //var pack = Instantiate(packPrefab);
        //pack.LoadRandomCard(tag);
        //pack.LoadRandomCard(tag);
        //pack.LoadRandomCard(tag);
        FindObjectOfType<DraftManager>().Draft(3, tag);
    }

    public override void Unlock()
    {
        FindObjectOfType<CardManager>().UnlockCards(tag);
    }
}
