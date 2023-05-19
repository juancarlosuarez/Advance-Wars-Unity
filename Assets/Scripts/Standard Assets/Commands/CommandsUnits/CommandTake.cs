using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTake : ICommand
{
    private Soldier _currentSoldier;
    private Build _currentBuild;
    private int _currentDamageFromUnit;
    public void Execute()
    {
        _currentSoldier = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference.occupiedSoldier;
        _currentBuild = WorldScriptableObjects.GetInstance().tileSelected.reference.occupiedBuild;
        _currentDamageFromUnit = _currentSoldier.currentLife / 10;

        var currentLife = _currentBuild.currentLifeUI - _currentDamageFromUnit;

        if (currentLife <= 0)
            ConquerBuild();
        if (currentLife > 0) BesiegeBuild();
        
        
        
        
        FinishExecute();
    }

    private void ConquerBuild()
    {
        var nameBuild = _currentBuild.nameUnit;
        var factionUnitConquer = _currentSoldier.playerThatCanControlThisUnit;
        var tileWhereWillBeSpawned = _currentBuild.occupiedTileRefactor;
        
        if (nameBuild == NameUnit.Base)
        {
            if (factionUnitConquer == FactionUnit.Player1) CommandQueue.GetInstance.AddCommand(new CommandWinScreen(), true);
            else CommandQueue.GetInstance.AddCommand(new CommandLoseGame(), true);
            return;
        }

        var dataNewBuild = new UnitDataPrefab
        {
            factionUnit = factionUnitConquer,
            nameUnit = nameBuild
        };
        var spawnerBuild = new SpawnerBuilds();
        _currentSoldier.thisUnitIsConquerCity = false;
        spawnerBuild.PutElement(dataNewBuild, tileWhereWillBeSpawned);
    }
    private void BesiegeBuild()
    {
        _currentBuild.currentLifeUI -= _currentDamageFromUnit;
        _currentBuild.isThisBeingConquered = true;
        _currentSoldier.thisUnitIsConquerCity = true;
    }
    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
