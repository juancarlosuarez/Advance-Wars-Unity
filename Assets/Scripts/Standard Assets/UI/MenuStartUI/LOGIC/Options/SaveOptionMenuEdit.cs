using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOptionMenuEdit : IStartOption
{
    public void Trigger()
    {
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.SaveMenuStart, 0, false);
    }
}
