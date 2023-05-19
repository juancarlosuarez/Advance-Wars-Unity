using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPutAvailableAllSoldiers : ICommand
{
    private FactionUnit _unitsFromPlayers;

    public CommandPutAvailableAllSoldiers(FactionUnit unitsFromPlayers)
    {
        _unitsFromPlayers = unitsFromPlayers;
    }

    public void Execute()
    {
        var managerStats = WorldScriptableObjects.GetInstance().statsPlayersManager;

        for (int i = 1; i < 5; i++)
        {
            var eachStats = managerStats.GetStatsPlayer((FactionUnit)i);
            foreach (Soldier soldier in eachStats.GetAllSoldiers())
            {
                soldier.thisUnitCanMakeSomeAction = true;
                ChangeTransparency(soldier);
            }

        }

        FinishExecute();
    }

    private void ChangeTransparency(Soldier soldier)
    {
        Color transparentColor = soldier.spriteRenderer.color;
        transparentColor.a = 1f;
        soldier.spriteRenderer.color = transparentColor;
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
