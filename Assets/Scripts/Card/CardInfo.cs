using UnityEngine;

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
public class CardInfo : ScriptableObject
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
{
    public string cardName = "DEFAULT";
    public Sprite fieldSprite;
    public int cost;
    [TextArea(1, 2)]
    public string effectDescription;

    public override bool Equals(object other)
    {
        if (other == null)
        {
            return false;
        }

        if (!(other is CardInfo))
        {
            return false;
        }

        return cardName.Equals(((CardInfo)other).cardName);
    }

    public bool IsMonster()
    {
        return this is MonsterCardInfo;
    }

    public bool IsRoom()
    {
        return this is RoomCardInfo;
    }

    public virtual Card GetCardPrefab() { return new Card(); }

    public virtual CardUI GetCardUI() { return new CardUI(); }
}
