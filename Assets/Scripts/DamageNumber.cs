using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [Range(0, 1)]public float duration = 0.5f;
    private string numberPrefabFilePath = "Prefabs/DamageNumber";

    private void Awake()
    {
        GetComponent<Health>().OnTakeDamage += SpawnDamageNumber;
    }

    public void SpawnDamageNumber(int damage)
    {
        if (damage == 0)
        {
            return;
        }

        var prefab = Resources.Load(numberPrefabFilePath);
        var damageNumber = Instantiate(prefab, transform) as GameObject;
        damageNumber.GetComponent<TextMeshPro>().text = $"-{damage}";
        damageNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * 100);
        Destroy(damageNumber.gameObject, duration);
    }
}
