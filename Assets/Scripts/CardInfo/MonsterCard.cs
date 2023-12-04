using UnityEngine;

public class MonsterCard : Card
{
    private static readonly string FIELD_MONSTER_PREFAB = "Prefabs/FieldMonster";
    private Monster monsterPrefab;
    private Transform selectedRoom;
    private MonsterCardInfo monsterCardInfo;

    public void SetMonsterPrefab(Monster newMonsterPrefab)
    {
        monsterPrefab = newMonsterPrefab;
    }

    public Monster GetMonsterPrefab()
    {
        return monsterPrefab;
    }

    public override void SetCardInfo(CardInfo newCardInfo)
    {
        monsterPrefab = Resources.Load<Monster>(FIELD_MONSTER_PREFAB);
        monsterCardInfo = (MonsterCardInfo)newCardInfo;
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
        monster.GetComponent<SpriteRenderer>().sprite = monsterCardInfo.fieldSprite;
        monster.cardInfo = monsterCardInfo;
        monster.Entrance();
        monster.damageStat = monsterCardInfo.GetDamage();
        monster.healthStat = monsterCardInfo.GetHealth();
        monster.SetupStats();
        var room = selectedRoom.GetComponent<Room>();
        room.Add(monster);
        room.ReduceCapacity(1);
    }
}
