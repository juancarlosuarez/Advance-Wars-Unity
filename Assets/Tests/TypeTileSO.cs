using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileRefactor : ITsSpawnableInGrid
{
    public TileRefactor(int spawnID, Vector2 positionTileInGrid, VariableDataTileSO dataVariable, NodeBaseRefactor dataNodeBase)
    {
        this.spawnID = spawnID;
        this.positionTileInGrid = positionTileInGrid;
        this.dataVariable = dataVariable;
        this.dataNodeBase = dataNodeBase;
    }


    public Soldier occupiedSoldier;
    public Build occupiedBuild;
    public AbstractBaseUnit occupiedUnit;

    //Data Sensible
    public readonly int spawnID;
    public readonly Vector2 positionTileInGrid;
    public List<TileRefactor> NeighboursInCross { get; set; } = new List<TileRefactor>();

    public readonly VariableDataTileSO dataVariable;
    public TilesUIDataSO dataUITile;
    public readonly NodeBaseRefactor dataNodeBase;

    public void SetNeighbourCross(List<TileRefactor> tilesNeighbours)
    {
        if (NeighboursInCross.Count > 4) return;
        
        NeighboursInCross = tilesNeighbours;
        
    }
    
}

public enum TypesOfTiles
{
    Plain, Mountain, Water, Forest, Road, River, Bridge, Beach, Reef
}