using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusOptionMenuUI : IStartOption
{
    public void Trigger()
    {
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.SelectNewMap, 0, true);
    }
}
