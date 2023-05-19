using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileOptionMenuEdit : IStartOption
{
    public void Trigger()
    {
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.FileMenuStart, 0, false);
    }
}
