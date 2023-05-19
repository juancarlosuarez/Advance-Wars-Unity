using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandStopConquerCity : ICommand
{
    public void Execute()
    {
        var currentTileUnit = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference;
        var currentSoldier = currentTileUnit.occupiedSoldier;
        if (currentSoldier.thisUnitIsConquerCity)
        {
            currentSoldier.thisUnitIsConquerCity = false;
            currentTileUnit.occupiedBuild.currentLifeUI = 20;
        }
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
