using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Manager/BuilderTiles")]
public class BuilderTileSO : SerializedScriptableObject
{
    [SerializeField] private Dictionary<TypesOfTiles, VariableDataTileSO> _dictionaryTerrains;

    private VariableDataTileSO _currentVariableData;
    private NodeBaseRefactor _currentNodeBase;
    private int _currentIDInGrid, _maxID;
    private Vector2 _currentPosition;

    
    
    public TileRefactor BuildTile(TypesOfTiles typeOfTile, int identificationInGrid, Vector2 positionInGrid)
    {
        
        _currentVariableData = _dictionaryTerrains[typeOfTile];
        _currentNodeBase = new NodeBaseRefactor(_currentVariableData.ammountEffortToPass);
        _currentIDInGrid = identificationInGrid;
        _currentPosition = positionInGrid;

        if (WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference.ContainsKey(identificationInGrid))
        {
            CopyDataFromPreviousTile();
            return WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference[identificationInGrid];
        }
        TileRefactor newTile = new TileRefactor(_currentIDInGrid, _currentPosition, _currentVariableData, _currentNodeBase);
        WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference.Add(_currentIDInGrid, newTile);
        
        AddTexture(newTile);
        return newTile;

    }

    public void AddNeighbourToTile(int maxID)
    {
        _maxID = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference.Count - 1;
        WorldScriptableObjects.GetInstance().maxVoxelID.reference = _maxID;
        var getSearcherNeighbours = new GetTileNeighbours(_maxID);
        for (int i = 0; i < _maxID; i++)
        {
            var tileReference = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference[i];
            tileReference.SetNeighbourCross(getSearcherNeighbours.CalculateNeighbours(i, GetNumberColumne(i)));
        }
    }
    private int GetNumberColumne(int count)
    {
        if (count < 0) return 0;
        if (count > _maxID) return _maxID;
        
        return count / WorldScriptableObjects.GetInstance()._currentMapDisplayData.height + 1;
    }

    private void AddTexture(TileRefactor newTile)
    {
        var meshGrid = WorldScriptableObjects.GetInstance().gridData.meshData.mesh;
        var meshGridHighLight = WorldScriptableObjects.GetInstance().gridHighLightData.meshData.mesh;
        var changeTexture = new ChangeTextureForTile();
        changeTexture.Change(_currentVariableData.textureID, meshGrid, newTile);
        changeTexture.Change(_currentVariableData.textureID, meshGridHighLight, newTile);
    }
    private void CopyDataFromPreviousTile()
    {
        //Create and Search New and Old Tiles
        TileRefactor newTile =
            new TileRefactor(_currentIDInGrid, _currentPosition, _currentVariableData, _currentNodeBase);
        TileRefactor oldTile = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference[_currentIDInGrid];

        //Assign Corretly Soldier References
        if (oldTile.occupiedSoldier != null) UnsetUnitToTheGridRefactor.UnSetUnit(oldTile.occupiedSoldier);
        
        AddTexture(newTile);
        //Put Reference In Right Place
        WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference[_currentIDInGrid] = newTile;
        WorldScriptableObjects.GetInstance().tileSelected.reference = newTile;

        //Calculate New Neighbours
        var getSearcherNeighbours = new GetTileNeighbours(WorldScriptableObjects.GetInstance().maxVoxelID.reference);
        newTile.SetNeighbourCross(getSearcherNeighbours.CalculateNeighbours(_currentIDInGrid, GetNumberColumne(_currentIDInGrid)));
        foreach (var c in newTile.NeighboursInCross)
        {
            c.SetNeighbourCross(getSearcherNeighbours.CalculateNeighbours(c.spawnID, GetNumberColumne(c.spawnID)));
        }    
    }
}