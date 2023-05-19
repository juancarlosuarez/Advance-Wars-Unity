using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTileWithNormalizedVector
{
    private float _xPositionRaw;
    private float _yPositionRaw;
    private WorldScriptableObjects WSO;

    private int _xSize;
    private int _ySize;

    public FindTileWithNormalizedVector()
    {
        WSO = WorldScriptableObjects.GetInstance();
    }
    
    public TileRefactor GetTileByVectorNormalized(Vector2 newPositionRaw)
    {
        if (newPositionRaw == Vector2.zero) return null;
        _ySize = WorldScriptableObjects.GetInstance()._currentMapDisplayData.height;

        _xPositionRaw = newPositionRaw.x;
        _yPositionRaw = newPositionRaw.y;

        CheckRightDirection();
        CheckLeftDirection();
        CheckDownDirection();
        CheckUpDirection();
        
        return CalculateTileAssignToID();
    }
    private void CheckRightDirection()
    {
        if (_xPositionRaw == 0 || _xPositionRaw < 0) return;
        if (WSO.currentVoxelID.reference + _ySize > WSO.maxVoxelID.reference) return;

        WSO.currentVoxelID.reference += _ySize;
    }
    private void CheckLeftDirection()
    {
        if (_xPositionRaw == 0 || _xPositionRaw > 0) return;
        if (WSO.currentVoxelID.reference - _ySize < 0) return;

        WSO.currentVoxelID.reference -= _ySize;
    }
    private void CheckUpDirection()
    {
        if (_yPositionRaw == 0 || _yPositionRaw < 0) return;
        if (WSO.currentVoxelID.reference + 1 == _ySize * (WSO.currentVoxelID.reference / _ySize + 1)) return;

        WSO.currentVoxelID.reference++;
    }
    private void CheckDownDirection()
    {
        if (_yPositionRaw == 0 || _yPositionRaw > 0) return;
        if (WSO.currentVoxelID.reference == _ySize * (WSO.currentVoxelID.reference / _ySize)) return;

        WSO.currentVoxelID.reference--;
    }
    private TileRefactor CalculateTileAssignToID() => WSO.allTilesInTheGrid.reference[WorldScriptableObjects.GetInstance().currentVoxelID.reference];

}
