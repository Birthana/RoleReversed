using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [Range(0, 1)]public float duration = 0.5f;
    private string numberPrefabFilePath = "Prefabs/DamageNumber";
    private Health health;
    private GameObject damageNumberPrefab;

    private Health GetHealth()
    {
        if (health == null)
        {
            health = GetComponent<Health>();
        }

        return health;
    }

    private void Awake()
    {
        damageNumberPrefab = Resources.Load<GameObject>(numberPrefabFilePath);
        GetHealth().OnTakeDamage += TryToSpawnDamageNumber;
    }

    public void TryToSpawnDamageNumber(int damage)
    {
        if (damage == 0)
        {
            return;
        }

        SpawnDamageNumberWithDespawnTimer(damage);
    }

    private void SpawnDamageNumberWithDespawnTimer(int damage)
    {
        var damageNumber = Instantiate(damageNumberPrefab, transform);
        damageNumber.GetComponent<TextMeshPro>().text = $"{new IconText(Color.red).GetNumbersText($"-{damage}")}";
        damageNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * 100);
        Destroy(damageNumber.gameObject, duration);
    }
}
