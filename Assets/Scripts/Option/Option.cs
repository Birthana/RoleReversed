using UnityEngine;
using TMPro;

public class Option : MonoBehaviour
{
    private OptionInfo optionInfo;
    private IMouseWrapper mouse;
    private PlayerSoulCounter playerSoulCount;

    public void SetMouseWrapper(IMouseWrapper wrapper) { mouse = wrapper; }

    private void Awake()
    {
        SetMouseWrapper(new MouseWrapper());
    }

    public OptionInfo GetOptionInfo() { return optionInfo; }

    public void SetOptionInfo(OptionInfo newOptionInfo)
    {
        optionInfo = newOptionInfo;
        var texts = GetComponentsInChildren<TextMeshPro>();
        texts[0].text = $"{new IconText(Color.magenta).GetNumbersText("" + newOptionInfo.cost)}";
        texts[1].text = $"{newOptionInfo.description}";
    }

    public int GetCost() { return optionInfo.cost; }

    public string GetDescription()
    {
        if (optionInfo == null)
        {
            return "";
        }

        return optionInfo.description;
    }

    public void SetPlayerSoulCount()
    {
        if (playerSoulCount != null)
        {
            return;
        }

        playerSoulCount = FindObjectOfType<PlayerSoulCounter>();
    }

    public void Update()
    {
        SetPlayerSoulCount();
        if (mouse.PlayerPressesLeftClick() && mouse.IsOnOption() &&
            (playerSoulCount.GetCurrentSouls()) > 0 && (optionInfo.cost <= playerSoulCount.GetCurrentSouls()))
        {
            if (OptionIsNotClicked())
            {
                return;
            }

            optionInfo.Choose();
            optionInfo = null;
            SetOptionInfo(FindObjectOfType<SoulShop>().GetRandomOptionInfo());
        }
    }

    private bool OptionIsNotClicked()
    {
        if (mouse.GetHitComponent<Option>() == null)
        {
            return true;
        }

        return !mouse.GetHitComponent<Option>().gameObject.Equals(gameObject);
    }
}
