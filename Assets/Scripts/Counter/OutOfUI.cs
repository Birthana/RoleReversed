using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfUI : BasicUI
{
    public int maxCount;

    // Start is called before the first frame update
    public override void Display(int health)
    {
        ui.text = $"{health} / {maxCount}";
    }
}
