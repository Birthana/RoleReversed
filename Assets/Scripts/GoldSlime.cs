public class GoldSlime : Monster
{
    public Monster slimePrefab;

    public override void Exit()
    {
        var room = transform.parent.GetComponent<Room>();
        var slime = Instantiate(slimePrefab, room.transform);
        room.Add(slime);
        slime = Instantiate(slimePrefab, room.transform);
        room.Add(slime);
    }
}
