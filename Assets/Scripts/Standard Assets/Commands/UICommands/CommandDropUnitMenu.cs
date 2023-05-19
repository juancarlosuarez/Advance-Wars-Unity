using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDropUnitMenu : ICommand
{
    public void Execute()
    {
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.LoadTransport, 0, true);
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
