using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WalkHighLight
{
    private HashSet<TileRefactor> _allTilesNeighbour = new HashSet<TileRefactor>();
    private List<TileRefactor> _tilesMasterLoop = new List<TileRefactor>();
    private List<TileRefactor> _tilesSlave = new List<TileRefactor>();

    private List<int> _stepsAvailableLoopMaster = new List<int>();
    private List<int> _stepsAvailableSlave = new List<int>();

    private int _stepsAvailable;
    private bool _isFirstRoundComplete = false;

    private Soldier _currentUnit;

    private FactionUnit _currentPlayer;
    private int count;
    private int pene = 0;
    public List<TileRefactor> CalculateHighLightTilesForPath(TileRefactor originTile, FactionUnit currentPlayer)
    {
        _currentUnit = originTile.occupiedSoldier;
        _currentPlayer = currentPlayer;
        
        if (_currentUnit == null) Debug.Log("This Tile Dont had Unit");

        
        //foreach (var c in originTile.NeighboursInCross)
        //{
        _isFirstRoundComplete = false;
        _tilesMasterLoop = CalculateNewPossibleNeighbours(originTile, _currentUnit.numberMoveAvailable);
        for (int eachStep = 0; eachStep < _currentUnit.numberMoveAvailable; eachStep++)
        {
            count = 0;
            foreach (var tile in _tilesMasterLoop)
            {
                if (count < _stepsAvailableLoopMaster.Count)
                {
                    CalculateNewPossibleNeighbours(tile, _stepsAvailableLoopMaster[count]);
                } 
                else break;

                if (tile == _tilesMasterLoop.Last())
                {
                    AddNewNeighboursMaster();
                    PrepareValuesForAnotherInteraction();
                }

                count++;
            }
        }

        //}
        _allTilesNeighbour.Add(originTile);
        return _allTilesNeighbour.ToList();
    }
    private List<TileRefactor> CalculateNewPossibleNeighbours(TileRefactor tileToCheck, int stepsAvailable)
    {
        List<TileRefactor> neighboursAccepted = new List<TileRefactor>();

        foreach (TileRefactor newPossibleNeighbour in tileToCheck.NeighboursInCross)
        {
            // if (tileToCheck.spawnID == 33)
            // {
            //     pene++;
            //     if(newPossibleNeighbour.spawnID == 32 && pene == 2) Debug.Log("jooooooder");
            // }
            if (!CanTileEnterToTheHash(newPossibleNeighbour, stepsAvailable))
            {
                if (!SpecialConditions(newPossibleNeighbour)) 
                    continue;
            }
            AddNewNeighbourSlave(newPossibleNeighbour);
            neighboursAccepted.Add(newPossibleNeighbour);
        }
        if (!_isFirstRoundComplete)
        {
            _stepsAvailableLoopMaster = _stepsAvailableSlave;
            _isFirstRoundComplete = true;
        }
        return neighboursAccepted;
    }
    private void AddNewNeighboursMaster()
    {
        _tilesMasterLoop = _tilesSlave;
        _stepsAvailableLoopMaster = _stepsAvailableSlave;
    }
    private void PrepareValuesForAnotherInteraction()
    {
        _tilesSlave = new List<TileRefactor>();
        _stepsAvailableSlave = new List<int>();
    }
    private bool CanTileEnterToTheHash(TileRefactor tileToCheck, int stepsAvailable) =>
        HadTileUnitWalkable(tileToCheck) && HadStepsAvailable(tileToCheck, stepsAvailable) &&
        IsPossibleToUnitPassTheTerrain(tileToCheck) && CheckIsTogether(tileToCheck);

    private bool SpecialConditions(TileRefactor tileToCheck) =>
        IsUnitTransport(tileToCheck.occupiedSoldier) || IsTileAPort(tileToCheck);
    private bool HadStepsAvailable(TileRefactor tileToCheck, int stepsAvailableLocal)
    {
        _stepsAvailable = stepsAvailableLocal - tileToCheck.dataVariable.ammountEffortToPass;
        return _stepsAvailable >= 0;
    }

    private bool CheckIsTogether(TileRefactor tileToCheck)
    {
        //I do this function because, sometime i was received some wrong results
        if (!_isFirstRoundComplete) return true;
        var tileMaster = _tilesMasterLoop[count];
        var isTileMasterCorrect = false;
        foreach (var tile in tileMaster.NeighboursInCross)
        {
            if (tileToCheck.spawnID == tile.spawnID) isTileMasterCorrect = true;
        }
        return isTileMasterCorrect;
    }
    private bool HadTileUnitWalkable(TileRefactor tileToCheck)
    {
        if (!tileToCheck.occupiedSoldier) return true;

        return IsUnitSearchedAlly(tileToCheck.occupiedSoldier);
    }
    private bool IsPossibleToUnitPassTheTerrain(TileRefactor tileToCheck)
    {
        if( _currentUnit.terrainUnitCanTransit.Contains(tileToCheck.dataVariable.terrainTypes)) return  true;
        return false;
    }
    private bool IsUnitSearchedAlly(Soldier unit)
    {
        return unit.playerThatCanControlThisUnit == _currentPlayer || unit.playerThatCanControlThisUnit == FactionUnit.Neutral;
    }

    private bool IsUnitTransport(Soldier unit)
    {
        
        if (!unit) return false;
        if (!IsUnitSearchedAlly(unit)) return false;
        if (_stepsAvailable < 0) return false;
        return unit.typeSoldier == TypeSoldier.Transport;
    }

    private bool IsTileAPort(TileRefactor tileToCheck)
    {
        if (!tileToCheck.occupiedBuild) return false;
        if (_stepsAvailable < 0) return false;
        return (tileToCheck.occupiedBuild.nameUnit == NameUnit.Port);
    }
    private void AddNewNeighbourSlave(TileRefactor neighbourAccepted)
    {
        _allTilesNeighbour.Add(neighbourAccepted);
        _tilesSlave.Add(neighbourAccepted);
        _stepsAvailableSlave.Add(_stepsAvailable);
    }
}
