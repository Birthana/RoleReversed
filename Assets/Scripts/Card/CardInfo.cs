using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Tag
{
    Slime, Goblin, Rat, Statue, Room
}

[Serializable]
public struct Description
{
    [TextArea(1, 2)]
    public string text;
    public int fontSize;
}

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
public class CardInfo : ScriptableObject
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
{
    public string cardName = "DEFAULT";
    private readonly string EFFECT_ICON_PREFAB_FILE_PATH = "Prefabs/EffectIcon";
    public Sprite fieldSprite;
    public int cost;
    public Description effect;
    public List<Tag> tags;

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

    public string GetDescription() { return effect.text; }
    public int GetFontSize() { return effect.fontSize; }

    public void SetDescription(string description) { effect.text = description; }

    public bool HasNoEffect() { return GetDescription().Equals(""); }

    public virtual Card GetCardPrefab() { return new Card(); }

    public virtual CardUI GetCardUI() { return new CardUI(); }

    public void SpawnBattleStartIcon(Vector3 position)
    {
        SpawnEffectIcon(position, "Battle Start");
    }

    private void SpawnEffectIcon(Vector3 position, string iconName)
    {
        var damageNumberPrefab = Resources.Load<GameObject>(EFFECT_ICON_PREFAB_FILE_PATH);
        var damageNumber = Instantiate(damageNumberPrefab);
        damageNumber.transform.position = position;
        damageNumber.GetComponent<TextMeshPro>().text = $"{new EffectText().GetText($"{iconName}")}";
        damageNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2) * 100);
        Destroy(damageNumber.gameObject, 0.5f);
    }

    public bool Is(Tag tag)
    {
        return tags.Contains(tag);
    }

    public bool IsGoblin()
    {
        return tags.Contains(Tag.Goblin);
    }

    public bool IsSlime()
    {
        return tags.Contains(Tag.Slime);
    }

    public bool IsRat()
    {
        return tags.Contains(Tag.Rat);
    }

    public bool IsStatue()
    {
        return tags.Contains(Tag.Statue);
    }
}
