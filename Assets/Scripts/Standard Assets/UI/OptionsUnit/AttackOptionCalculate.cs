using System.Collections.Generic;
using UnityEngine;

public class AttackOption : IOptionsUnitConditions
{
    private ScriptableObjectPlayers _currentPlayerFaction;
    private TileRefactor _currentTileAnalyzed;
    public AttackOption()
    {
        _currentPlayerFaction =
            Resources.Load<ScriptableObjectPlayers>("ScriptableObject/Data/DataSpecial/Player/CurrentPlayer");
    }
    public OptionsUnit GetOptionAssociated() => OptionsUnit.Attack;
    public bool DoesOptionMeetCondition()
    {
        var allTilesInsideUnitSelectedRange = new CalculatorUnitActionRange().CalculateTilesInsideUnitRange();

        bool thisValue = false;

            if (ThereIsTileSelectedSoldier()) return false;
        foreach (var tile in allTilesInsideUnitSelectedRange)
        {
            _currentTileAnalyzed = tile;
            if (ThereIsUnitInTile() is false) continue;
            if (IsUnitSearchedAlly()) continue;
            if (CanAttackerAttackThisUnitSearched() is false) continue;
            if (IsSpecialConditionTrue() is false) continue;

            AddEnemyToList(ref thisValue);
        }
        return thisValue;
    }

    private bool ThereIsUnitInTile() => _currentTileAnalyzed.occupiedSoldier;

    private bool ThereIsTileSelectedSoldier()
    {
        var tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;
        var tileWithCurrentUnit = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference;
        if (tileSelected.occupiedSoldier && tileSelected.occupiedSoldier != tileWithCurrentUnit.occupiedSoldier)
        {
            return true;
        }
        return false;
    }
    private bool IsUnitSearchedAlly()
    {
        FactionUnit factionUnitSearched = _currentTileAnalyzed.occupiedSoldier.playerThatCanControlThisUnit;
        FactionUnit factionAttacker = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference
        .occupiedSoldier.playerThatCanControlThisUnit;

        if (factionUnitSearched == factionAttacker) return true;
        return false;
    }

    private bool CanAttackerAttackThisUnitSearched()
    {
        Soldier attackerSoldier =
            WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference.occupiedSoldier;
        Soldier defenderSoldier = _currentTileAnalyzed.occupiedSoldier;

        var soldierDamage = attackerSoldier.baseDamageVSAnotherUnits.reference[(int)defenderSoldier.nameUnit];

        if (soldierDamage != 0) return true;
        return false;
    }

    private bool IsSpecialConditionTrue()
    {
        var soldierSelect = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference.occupiedSoldier;
        return soldierSelect.conditionsAttack.CanAttack();
    }
    private void AddEnemyToList(ref bool unitSearched)
    {
        if (!unitSearched)
        {
            unitSearched = true;
            WorldScriptableObjects.GetInstance().currentTilesWhereEnemyAre.reference = new List<TileRefactor>();
        }
        WorldScriptableObjects.GetInstance().currentTilesWhereEnemyAre.reference.Add(_currentTileAnalyzed);
    }
}
