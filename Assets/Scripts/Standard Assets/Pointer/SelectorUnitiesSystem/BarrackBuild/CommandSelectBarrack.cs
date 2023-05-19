using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSelectBarrack : ICommand
{
    private BarrackUnits _barrackUnits;
    public CommandSelectBarrack(BarrackUnits barrackSelected)
    {
        _barrackUnits = barrackSelected;
    }
    public void Execute()
    {
        WorldScriptableObjects.GetInstance().keyForBarrackMenuData.reference = _barrackUnits.idBarrackMenuData;
        FactoryControllersMenuStartUI.sharedInstance.OpenMenu(ControllerMenuName.BarrackMenu, 2, false);
        FinishExecute();
    }
    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
