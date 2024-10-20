using UnityEngine;
using TMPro;

public class EffectIcons : MonoBehaviour
{
    private readonly string EFFECT_ICON_PREFAB_FILE_PATH = "Prefabs/EffectIcon";

    public void SpawnEntranceIcon(Vector3 position)
    {
        SpawnEffectIcon(position, "Entrance");
    }

    public void SpawnExitIcon(Vector3 position)
    {
        SpawnEffectIcon(position, "Exit");
    }

    public void SpawnEngageIcon(Vector3 position)
    {
        SpawnEffectIcon(position, "Engage");
    }

    public void SpawnPullIcon(Vector3 position)
    {
        SpawnEffectIcon(position, "Pull");
    }

    public void SpawnPushIcon(Vector3 position)
    {
        SpawnEffectIcon(position, "Push");
    }

    private void SpawnEffectIcon(Vector3 position, string iconName)
    {
        var damageNumberPrefab = Resources.Load<GameObject>(EFFECT_ICON_PREFAB_FILE_PATH);
        var damageNumber = Instantiate(damageNumberPrefab);
        damageNumber.transform.position = position;
        damageNumber.GetComponent<TextMeshPro>().text = $"{new EffectText().GetText($"{iconName}")}";
        damageNumber.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2) * 100);
        Destroy(damageNumber, 0.5f);
    }
}
