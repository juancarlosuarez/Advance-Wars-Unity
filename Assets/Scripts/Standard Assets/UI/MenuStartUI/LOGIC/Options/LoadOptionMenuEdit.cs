using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOptionMenuEdit : IStartOption
{
    public void Trigger()
    {
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.LoadMenuStart, 0, false);
    }
}
