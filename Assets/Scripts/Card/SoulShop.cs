using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulShop : MonoBehaviour
{
    public Sprite openShop;
    public Sprite closeShop;
    public List<OptionInfo> optionInfos = new List<OptionInfo>();
    public Option optionPrefab;
    public float SPACING = 5.0f;
    private static readonly string OPTIONS_FILE_PATH = "Prefabs/Options";
    private List<Option> options = new List<Option>();
    private IMouseWrapper mouse;

    public void SetMouseWrapper(IMouseWrapper wrapper) { mouse = wrapper; }

    private void Awake()
    {
        SetMouseWrapper(new MouseWrapper());
        LoadOptionInfos(OPTIONS_FILE_PATH);
    }

    private void LoadOptionInfos(string path)
    {
        var resourceOptionInfos = Resources.LoadAll<OptionInfo>(path);
        foreach (var resourceOptionInfo in resourceOptionInfos)
        {
            optionInfos.Add(resourceOptionInfo);
        }
    }


    public void Update()
    {
        if (PlayerDoesNotClickOnShop())
        {
            return;
        }

        if (IsOpen())
        {
            CloseShop();
            return;
        }

        OpenShop();
        SetSprite(closeShop);
    }

    private void SetSprite(Sprite sprite)
    {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer == null)
        {
            return;
        }

        renderer.sprite = sprite;
    }

    private bool PlayerDoesNotClickOnShop() { return (!mouse.PlayerPressesLeftClick() || !mouse.IsOnOpenSoulShop()); }

    public void CloseShop()
    {
        HideOptions();
        SetSprite(openShop);
    }

    public void OpenShop()
    {
        if (!ShopIsEmpty())
        {
            ShowOptions();
            return;
        }

        CreateNewOptions();
        DisplayOptions();
    }

    private void ShowOptions() { SetOptionsActive(true); }

    private void HideOptions() { SetOptionsActive(false); }

    private void SetOptionsActive(bool state)
    {
        foreach (var option in options)
        {
            option.gameObject.SetActive(state);
        }
    }

    private void CreateNewOptions()
    {
        for (int i = 0; i < 3; i++)
        {
            var newOption = CreateRandomOption();
            options.Add(newOption);
        }
    }

    private Option CreateRandomOption()
    {
        var newOption = Instantiate(optionPrefab, transform);
        var rngOptionInfo = GetRandomOptionInfo();
        newOption.SetOptionInfo(rngOptionInfo);
        return newOption;
    }

    private bool OptionInfoIsNotUnique(OptionInfo newOptionInfo)
    {
        if (OptionsAreInValid())
        {
            return false;
        }

        foreach (var option in options)
        {
            if (OptionInfoIsSameAsOption(newOptionInfo, option))
            {
                return true;
            }
        }

        return false;
    }

    private bool OptionsAreInValid() { return optionInfos.Count < 3 || options.Count == 0; }

    private bool OptionInfoIsSameAsOption(OptionInfo optionInfo, Option option)
    {
        return optionInfo.description.Equals(option.GetDescription());
    }

    public OptionInfo GetRandomOptionInfo()
    {
        OptionInfo rngOptionInfo;
        do
        {
            rngOptionInfo = optionInfos[Random.Range(0, optionInfos.Count)];
        } while (OptionInfoIsNotUnique(rngOptionInfo));
        
        return rngOptionInfo;
    }

    private void DisplayOptions()
    {
        var centerPosition = new CenterPosition(Vector3.zero, options.Count, SPACING);
        for (int i = 0; i < options.Count; i++)
        {
            options[i].transform.position = centerPosition.GetHorizontalOffsetPositionAt(i);
        }
    }

    private bool ShopIsEmpty() { return options.Count == 0; }

    public bool IsOpen()
    {
        if (ShopIsEmpty())
        {
            return false;
        }

        return options[0].gameObject.activeSelf;
    }
}
