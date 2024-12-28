using UnityEngine;

public class MoveLine : MonoBehaviour
{
    private Monster monster;
    private LineRenderer line;

    private void Awake()
    {
        monster = GetComponent<Monster>();
        line = GetComponent<LineRenderer>();
        Disable();
        SetStartPoint();
    }

    private void Update()
    {
        if (line.enabled)
        {
            MoveEndPoint();
        }
    }

    public void SetStartPoint()
    {
        line.SetPosition(1, monster.GetCurrentPosition());
    }

    public void Enable() { line.enabled = true; }

    public void Disable() { line.enabled = false; }

    private void MoveEndPoint()
    {
        line.SetPosition(0, monster.transform.position);
    }
}
