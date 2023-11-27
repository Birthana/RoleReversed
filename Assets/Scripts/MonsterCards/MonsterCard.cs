using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MonsterInfo
{
    public Sprite sprite;
    public int damage;
    public int health;
}

public class MonsterCard : Card
{
    public Monster monsterPrefab;
    private Transform selectedRoom;
    private MonsterInfo monsterInfo;
    private MonsterCardInfo monsterCardInfo;

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        monsterPrefab = newCardInfo.prefab.GetComponent<Monster>();
        monsterCardInfo = (MonsterCardInfo)newCardInfo;
        monsterInfo.sprite = newCardInfo.fieldSprite;
        monsterInfo.damage = ((MonsterCardInfo)newCardInfo).damage;
        monsterInfo.health = ((MonsterCardInfo)newCardInfo).health;
        base.SetCardInfo(newCardInfo);
    }

    public override bool HasTarget()
    {
        if (Mouse.IsOnRoom())
        {
            selectedRoom = Mouse.GetHitTransform();
            if (selectedRoom.GetComponent<Room>().HasCapacity())
            {
                return true;
            }
        }

        return false;
    }

    public override void Cast()
    {
        SpawnMonster();
        base.Cast();
    }

    private void SpawnMonster()
    {
        var monster = Instantiate(monsterPrefab, selectedRoom);
        monster.GetComponent<SpriteRenderer>().sprite = monsterInfo.sprite;
        monster.cardInfo = monsterCardInfo;
        monster.Entrance();
        monster.damageStat = monsterInfo.damage;
        monster.healthStat = monsterInfo.health;
        monster.SetupStats();
        var room = selectedRoom.GetComponent<Room>();
        room.Add(monster);
        room.ReduceCapacity(1);
    }
}
