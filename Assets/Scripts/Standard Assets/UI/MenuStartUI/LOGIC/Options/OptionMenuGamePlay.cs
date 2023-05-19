using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenuGamePlay : IStartOption
{
    public void Trigger()
    {
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.OptionStartGameplay, 0, false);
    }
}
