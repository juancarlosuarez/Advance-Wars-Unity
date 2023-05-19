using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandAddLifeToUnitInBuilds : ICommand
{
    public void Execute()
    {
        var currentPlayer = WorldScriptableObjects.GetInstance().currentPLayer.reference;
        var currentStats = WorldScriptableObjects.GetInstance().statsPlayersManager.GetStatsPlayer(currentPlayer);
        var allSoldiersInBuilds = currentStats.GetAllSoldiers().Where(soldier =>
            soldier.occupiedTileRefactor.occupiedBuild &&
            soldier.occupiedTileRefactor.occupiedBuild.playerThatCanControlThisUnit == currentPlayer);
        
        foreach (var soldier in allSoldiersInBuilds)
        {
            if (soldier.currentLifeUI == 10) continue;
            

            var currentLifeUITest = soldier.currentLifeUI + 2;
            var currentLifeTest = soldier.currentLife + 20;
            
            soldier.currentLifeUI = currentLifeUITest > 10 ? 10 : currentLifeUITest;
            soldier.currentLife = currentLifeTest > 100 ? 100 : currentLifeTest;
            
            soldier.managerLifeUnitUI.SetLife(soldier);    

        }
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
