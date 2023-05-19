using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDestroyUnit : ICommand
{
    private Soldier _currentSoldier;
    private bool _hadToBeDestroyImmediately;

    public CommandDestroyUnit(Soldier currentSoldier, bool hadToBeDestroyImmediately)
    {
        _currentSoldier = currentSoldier;
        _hadToBeDestroyImmediately = hadToBeDestroyImmediately;
    }
    public void Execute()
    {
        if (_hadToBeDestroyImmediately)
        {
            DestroySoldier();
            FinishExecute();
            return;
        }
        if (_currentSoldier == null)
        {
            SoundManager._sharedInstance.PlayEffectSound(EffectNames.Error);
            FinishExecute();
            return;
        }
        if (CanBeDestroyThisSoldier()) DestroySoldier();
        FinishExecute();
    }

    private bool CanBeDestroyThisSoldier()
    {
        var currentPlayer = WorldScriptableObjects.GetInstance().currentPLayer.reference;

        if (currentPlayer == _currentSoldier.playerThatCanControlThisUnit) return true;
        return false;
    }
    private void DestroySoldier()
    {
        var managerStats = WorldScriptableObjects.GetInstance().statsPlayersManager
            .GetStatsPlayer(_currentSoldier.playerThatCanControlThisUnit);
        managerStats.RemoveSoldier(_currentSoldier);
        if (WasUnitConquerBuild()) StopConquerBuild(); 
        UnsetUnitToTheGridRefactor.UnSetUnit(_currentSoldier);
        GameObject.Destroy(_currentSoldier.gameObject);
        CommandQueue.GetInstance.AddCommand(new CommandUpdateR1L1Stats(), false);
    }

    private bool WasUnitConquerBuild()
    {
        return _currentSoldier.thisUnitIsConquerCity;
    }

    private void StopConquerBuild()
    {
        var buildBeingConquer = _currentSoldier.occupiedTileRefactor.occupiedBuild;
        buildBeingConquer.isThisBeingConquered = false;
        buildBeingConquer.currentLifeUI = 20;
    }
    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
