using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpOptionMenuEdit : IStartOption
{
    public void Trigger()
    {
        Debug.Log("HelpOption");
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.HelpMenuStart1, 0, true);
    }
}
