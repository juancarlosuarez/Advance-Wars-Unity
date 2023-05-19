using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareValuesAttack
{
    private float _lifeDefenderAfterAttack;
    private TileRefactor _currentTileDefender;
    private TileRefactor _currentTileAttacker;
    
    //The value X is the damage from the Attacker and the Y from the defender.
    public List<Vector2> allDamagesCalculate;

    public PrepareValuesAttack()
    {
        allDamagesCalculate = new List<Vector2>();
    }

    public void PrepareValues()
    {
        var allUnitsObjective = WorldScriptableObjects.GetInstance().currentTilesWhereEnemyAre.reference;
        var tileAttacker = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference;

        foreach (var tileDefender in allUnitsObjective)
        {
            CalculateDamage(tileDefender, tileAttacker);
        }
    }
    private void CalculateDamage(TileRefactor tileDefender, TileRefactor tileAttacker)
    {
        //The damage base from the attaker vs the defender, more the luck.
        _currentTileAttacker = tileAttacker;
        _currentTileDefender = tileDefender;
        
        int baseDMGvsDefender = _currentTileAttacker.occupiedSoldier.baseDamageVSAnotherUnits.reference[(int)_currentTileDefender.occupiedSoldier.nameUnit] + CalculateLuck();
        var damageAttacker = 0;
        var damageDefender = 0;
        
        /*The life need to be expressed from 0 - 1, because need to be multiply with the baseDamage. If the unit had full life,
        the result will be the baseDamage.*/
        float currentLifeDefender = _currentTileDefender.occupiedSoldier.currentLife / 100.0f;
        float currentLifeAttacker = _currentTileAttacker.occupiedSoldier.currentLife / 100.0f;
        var defenseTerrainDefender = _currentTileDefender.dataVariable.terrainDefense;
        
        /*The dmg base will be multiply by the currentLife, by example 120 * 0.8, then the result will be less that 120,
        then it will less with the percentage of defense terrain*/
        float damageDefenderWithOutOffSet = baseDMGvsDefender * currentLifeAttacker;
        damageAttacker = (int)OffSetDamageInTerrain(defenseTerrainDefender, damageDefenderWithOutOffSet);

        /*The dmgCalculated will be substracted and store in a localvariable, because not yet we will be substracted to
         the real value*/
        _lifeDefenderAfterAttack = (currentLifeDefender * 100) - damageAttacker;
        Debug.Log("The damage From attacker is " + damageAttacker);
        
        if (_lifeDefenderAfterAttack > 0) damageDefender = CounterAttackFromDefender();

        //Here we put our data collected in the vector.
        var dataDamageBothUnits = new Vector2(damageAttacker, damageDefender);
        allDamagesCalculate.Add(dataDamageBothUnits);
    }
    private int CounterAttackFromDefender()
    {
        if (!CanUnitCounterAttack())
        {
            return 0;
        }
        var baseDMGvsAttacker =
            _currentTileDefender.occupiedSoldier.baseDamageVSAnotherUnits.reference[
                (int)_currentTileAttacker.occupiedSoldier.nameUnit] + CalculateLuck();
        var currentLifeDefender = _lifeDefenderAfterAttack / 100.0f;
        var defenseTerrainAttacker = _currentTileAttacker.dataVariable.terrainDefense;
        
        var damageDefenderWithOutOffSet = baseDMGvsAttacker * currentLifeDefender;
        var finalDamage = (int)OffSetDamageInTerrain(defenseTerrainAttacker, damageDefenderWithOutOffSet);
        
        Debug.Log("The damage From defender in counterAttack is " + finalDamage);
        return finalDamage;
    }

    private bool CanUnitCounterAttack() => IsDefenderInRange() && !IsDefenderArtillery() && DefenderCanCounterAttackThisUnit();
    private bool IsDefenderInRange()
    {
        var tileDestinationWhereAttackerWillBe = WorldScriptableObjects.GetInstance().tileSelected.reference;
        foreach (var tile in tileDestinationWhereAttackerWillBe.NeighboursInCross)
        {
            if (tile == _currentTileDefender) return true;
        }
        return false;
    }
    private bool IsDefenderArtillery()
    {
        if (_currentTileDefender.occupiedSoldier.typeSoldier == TypeSoldier.Artillery) return true;
        return false;
    }

    private bool DefenderCanCounterAttackThisUnit()
    {
        if (_currentTileDefender.occupiedSoldier.baseDamageVSAnotherUnits.reference[
                (int)_currentTileAttacker.occupiedSoldier.nameUnit] != 0) return true;
        return false;
    }
    private float OffSetDamageInTerrain(int terrainDefense, float damageWithOutOffSet)
    {
        if (terrainDefense <= 0) return damageWithOutOffSet;
        
        var offSet = damageWithOutOffSet * (terrainDefense * 10) / 100f;
        return damageWithOutOffSet - offSet;
    }
    private int CalculateLuck()
    {
        return Random.Range(1, 9);
    }
}
