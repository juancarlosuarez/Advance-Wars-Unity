using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CalculatorUnitActionRange
{
    private readonly TileRefactor _tileSelected;

    private int _currentInteraction;

    private readonly int _rangeMin, _rangeMax;
    private HashSet<TileRefactor> _tilesSlaveLoop = new HashSet<TileRefactor>();
    private HashSet<TileRefactor> _tilesMasterLoop = new HashSet<TileRefactor>();
    private readonly HashSet<TileRefactor> _tilesFinal = new HashSet<TileRefactor>();
    private readonly HashSet<TileRefactor> _allTiles = new HashSet<TileRefactor>();

    public CalculatorUnitActionRange()
    {
        _tileSelected = WorldScriptableObjects.GetInstance().tileSelected.reference;
        
        var tileSelectedWithUnit = WorldScriptableObjects.GetInstance().tileSelectedWithUnit.reference;
        _rangeMin = tileSelectedWithUnit.occupiedSoldier.rangeMin;
        _rangeMax = tileSelectedWithUnit.occupiedSoldier.rangeMax;
    }
    public List<TileRefactor> CalculateTilesInsideUnitRange()
    {
        if (_rangeMin == 1 && _rangeMax == 1) return _tileSelected.NeighboursInCross;
        
        _tilesMasterLoop = CalculateNewPossibleNeighbours(_tileSelected);
        for (int i = 1; i <= _rangeMax; i++)
        {
            _currentInteraction = i;
            //I do this because the first interaction is just the cross tiles. If i dont put this the range wont be detected
            //in the range minimum
            if (_currentInteraction == 1)
            {
                CalculateNewPossibleNeighbours(_tileSelected);
                continue;
            }
            foreach (var tile in _tilesMasterLoop)
            {
                CalculateNewPossibleNeighbours(tile);

                if (tile == _tilesMasterLoop.Last())
                {
                    PrepareValuesForAnotherInteraction();
                    break;
                }
            }       
        }
        return _tilesFinal.ToList();
    }
    private HashSet<TileRefactor> CalculateNewPossibleNeighbours(TileRefactor tileToCheck)
    {
        HashSet<TileRefactor> localLoop = new HashSet<TileRefactor>();
        foreach (var tile in tileToCheck.NeighboursInCross)
        {
            if (tile == _tileSelected) continue;

            bool tileIsValidForFinal = _currentInteraction >= _rangeMin && !_allTiles.Contains(tile);
            
            _tilesSlaveLoop.Add(tile);
            localLoop.Add(tile);
            if (tileIsValidForFinal)
            {
                _tilesFinal.Add(tile);
            }
            _allTiles.Add(tile);

        }
        return localLoop;
    }
    private void PrepareValuesForAnotherInteraction()
    {
        _tilesMasterLoop = _tilesSlaveLoop;
        _tilesSlaveLoop = new HashSet<TileRefactor>();
    }
}
