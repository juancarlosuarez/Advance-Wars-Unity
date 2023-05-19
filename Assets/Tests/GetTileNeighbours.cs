using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTileNeighbours
{
    private int _maxSize;
    private int _sizeY;
    private int _currentTileID;
    private int _currentRow;
    private List<int> _neighbourListID = new List<int>();
    private List<TileRefactor> _neighboursTileReferences = new List<TileRefactor>();
    public GetTileNeighbours(int maxSize)
    {
        _maxSize = maxSize;
        _sizeY = WorldScriptableObjects.GetInstance()._currentMapDisplayData.height;
    }
    public List<TileRefactor> CalculateNeighbours(int currentTileID, int currentRow)
    {
        _currentTileID = currentTileID;
        _currentRow = currentRow;
        _neighbourListID = new List<int>();
        _neighboursTileReferences = new List<TileRefactor>();
        
        UpNeighbour();
        DownNeighbour();
        RightNeighbour();
        LeftNeighbour();
        SearchTileReferenceAssignForEachIDCollected();
        return _neighboursTileReferences;
    }

    private void SearchTileReferenceAssignForEachIDCollected()
    {
        foreach (var c in _neighbourListID)
        {
            var tileReference = WorldScriptableObjects.GetInstance().allTilesInTheGrid.reference[c];
            _neighboursTileReferences.Add(tileReference);
        }
    }
    private void UpNeighbour()
    {
        
        int currentID = _currentTileID + 1;
        if (currentID == _sizeY * _currentRow) return;
        _neighbourListID.Add(currentID);
    }
    private void DownNeighbour()
    {
        int currentID = _currentTileID - 1;
        
        if (currentID == _sizeY * (_currentRow - 1) - 1 || currentID < 0) return;
        _neighbourListID.Add(currentID);
    }
    private void RightNeighbour()
    {
        int currentID = _currentTileID + _sizeY;
        if (currentID > _maxSize) return;
        _neighbourListID.Add(currentID);
    }
    private void LeftNeighbour()
    {
        int currentID = _currentTileID - _sizeY;
        if (currentID < 0) return;
        _neighbourListID.Add(currentID);
    }
}
