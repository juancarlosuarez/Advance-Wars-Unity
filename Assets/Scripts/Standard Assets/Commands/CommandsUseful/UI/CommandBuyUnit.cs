using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBuyUnit : ICommand
{
    private FactionUnit _factionUnit;
    private int _goldSpend;
    public CommandBuyUnit(FactionUnit factionUnit, int goldSpend)
    {
        _factionUnit = factionUnit;
        _goldSpend = goldSpend;
    }
    public void Execute()
    {
        
        var statsPlayer = WorldScriptableObjects.GetInstance().statsPlayersManager.GetStatsPlayer(_factionUnit);
        statsPlayer.SetGoldAmount(-_goldSpend);
        CommandQueue.GetInstance.AddCommand(new CommandUpdateInterfaceGamePlay(), false);
        FinishExecute();
    }

    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
