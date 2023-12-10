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
        SetSprite(newCardInfo.cardSprite);
        SetDescription(newCardInfo.effectDescription);
    }

    public override int GetCost()
    {
        return monsterCardInfo.cost;
    }

    public override string GetName()
    {
        return monsterCardInfo.cardName;
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
        monster.SetupStats(monsterCardInfo.GetDamage(), monsterCardInfo.GetHealth());
        var room = selectedRoom.GetComponent<Room>();
        room.Add(monster);
        room.ReduceCapacity(1);
    }
}
