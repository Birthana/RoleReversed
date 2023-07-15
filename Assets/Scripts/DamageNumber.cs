using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public GameObject numberPrefab;
    public float duration = 0.5f;

    private void Awake()
    {
        GetComponent<Health>().OnTakeDamage += SpawnDamageNumber;
    }

    public void SpawnDamageNumber(int damage)
    {
        var damageNumber = Instantiate(numberPrefab, transform);
        damageNumber.GetComponent<TextMeshPro>().text = $"-{damage}";
        damageNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * 100);
        Destroy(damageNumber.gameObject, duration);
    }
}
