using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulShop : MonoBehaviour
{
    public Option optionPrefab;
    public float SPACING = 5.0f;
    private List<Option> options = new List<Option>();
    private IMouseWrapper mouse;

    public void SetMouseWrapper(IMouseWrapper wrapper)
    {
        mouse = wrapper;
    }

    private void Awake()
    {
        SetMouseWrapper(new MouseWrapper());
    }

    public void Update()
    {
        if (!mouse.PlayerPressesLeftClick() || !mouse.IsOnOpenSoulShop())
        {
            return;
        }

        if (IsOpen())
        {
            CloseShop();
            return;
        }

        OpenShop();
    }

    private void CloseShop() { HideOptions(); }

    private void OpenShop()
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
            var newOption = Instantiate(optionPrefab);
            newOption.transform.SetParent(transform);
            options.Add(newOption);
        }
    }

    private void DisplayOptions()
    {
        var centerPosition = new CenterPosition(Vector3.zero, options.Count, SPACING);
        for (int i = 0; i < options.Count; i++)
        {
            options[i].transform.position = centerPosition.GetHorizontalOffsetPositionAt(i);
        }
    }

    private bool ShopIsEmpty()
    {
        return options.Count == 0;
    }

    public bool IsOpen()
    {
        if (ShopIsEmpty())
        {
            return false;
        }

        return options[0].gameObject.activeSelf;
    }
}
