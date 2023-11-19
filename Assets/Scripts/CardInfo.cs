using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewCard", menuName ="CardInfo")]
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
public class CardInfo : ScriptableObject
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
{
    public string cardName = "DEFAULT";
    public Sprite sprite;
    public int cost;
    public GameObject prefab;

    public override bool Equals(object other)
    {
        if (other == null)
        {
            return false;
        }

        if (!(other is Card))
        {
            return false;
        }

        return cardName.Equals(((Card)other).GetName());
    }

    public bool IsMonster()
    {
        return prefab.GetComponent<Monster>();
    }

    public bool IsRoom()
    {
        return prefab.GetComponent<Room>();
    }
}
