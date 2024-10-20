using UnityEngine;

public struct EffectInput
{
    public Player player;
    public Room room;
    public Vector3 position;
    public Monster monster;
    public MonsterCard card;

    public EffectInput(Player player, Room room)
    {
        this.player = player;
        this.room = room;
        position = Vector3.zero;
        monster = null;
        card = null;
    }

    public EffectInput(Player player, Room room, Vector3 position, Monster monster)
    {
        this.player = player;
        this.room = room;
        this.position = position;
        this.monster = monster;
        card = null;
    }

    public void IncreaseMonsterStats(int damage, int health)
    {
        if (monster == null)
        {
            card.IncreaseStats(damage, health);
            return;
        }

        monster.IncreaseStats(damage, health);
    }
}
