using UnityEngine;

[RequireComponent(typeof(Character))]
public class Player : MonoBehaviour
{
    private Character stats;

    private void Awake()
    {
        stats = GetComponent<Character>();
    }
}
