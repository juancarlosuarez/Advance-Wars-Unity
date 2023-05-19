using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandUpdateInterfacesChangeTurn : ICommand
{
    public static event Action UpdateInterfaceByChangeTurn;
    public void Execute()
    {
        var player = WorldScriptableObjects.GetInstance().currentPLayer.reference;

        if (WorldScriptableObjects.GetInstance()._currentMapDisplayData.currentDays != 1)
        {
            WorldScriptableObjects.GetInstance().statsPlayersManager.GetStatsPlayer(player).CalculateGoldByEachTurn();
        }
        CommandQueue.GetInstance.AddCommand(new CommandUpdateInterfaceGamePlay(), false);
        
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
