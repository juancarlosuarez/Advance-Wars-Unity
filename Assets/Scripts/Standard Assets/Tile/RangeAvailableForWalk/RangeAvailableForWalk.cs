using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeAvailableForWalk
{
//     private HashSet<TileRefactor> _allTilesNeighbour = new HashSet<TileRefactor>();
//     private HashSet<TileRefactor> _tilesMasterLoop = new HashSet<TileRefactor>();
//     private HashSet<TileRefactor> _tilesSlave = new HashSet<TileRefactor>();
//
//     private List<int> _allStepsAvailable = new List<int>();
//     private List<int> _stepsAvailableLoopMaster = new List<int>();
//     private List<int> _stepsAvailableSlave = new List<int>();
//
//     private int _stepsAvailable;
//     private bool _isFirstRoundComplete = false;
//
//     private Soldier _currentUnit;
//
//     private readonly ScriptableObjectPlayers currentPlayer;
//     public RangeAvailableForWalk()
//     {
//         currentPlayer = Resources.Load<ScriptableObjectPlayers>("ScriptableObject/Data/DataSpecial/Player/CurrentPlayer");
//     }
//     public List<TileRefactor> CalculateHighLightTilesForPath(TileRefactor originTile)
//     {
//         _currentUnit = originTile.occupiedSoldier;
//
//         if (_currentUnit == null) Debug.Log("This Tile Dont had Unit");
//         
//         _tilesMasterLoop = CalculateNewPossibleNeighbours(originTile, _currentUnit.numberMoveAvailable);
//         for (int eachStep = 0; eachStep < _currentUnit.numberMoveAvailable - 1; eachStep++)
//         {
//             int count = 0;
//             foreach (var tile in _tilesMasterLoop)
//             {
//                 CalculateNewPossibleNeighbours(tile, _stepsAvailableLoopMaster[count]);
//
//                 if (tile == _tilesMasterLoop.Last())
//                 {
//                     AddNewNeighboursMaster();
//                     PrepareValuesForAnotherInteraction();
//                 }
//
//                 count++;
//             }
//         }
//         _allTilesNeighbour.Add(originTile);
//         return _allTilesNeighbour.ToList();
//     }
//     private HashSet<TileRefactor> CalculateNewPossibleNeighbours(TileRefactor tileToCheck, int stepsAvailable)
//     {
//         HashSet<TileRefactor> neighboursAccepted = new HashSet<TileRefactor>();
//
//         foreach (TileRefactor newPossibleNeighbour in tileToCheck.NeighboursInCross)
//         {
//             Debug.Log("me cago en todo");
//             if (!CanTileEnterToTheHash(newPossibleNeighbour, stepsAvailable))
//             {
//                 if (!SpecialConditions(newPossibleNeighbour)) 
//                     continue;
//             }
//             AddNewNeighbourSlave(newPossibleNeighbour);
//             neighboursAccepted.Add(newPossibleNeighbour);
//             if (!_isFirstRoundComplete)
//             {
//                 _stepsAvailableLoopMaster = _stepsAvailableSlave;
//                 _isFirstRoundComplete = true;
//             }
//         }
//         return neighboursAccepted;
//     }
//     private void AddNewNeighboursMaster()
//     {
//         _tilesMasterLoop = _tilesSlave;
//         _stepsAvailableLoopMaster = _stepsAvailableSlave;
//     }
//     private void PrepareValuesForAnotherInteraction()
//     {
//         _tilesSlave = new HashSet<TileRefactor>();
//         _stepsAvailableSlave = new List<int>();
//     }
//     private bool CanTileEnterToTheHash(TileRefactor tileToCheck, int stepsAvailable) =>
//         HadTileUnitWalkable(tileToCheck) && HadStepsAvailable(tileToCheck, stepsAvailable) &&
//         IsPossibleToUnitPassTheTerrain(tileToCheck);
//
//     private bool SpecialConditions(TileRefactor tileToCheck) =>
//         IsUnitTransport(tileToCheck.occupiedSoldier) || IsTileAPort(tileToCheck);
//     private bool HadStepsAvailable(TileRefactor tileToCheck, int stepsAvailableLocal)
//     {
//         _stepsAvailable = stepsAvailableLocal - tileToCheck.dataVariable.ammountEffortToPass;
//
//         return _stepsAvailable >= 0;
//     }
//
//     private bool HadTileUnitWalkable(TileRefactor tileToCheck)
//     {
//         if (!tileToCheck.occupiedSoldier) return true;
//
//         return IsUnitSearchedAlly(tileToCheck.occupiedSoldier);
//     }
//     private bool IsPossibleToUnitPassTheTerrain(TileRefactor tileToCheck)
//     {
//         if( _currentUnit.terrainUnitCanTransit.Contains(tileToCheck.dataVariable.terrainTypes)) return  true;
//         return false;
//     }
//     private bool IsUnitSearchedAlly(Soldier unit)
//     {
//         return unit.playerThatCanControlThisUnit == currentPlayer.reference || unit.playerThatCanControlThisUnit == FactionUnit.Neutral;
//     }
//
//     private bool IsUnitTransport(Soldier unit)
//     {
//         
//         if (!unit) return false;
//         if (IsUnitSearchedAlly(unit)) return false;
//         
//         return unit.typeSoldier == TypeSoldier.Transport;
//     }
//
//     private bool IsTileAPort(TileRefactor tileToCheck)
//     {
//         if (!tileToCheck.occupiedBuild) return false;
//         return (tileToCheck.occupiedBuild.nameUnit == NameUnit.Port);
//     }
//     private void AddNewNeighbourSlave(TileRefactor neighbourAccepted)
//     {
//         _allTilesNeighbour.Add(neighbourAccepted);
//         _tilesSlave.Add(neighbourAccepted);
//         _allStepsAvailable.Add(_stepsAvailable);
//         _stepsAvailableSlave.Add(_stepsAvailable);
//     }
// }
}