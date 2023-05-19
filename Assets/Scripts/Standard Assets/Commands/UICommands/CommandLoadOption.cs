using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CommandLoadOption : ICommand
{
    public void Execute()
    {
        var unitTransporter = (TransportSoldier)WorldScriptableObjects.GetInstance().tileSelected.reference.occupiedSoldier;
        var soldierInfantry = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference.occupiedSoldier;
        
        unitTransporter.PutUnitInsideTransport(soldierInfantry);
        
        PlayerController.sharedInstance.ChangeControlToGamePlay();
        
        CommandQueue.GetInstance.AddCommand(new CommandDisableUnit(soldierInfantry), false);
        CommandQueue.GetInstance.AddCommand(new ActiveSelectCommand(), false);
        CommandQueue.GetInstance.AddCommand(new DeselectUnitCommand(), false);
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
