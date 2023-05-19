using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUnitsGrid : MonoBehaviour
{
    //Posible optimizacion si debes hacerla, en vez de que cada tile tenga su archivo setunitgrid, simplemente pues crear una nueva instancia y ya, no la hago porque me da palos;
    public void SetUnitToTheGrid(Soldier @new, Tile newTile, bool callFromGridManager)
    {    
        UnSetUnitOfThePreviousTile(@new);
        SetUnitInNewTile(newTile, @new, callFromGridManager);            
    }
    public void UnSetUnitOfThePreviousTile(AbstractBaseUnit newUnit)
    {
        Tile previousTile = newUnit.occupiedTile;

        if (previousTile == null) return;
        previousTile.occupiedSoldier = null;

        if (previousTile.occupiedBuild != null) previousTile.occupiedUnit = previousTile.occupiedBuild;

        if (previousTile.occupiedBuild == null && previousTile.occupiedSoldier == null) previousTile.occupiedUnit = null;
    }
    public void SetUnitInNewTile(Tile newTile, AbstractBaseUnit newUnit, bool callFromGridManager)
    {
        newTile.occupiedUnit = newUnit;
        newUnit.occupiedTile = newTile;
        newTile.occupiedSoldier = (Soldier)newUnit;

        newUnit.transform.position = newTile.transform.position;

        if (!callFromGridManager)
        {
            var tileSelectedWithUnit = Resources.Load<TileReference>("ScriptableObject/Data/DataSpecial/TilesData/SelectedTileWithUnit");
            tileSelectedWithUnit.reference = newTile;
        }
    }
    
    public void SetSelectorToTheGrid(Soldier selector, Tile tileWhereSelectorGo)
    {
        selector.transform.position = tileWhereSelectorGo.transform.position;
    }
    public void SetBuildToTheGrid(Build newBuild, Tile tileSpawnPoint) //Esta funcion solo se llama cuando se spawnea todo el Tile.
    {
        if (newBuild.occupiedTile != null) newBuild.occupiedTile.occupiedBuild = null;


        newBuild.occupiedTile = tileSpawnPoint;
        tileSpawnPoint.occupiedBuild = newBuild;
        tileSpawnPoint.occupiedUnit = newBuild;

        newBuild.transform.position = tileSpawnPoint.transform.position;
    }
}
