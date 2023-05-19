using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CalculatorRangeUnitUI
{
    private List<TileRefactor> _allPossibleTilesPathForUnit;
    private HashSet<TileRefactor> _cornersFromPath;
    private TileRefactor _currentTile;
    private Soldier _currentUnit;
    
    private HashSet<TileRefactor> _allTilesNeighbour = new HashSet<TileRefactor>();
    private HashSet<TileRefactor> _tilesMasterLoop = new HashSet<TileRefactor>();
    private HashSet<TileRefactor> _tilesSlave = new HashSet<TileRefactor>();
    private HashSet<TileRefactor> _tilesPreviousRangeMin = new HashSet<TileRefactor>();

    private int _currentRound;
    
    public HashSet<TileRefactor> Calculate()
    {
        var instanceCalculatePathUnit = new WalkHighLight();
        var allTilesFromRangeUI = new HashSet<TileRefactor>();
        _currentTile = WorldScriptableObjects.GetInstance().tileSelected.reference;
        _currentUnit = _currentTile.occupiedSoldier;
        
        
        _allPossibleTilesPathForUnit = instanceCalculatePathUnit.CalculateHighLightTilesForPath(_currentTile, _currentTile.occupiedSoldier.playerThatCanControlThisUnit);
        _cornersFromPath = CalculateCorners();

        if (_currentUnit.rangeMin == 1) allTilesFromRangeUI = CalculateRange();
        else allTilesFromRangeUI = CalculateRangeStatic();
        
        return allTilesFromRangeUI;
    }

    private HashSet<TileRefactor> CalculateCorners()
    {
        HashSet<TileRefactor> corners = new HashSet<TileRefactor>(); 
        foreach (var eachTile in _allPossibleTilesPathForUnit)
        {
            foreach (var eachNeighbour in eachTile.NeighboursInCross)
            {
                if (!_allPossibleTilesPathForUnit.Contains(eachNeighbour))
                {
                    if (_currentUnit.rangeMin == 1) _allTilesNeighbour.Add(eachTile);
                    corners.Add(eachTile);
                }
            }
        }
        return corners;
    }

    private HashSet<TileRefactor> CalculateRangeStatic()
    {
        var currentUnitRangeMax = _currentUnit.rangeMax;

        PrepareReferences();
        for (int i = 2; i <= currentUnitRangeMax; i++)
        {
            _currentRound = i;
            foreach (var c in _tilesMasterLoop)
            {
                CalculateNewPossibleNeighboursStatic(c);

                if (c == _tilesMasterLoop.Last())
                {
                    AddNewNeighboursMaster();
                    PrepareValuesForAnotherInteraction();
                }
            }
        }

        return _allTilesNeighbour;
    }

    private void PrepareReferences()
    {
        //I do this because i wanna to avoid that the system keep this references, when the range min is 2
        _tilesPreviousRangeMin.Add(_currentTile);
        foreach (var tile in _currentUnit.occupiedTileRefactor.NeighboursInCross)
        {
            _tilesPreviousRangeMin.Add(tile);
            _tilesMasterLoop.Add(tile);
        }
    }
    private HashSet<TileRefactor> CalculateRange()
    {
        var currentUnitRange = _currentUnit.rangeMax;
        _tilesMasterLoop = _cornersFromPath;

        for (int i = 0; i < currentUnitRange; i++)
        {
            _currentRound = 1;
            foreach (var tile in _tilesMasterLoop)
            {
                CalculateNewPossibleNeighbours(tile);

                if (tile == _tilesMasterLoop.Last())
                {
                    AddNewNeighboursMaster();
                    PrepareValuesForAnotherInteraction();
                }
            }
        }
        
        foreach (var tile in _allPossibleTilesPathForUnit) _allTilesNeighbour.Add(tile);
        
        return _allTilesNeighbour;
    }

    private void CalculateNewPossibleNeighbours(TileRefactor tileToCheck)
    {
       // HashSet<TileRefactor> neighboursAccepted = new HashSet<TileRefactor>();

        foreach (var possibleNeighbour in tileToCheck.NeighboursInCross)
        {
            if (PathContainsThisTile(possibleNeighbour)) continue;
            AddNewNeighbourSlave(possibleNeighbour);
            //neighboursAccepted.Add(possibleNeighbour);
        }
    }

    private void CalculateNewPossibleNeighboursStatic(TileRefactor tileToCheck)
    {
        foreach (var possibleNeighbour in tileToCheck.NeighboursInCross)
        {
            AddNewNeighbourSlave(possibleNeighbour);
        }
    }
    private bool PathContainsThisTile(TileRefactor tileToCheck) => _allPossibleTilesPathForUnit.Contains(tileToCheck);
    private void AddNewNeighbourSlave(TileRefactor neighbourAccepted)
    {
        if (_currentRound >= _currentUnit.rangeMin && !_tilesPreviousRangeMin.Contains(neighbourAccepted))
        {
            _allTilesNeighbour.Add(neighbourAccepted);
        }
        else
        {
            _tilesPreviousRangeMin.Add(neighbourAccepted);
        }
        _tilesSlave.Add(neighbourAccepted);
    }
    private void AddNewNeighboursMaster()
    {
        _tilesMasterLoop = _tilesSlave;
    }
    private void PrepareValuesForAnotherInteraction()
    {
        _tilesSlave = new HashSet<TileRefactor>();
    }
}
