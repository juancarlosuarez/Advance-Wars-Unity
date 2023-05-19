using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnsetUnitToTheGridRefactor 
{
    public static void UnSetUnit(Soldier previousUnit)
    {
        TileRefactor previousTile = previousUnit.occupiedTileRefactor;
        //Prevention Null References
        if (previousTile == null) return;

        previousTile.occupiedSoldier = null;
        
        if (previousTile.occupiedBuild != null) previousTile.occupiedUnit = previousTile.occupiedBuild;
        
        if (previousTile.occupiedBuild == null && previousTile.occupiedSoldier == null)
            previousTile.occupiedUnit = null;
    }

    public static void UnSetBuild(Build previousBuild)
    {
        Debug.Log("hmmmmmm");
        var currentMap = WorldScriptableObjects.GetInstance()._currentMapDisplayData;
        currentMap.allBuildInGrid.Remove(previousBuild);

        TileRefactor previousTile = previousBuild.occupiedTileRefactor;
        
        if (previousTile == null) return;

        previousTile.occupiedBuild = null;
        
        if (previousTile.occupiedSoldier == null) previousTile.occupiedUnit = null;
    }

    //This class just need to be used To DESTROY a unit
    public static void CleanReferences(TileRefactor tile)
    {
        var currentMapData = WorldScriptableObjects.GetInstance()._currentMapDisplayData;
        var managerStats = WorldScriptableObjects.GetInstance().statsPlayersManager;
        
        if (tile.occupiedBuild != null)
        {
            currentMapData.allBuildInGrid.Remove(tile.occupiedBuild);

            if (tile.occupiedBuild.playerThatCanControlThisUnit != FactionUnit.Neutral)
            {
                var currentPlayer = tile.occupiedBuild.playerThatCanControlThisUnit;
                var currentStatsPlayer = managerStats.GetStatsPlayer(currentPlayer);
                if (tile.occupiedBuild.typeOfBuild == TypeOfBuild.City) currentStatsPlayer.SetCityAmount(false);
                if (tile.occupiedBuild.typeOfBuild == TypeOfBuild.Military) currentStatsPlayer.RemoveBarrack((BarrackUnits)tile.occupiedBuild);
            }
            tile.occupiedBuild = null;
        }
        if (tile.occupiedSoldier != null)
        {
            currentMapData.allSoldierInGrid.Remove(tile.occupiedSoldier);

            if (tile.occupiedSoldier.playerThatCanControlThisUnit != FactionUnit.Neutral)
            {
                var currentPlayer = tile.occupiedSoldier.playerThatCanControlThisUnit;
                var currentStatsPlayer = managerStats.GetStatsPlayer(currentPlayer);
                currentStatsPlayer.RemoveSoldier(tile.occupiedSoldier);
            }
            tile.occupiedSoldier = null;
        }
        tile.occupiedUnit = null;
    }
}
