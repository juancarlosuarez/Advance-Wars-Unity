using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorOptionsStartMenuUI : IStartOption
{
    public void Trigger()
    {
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.OptionEdits, 0, true);
    }
}
