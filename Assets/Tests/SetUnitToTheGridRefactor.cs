using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUnitToTheGridRefactor
{
    public void SetUnit(TileRefactor newTileForTheUnit, Soldier newSoldier)
    {
        newTileForTheUnit.occupiedUnit = newSoldier;
        newTileForTheUnit.occupiedSoldier = newSoldier;
        newSoldier.occupiedTileRefactor = newTileForTheUnit;
        newSoldier.transform.position = newTileForTheUnit.positionTileInGrid;
    }

    public void SetBuild(TileRefactor newTileForTheBuild, Build newBuild)
    {
        if (newBuild.occupiedTile != null) newBuild.occupiedTile.occupiedBuild = null;

        newBuild.occupiedTileRefactor = newTileForTheBuild;
        newTileForTheBuild.occupiedBuild = newBuild;
        
        if (newTileForTheBuild.occupiedSoldier == null) newTileForTheBuild.occupiedUnit = newBuild;

        newBuild.transform.position = newTileForTheBuild.positionTileInGrid;
    }
}
