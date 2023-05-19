using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit
{
    //Borrar esto
    private SOListUnitsPrefabs unitsPrefabs;
    
    public SpawnUnit()
    {
        unitsPrefabs = Resources.Load<SOListUnitsPrefabs>("ScriptableObject/Data/DataSpecial/ListUnitsPrefabs/DictionaryUnits");
    }

    public void Spawn(NameUnit nameUnit, FactionUnit factionUnit, Tile tileWhereMyUnitWillBe)
    {
        var unitPrefab = unitsPrefabs.GetDictionaryOfUnits()[nameUnit][(int)factionUnit];

        if (tileWhereMyUnitWillBe.occupiedSoldier && unitPrefab.typeUnit != TypesUnit.Build) return;

        var unit = Object.Instantiate(unitPrefab);
        
        if (unitPrefab.typeUnit == TypesUnit.Soldier) tileWhereMyUnitWillBe.setUnitComponent.SetUnitToTheGrid((Soldier)unit, tileWhereMyUnitWillBe, false);
        if (unitPrefab.typeUnit == TypesUnit.Build) tileWhereMyUnitWillBe.setUnitComponent.SetBuildToTheGrid((Build)unit, tileWhereMyUnitWillBe);
    }

    public void NewSpawnSystem(NameUnit nameUnit, FactionUnit factionUnit, TileRefactor tileWhereWillMyUnitIs)
    {
        
    }



}
