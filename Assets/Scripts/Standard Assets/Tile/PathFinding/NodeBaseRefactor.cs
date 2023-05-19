using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBaseRefactor
{
    private int _gCost;
    private int _hCost;
    private int _fCost;
    private TileRefactor _cameFromCell;
    private int _effortNormalize; //New
    private int _effortTerrain; //New
    private int _cameFromCellSteps;

    public NodeBaseRefactor(int effortNormalize)
    {
        _effortNormalize = effortNormalize;
        SetAmountEffort();
    }

    private void SetAmountEffort()
    {
        switch (_effortNormalize)
        {
            case 1: _effortTerrain = 1;
                break;
            case 2: _effortTerrain = 89999999;
                break;
            case 3: _effortTerrain = 10000000;
                break;
        }
    }
    public void SetGCost(int value)
    {
        _gCost = value;
    }
    public int GetGCost()
    {
        return _gCost;
    }
    public void SetHCost(int value)
    {
        _hCost = value;
    }

    public void CalculateFCost()
    {
        _fCost = _gCost + _hCost + (_effortNormalize);
    }

    public int GetFCost()
    {
        return _fCost;
    }

    public int GetHCost() //Esto no se usa solo para test
    {
        return _hCost;
    }
    public void SetCameFromCell(TileRefactor previousCell)
    {
        _cameFromCell = previousCell;
    }
    public TileRefactor GetCameFromCell()
    {
        return _cameFromCell;
    }

    public int GetCameFromCellSteps()
    {
        return _cameFromCellSteps;
    }

    public void SetCameFromCellSteps(int cameFromCellSteps)
    {
        _cameFromCellSteps = cameFromCellSteps;
    }
}
